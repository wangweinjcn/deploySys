using Ace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;
using System.Text;

using Ace.Application;

using Chloe.Ext;

using Application.Model.Base;

using Chloe.Ext.Intf;
using FrmLib.Extend;
using deploySys.Model;
using System.IO;
using SharpCompress.Readers;
using SharpCompress.Common;
using System.Security.Cryptography;
using System.Reflection;
using Chloe;

namespace deploySys.Server
{

  
    public class Crontask:ICronTask
    {
        Dictionary<int, string> mailTemplateDic = new Dictionary<int, string>();
       
        private static IObjectSpaceService _oss = new ObjectSpaceService();
        private static IDistributedCache _distributedCache = Globals.ServiceProvider.GetService(typeof(IDistributedCache)) as IDistributedCache;

        private static string findMainFileDir(string basepath,string filename)
        {
            if (!Directory.Exists(basepath))
                return null;
            DirectoryInfo Dir = new DirectoryInfo(basepath);
            FileInfo[] allfiles = Dir.GetFiles();
            foreach (var obj in allfiles)
            {
                var fn = Path.GetFileName(obj.FullName);
                if (string.Equals(filename, fn, StringComparison.CurrentCultureIgnoreCase))
                {
                    var fpath = Path.GetFullPath(obj.FullName);
                    return fpath;
                }
            }
            DirectoryInfo[] DirSub = Dir.GetDirectories();
            foreach (var obj in DirSub)
            {
                return findMainFileDir(obj.FullName,filename);
            }
            return null;
        }
       
        /// <summary>
        /// 判断是否是奇数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static bool IsOdd(int num)
        {
            return (num & 1) == 1;
        }
    /// <summary>
    /// 按照实例数量选择部署的主机和端口
    /// 实现策略：取中位数以下的主机平均分配总数量
    /// </summary>
    /// <param name="zoneId"></param>
    /// <param name="count"></param>
    /// <param name="space"></param>
    /// <returns></returns>
    private static IList<Tuple<string, int>> getMinInstanceHosts(int zoneId, int count,IContextSpace space)
        {
            IList<Tuple<string, int>> result = new List<Tuple<string, int>>();
            var allhr = space.SpaceQuery<HostResource>().Where(a => a.Ass_Zone_Id == zoneId).ToList();
            if (allhr.Count == 0)
                return null;
            ///注意右连接
            ///
            var glist = space.SpaceQuery<DockerInstance>().Join<HostResource>(Chloe.JoinType.RightJoin, (a, b) => a.Ass_HostResource == b && b.Ass_Zone_Id == zoneId).Select((a, b) => new {a.Id,b.clientSessionId }).GroupBy(a=>a.clientSessionId).Select(a=>new {a.clientSessionId,count=  Sql.Count() }).OrderBy(a=>a.count).ToCommList();
            double MedianValue;
            if (IsOdd(glist.Count))
            {
                MedianValue =glist[ (glist.Count - 1) / 2 + 1].count;
            }
            else
            {
                MedianValue = (double)(glist[glist.Count / 2].count + glist[glist.Count / 2 + 1].count) / 2;
            }
           // var haveCount = (from x in glist where x.count <= MedianValue select x.count).Sum();
            var hostC = (from x in glist where x.count <= MedianValue select x).Count();

         

            var list = (from x in glist where x.count <= MedianValue select x).ToList();

            var remainCount = count; //剩余分配数量
            var remainHostcount = hostC+1;//剩余主机数量
            int avCount;
            foreach (var obj in list)
            {
                var host = (from x in allhr where x.clientSessionId == obj.clientSessionId select x).FirstOrDefault();
                if (host == null)
                {
                    throw new Exception("指定clientSessionId的主机不存在");
                }
                 if (remainHostcount <= 0 || remainCount<=0)
                    break;

                avCount = (int)Math.Ceiling(((double)(remainCount )) / remainHostcount);
                var tmpcount = Math.Min(avCount, remainCount);
                var allocPortList = host.getAllocPorts(tmpcount);
                foreach (var oneport in allocPortList)
                {
                    result.Add(new Tuple<string, int>(obj.clientSessionId, oneport));
                }
                remainCount = remainCount - allocPortList.Count();
                remainHostcount--;
            }

            return result;
        }
        private static void processVersionToHost(appVersion appv)
        {
            var task = appv.Ass_ReleaseTask;
            if (task == null)
                return;
            if (task.Ass_MicroServiceApp.serviceType ==(int)EnumAppServiceType.webSite && string.IsNullOrEmpty(task.HostIds))
            {
                throw new Exception("web站点的发布不支持自动分配主机，必须指定主机");
            }

            IList<HostResource> selectRes;
            var hostids = task.HostIds.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
            selectRes = appv.GetSpace().SpaceQuery<HostResource>().Where(a => hostids.Contains(a.Id)).ToList();

            if (task.Ass_MicroServiceApp.serviceType == (int)EnumAppServiceType.webSite)
            {
                if (selectRes.Count == 0)
                    throw new Exception("指定的主机不存在");
                foreach (var onehost in selectRes)
                {
                    HostTask hrt = new HostTask(appv.GetSpace());
                    hrt.Ass_appVersion = appv;
                    hrt.Ass_ReleaseTask = task;
                    hrt.HostId = onehost.Id;
                    hrt.allocPort = -1;
                }
            }
            else
            {
                if (task.releaseType == 0) //新增实例
                {
                    IList<Tuple<string, int>> hostports;

                       if (selectRes.Count == 0)
                    {
                        if (task.selectHostPolicy == 0)
                        {
                            hostports = RunConfig.Instance.getMinLoadHosts(task.Ass_Zone_Id.Value, task.count);

                        }
                        else
                        {
                            hostports = getMinInstanceHosts(task.Ass_Zone_Id.Value, task.count, appv.GetSpace());
                        }
                    }
                    else
                    {
                        ///指定主机的分配
                        hostports = new List<Tuple<string, int>>();
                        var remainCount = task.count; //剩余分配数量
                        var remainHostcount = selectRes.Count;//剩余主机数量
                        int avCount;
                        foreach (var obj in selectRes)
                        {

                            if (remainHostcount <= 0 || remainCount <= 0)
                                break;

                            var host = obj;
                            avCount = (int)Math.Ceiling(((double)(remainCount)) / remainHostcount);
                            var tmpcount = Math.Min(avCount, remainCount);
                            var allocPortList = host.getAllocPorts(tmpcount);

                            foreach (var oneport in allocPortList)
                            {
                                hostports.Add(new Tuple<string, int>(obj.clientSessionId, oneport));
                            }
                            remainCount = remainCount - allocPortList.Count();
                            remainHostcount--;
                        }

                    }
                    if (hostports == null)
                        return;
                    foreach (var oneh in hostports)
                    {
                        var host = appv.GetSpace().SpaceQuery<HostResource>().Where(a => a.clientSessionId == oneh.Item1).FirstOrDefault();
                        if (host == null)
                            throw new Exception("指定sessionId的host不存在");
                        HostTask hrt = new HostTask(appv.GetSpace());
                        hrt.Ass_appVersion = appv;
                        hrt.Ass_ReleaseTask = task;
                        hrt.HostId = host.Id;
                        hrt.allocPort = oneh.Item2;
                    }
                }
                else
                {
                    var dis = appv.GetSpace().SpaceQuery<DockerInstance>().Where(a => a.Ass_MicroServiceApp == appv.Ass_MicroServiceApp).Join<HostResource>(Chloe.JoinType.InnerJoin, (a, b) => a.Ass_HostResource == b && b.Ass_Zone_Id == task.Ass_Zone_Id).Select((a, b) => new { DockerInstance = a, HostResource = b }).ToCommList();
                    foreach (var onedi in dis)
                    {
                        HostTask hrt = new HostTask(appv.GetSpace());
                        hrt.Ass_appVersion = appv;
                        hrt.Ass_ReleaseTask = task;
                        hrt.HostId = onedi.HostResource.Id;
                        hrt.allocPort = -1; //不需要分配新的端口号
                        hrt.dockerInanceId = onedi.DockerInstance.instanceId;
                    }

                }
            }
        }
        /// <summary>
        /// 处理新增发布任务,拆解为单个文件和具体到主机的任务
        /// </summary>
       public static void doUncompressReleaseTask()
        {
            var _objectSpace = _oss.getContextSpace();

            try
            {
               var list= _objectSpace.SpaceQuery<ReleaseTask>().Where(a => a.status == 0).OrderBy(a=>a.CreateDt).ToList();

                foreach (var obj in list)
                {
                    SysFunc sf = SysFunc.getInstance(_objectSpace);
                    var content=    sf.GetFile(obj.FileGuid);
                    var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"tmpdata", obj.Id.ToString());
                    try
                    {
                        if (!Directory.Exists(baseDir))
                            Directory.CreateDirectory(baseDir);
                        //解压文件
                        using (var stream = new MemoryStream(content))
                        {
                            using (var reader = ReaderFactory.Open(stream))
                            {
                                if (!reader.Entry.IsDirectory)
                                {
                                    Console.WriteLine(reader.Entry.Key);
                                    reader.WriteEntryToDirectory(baseDir, new ExtractionOptions()
                                    {
                                        PreserveAttributes = true,
                                        ExtractFullPath = true,
                                        Overwrite = true
                                        
                                    });
                                }
                            }
                        }
                        var fpath = findMainFileDir(baseDir, obj.Ass_MicroServiceApp.rootDirMainFile);
                        if (fpath == null)
                        {
                            obj.status = (int) EnumReleaseTaskStatus.error;;
                            obj.ProcessInfo = "找不到根目录";
                        }
                        else
                        {
                            appVersion appver = new appVersion(_objectSpace);
                            appver.version = obj.Version;
                            appver.Ass_ReleaseTask = obj;
                            List<string> conffile = null;
                            var msapps = appver.Ass_MicroServiceApp;
                            if (msapps == null)
                                throw new Exception("找不到微服务应用");
                            if (!string.IsNullOrEmpty(msapps.confFileNames))
                                conffile = msapps.confFileNames.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                            else
                                conffile = new List<string>();
                            IList<FileItem> allFIs = new List<FileItem>();
                            dsFunc.initPathToFileItem(appver, baseDir, fpath,true,conffile, allFIs);
                            processVersionToHost(appver);
                            obj.status =(int) EnumReleaseTaskStatus.assigned;
                        }
                    }
                    catch (Exception ex)
                    {
                        obj.status = (int) EnumReleaseTaskStatus.error;
                        obj.ProcessInfo = ex.Message;

                    }
                    finally {
                        Directory.Delete(baseDir, true);
                    }
                      _objectSpace.UpdateAllDirtyObjects(true);
                }


              
                
            }
            catch (Exception e)
            {
                FrmLib.Log.commLoger.runLoger.FatalFormat("doImportCbeTask fatal error:{0},{1}", e.Message, e.StackTrace);
            }
            finally
            {
                _oss.returnContextSpace(_objectSpace);

            }
        }

       
    }
}
