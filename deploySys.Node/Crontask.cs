using Ace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Distributed;
using System.Text;


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
using FrmLib.Http;
using Docker.DotNet;

namespace deploySys.Node
{

  
    public class Crontask:ICronTask
    {

      static   DockerClient dockerC = new DockerClientConfiguration(new Uri("http://127.0.0.1:2375")).CreateClient();
       //static private HttpHelper hclient = new HttpHelper();

        public static void checkSelfUpdate()
        {
            try
            {
                var changelist = nodeClient.Instance.CompareClientFiles();

                if (changelist == null)
                    return;
                if (changelist.Count > 0)
                {
                    string timestr = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    string bakpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, timestr);
                    if (!Directory.Exists(bakpath))
                        Directory.CreateDirectory(bakpath);

                    foreach (var obj in changelist)
                    {


                        if (obj.isdelete)
                        {
                            File.Delete(Path.Combine(obj.basepath, obj.filename));
                            RunConfig.Instance.runlog.Info(string.Format("now delete file {0}", obj.filename));

                        }
                        else
                        {
                            byte[] gzipcontent = nodeClient.Instance.GetClientUpdateFile(obj.hashcode);
                            byte[] content = obj.GZipDecompress(gzipcontent);
                            RunConfig.Instance.runlog.Debug(string.Format("now write bak dir, {0}", obj.filename));
                            File.WriteAllBytes(Path.Combine(bakpath, obj.filename), content);
                        }
                    }
                    RunConfig.Instance.runlog.Debug("now move to basedir ");
                    string cmd = "mv " + Path.Combine(bakpath, "*.*") + "  " + AppDomain.CurrentDomain.BaseDirectory;
                    RunConfig.Instance.runlog.Debug("cmd string: " + cmd);
                    string res = tools.excutScript(cmd);
                    //cmd = "cp " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "*.o") + "  /lib";
                    //RunConfig.Instance.runlog.Debug("now cp *.o  string: " + cmd);
                    //res = tools.excutScript(cmd);
                    //RunConfig.Instance.runlog.Debug("mv ended:" + res);
                    //cmd = "cp " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "*.so") + "  /lib";
                    //RunConfig.Instance.runlog.Debug("now cp *.so  string: " + cmd);
                    //res = tools.excutScript(cmd);
                    //cmd = "chmod +x " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "*.sh");
                    //RunConfig.Instance.runlog.Debug("cmd string: " + cmd);
                    //res = tools.excutScript(cmd);
                    //RunConfig.Instance.runlog.Debug("chmod  ended:" + res);
                    //RunConfig.Instance.runlog.Debug("now delete dir: ");
                    //Directory.Delete(bakpath);
                    //cmd = "ldconfig";
                    //RunConfig.Instance.runlog.Debug("now do ldconfig : " + cmd);
                    //res = tools.excutScript(cmd);
                    RunConfig.Instance.runlog.Debug("now set restart: ");
                    tools.restartSelf();

                }

            }
            catch (Exception ex)
            { }



        }

        private static void addNewContainer(ReleaseTask rt,HostTask ht,MicroServiceApp app)
        {

        }
        private static void startDockerInstance(string instanceId)
        {
             FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStartParameters()));
        }
        private static void stopDockerInstance(string instanceId)
        {
              FrmLib.Extend.AsyncHelpers.RunSync(()=>dockerC.Containers.StopContainerAsync(instanceId,new Docker.DotNet.Models.ContainerStopParameters()));       
        }
        private static void restartDockerInstance(string instanceId)
        {

            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStartParameters()));
        }
        private static void overrideTaskFiles(ReleaseTask rt,appVersion version,MicroServiceApp msapps,string destPath)
        {
            IList<FileItem> needOverRideFiles = null;
            switch (rt.overridePolicy)
            {
                case (0): //部分包全覆盖

                    
                    

                case (2): //全量包全覆盖
                     needOverRideFiles = FrmLib.Extend.AsyncHelpers.RunSync<List<FileItem>>(() => nodeClient.Instance.InvokeApi<List<FileItem>>("getReleaseTaskFiles", rt.Id));
                    break;
                case (1):  //全量包有修改更新
                    List<string> conffile;
                    if (!string.IsNullOrEmpty(msapps.confFileNames))
                        conffile = msapps.confFileNames.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                    else
                        conffile = new List<string>();
                    IList<FileItem> existfilelist = new List<FileItem>();
                    dsFunc.initPathToFileItem(version, destPath, destPath, false,conffile,  existfilelist);
                    needOverRideFiles = FrmLib.Extend.AsyncHelpers.RunSync<List<FileItem>>(() => nodeClient.Instance.InvokeApi<List<FileItem>>("CompareRemoteFiles", rt.Id,existfilelist));
                    break;
            }
            foreach (var onefile in needOverRideFiles)
            {
                if (onefile.isConfFile && !rt.confFileOverride)
                    continue;

                var fullfilename = Path.Combine(destPath,onefile.retationPath,onefile.fileName);
                if (onefile.remoteShouldDeleted && File.Exists(fullfilename))
                {
                    System.IO.File.Delete(fullfilename);
                    continue;
                }
                byte[] filecontent = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.InvokeApi<byte[]>("GetFileContentByGuid", onefile.Guid));
                File.WriteAllBytes(fullfilename, filecontent);
            }
        }
        public static void processTaskAsync()
        {
           
           // var xxx = client.Containers.CreateContainerAsync();
            var alltask = nodeClient.Instance.getNodeTask();
            foreach (JObject onetask in alltask)
            {
                string url;
                HostTask ht =JsonConvert.DeserializeObject<HostTask>( onetask["HostTask"].ToString());
                DockerInstance di=null;
                if(FrmLib.Extend.tools_static.jobjectHaveKey(onetask,"DockerInstance")&&onetask["DockerInstance"]!=null  && !string.IsNullOrEmpty(onetask["DockerInstance"].ToString()))
                    di= JsonConvert.DeserializeObject<DockerInstance>(onetask["DockerInstance"].ToString());

                try
                {  nodeClient.Instance.reportTaskStart(ht.Id);
                    switch (ht.taskType)
                    {
                        case ((int)EnumHostTaskType.releaseTask):

                            ReleaseTask rt = JsonConvert.DeserializeObject<ReleaseTask>(onetask["ReleaseTask"].ToString());
                            if (rt == null)
                            {
                                throw new Exception("rt is null");
                            }
                            if (rt.releaseType == 0) //新增
                            {

                            }
                            else
                            {

                                if (rt.releaseType == 1) //更新
                                {
                                    if (di == null || string.IsNullOrEmpty(di.instanceId))
                                        throw new Exception("docker instance is null");
                                    MicroServiceApp msapp=JsonConvert.DeserializeObject<MicroServiceApp>(onetask["versionApp"].ToString());
                                    if(msapp==null)
                                        throw new Exception("versionApp is null");
                                    appVersion version = JsonConvert.DeserializeObject<appVersion>(onetask["appVersion"].ToString());
                                    if (version == null)
                                        throw new Exception("version is null");
                                    var listinstance = FrmLib.Extend.AsyncHelpers.RunSync<List<DockerInstance>>(() => nodeClient.Instance.InvokeApi<List<DockerInstance>>("getAppDockerInstnce", ht.Id));
                                    var appdirList = (from x in listinstance select x.baseDir).Distinct().ToList();
                                    foreach (var onedir in appdirList)
                                    {
                                        var appPath = Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir, onedir);
                                        if (!Directory.Exists(appPath))
                                        {
                                            throw new Exception("docker working dir is not exist,"+appPath);
                                        }


                                        overrideTaskFiles(rt,version,msapp, appPath);
                                        restartDockerInstance(di.instanceId);
                                    }
                                }

                            }

                            break;
                        case ((int)EnumHostTaskType.stopDockerInstance):
                                                
                            if (di == null || string.IsNullOrEmpty(di.instanceId))
                                throw new Exception("docker instance is null");

                            stopDockerInstance(di.instanceId);
                            break;
                        case ((int)EnumHostTaskType.restartDockerInstance):
                           
                            if (di == null || string.IsNullOrEmpty(di.instanceId))
                                throw new Exception("docker instance is null");
                          
                          restartDockerInstance(di.instanceId);
                          
                            break;

                    }
                      nodeClient.Instance.reportTaskFinish(ht.Id);
                }
                catch (Exception exp)
                {
                    nodeClient.Instance.reportTaskFail(ht.Id, exp.Message);
                }
            }
        }
        public static void reportPerf()
        {
            try
            { 
            nodeClient.Instance.ReportPerf();
            }
            catch (Exception ex)
            { }
        }
        public static void sayAlive()
        {
            try
            {
                nodeClient.Instance.SayAlive();
            }
            catch (Exception ex)
            { }

        }
    }
}
