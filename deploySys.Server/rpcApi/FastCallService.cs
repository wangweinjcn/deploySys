using NettyRPC.Core;
using NettyRPC.Fast;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using deploySys.Server.rpcFilters;
using FrmLib.Extend;
using deploySys.Model;
using Application.Model.Base;

namespace deploySys.Server.rpcApi
{
   /// <summary>
    /// fast协议Api服务  
    /// </summary>
    public class rpcFastCallService : choRpcApiService
    {
        #region clientUpdate
        [Api]
        [FastLogFilter("比较差异文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public List<InstallFileInfo> CompareClientFiles(List<InstallFileInfo> clientfiles)
        {
            RunConfig.Instance.devlog.Info(String.Format("CompareClientFiles "));

            Dictionary<string, InstallFileInfo> inputDic = new Dictionary<string, InstallFileInfo>();
            foreach (var obj in clientfiles)
            {
                if (!inputDic.ContainsKey(obj.filename))
                    inputDic.Add(obj.filename, obj);
            }
            var difffiles = RunConfig.Instance.clientFiles.CompareDiffFile(inputDic);
            List<InstallFileInfo> resultlist = new List<InstallFileInfo>();
            foreach (var obj in difffiles)
            {
                InstallFileInfo ifi = new InstallFileInfo(obj.filename, obj.basepath, obj.hashcode);
                ifi.isdelete = obj.isdelete;
                resultlist.Add(ifi);
            }
            RunConfig.Instance.devlog.Info(String.Format("CompareClientFiles result:{0}", resultlist.Count));

            return resultlist;

        }
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public byte[] GetClientUpdateFile(string hashcode)
        {
            InstallFileInfo fi = RunConfig.Instance.clientFiles.getFileInfoByHashKey(hashcode);
            if (fi != null)
                return fi.filecontent;
            else
                return null;
        }
        #endregion
        [Api]
        [FastLogFilter("比较差异文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public IList<FileItem> CompareRemoteFiles(int taskId,List<FileItem> clientfiles)
        {
             RunConfig.Instance.devlog.Info(String.Format("CompareRemoteFiles "));
            ReleaseTask rt = objectSpace.ObjectForIntId<ReleaseTask>(taskId);
            if (rt == null)
                throw new Exception("ReleaseTask is null");

            var resultlist = rt.Ass_appVersion.CompareDiffFile(clientfiles);
           RunConfig.Instance.devlog.Info(String.Format("CompareRemoteFiles result:{0}",resultlist.Count));

            return resultlist;

        }
        [Api]
        [FastLogFilter("比较差异文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public IList<FileItem> getReleaseTaskFiles(int taskId)
        {
            ReleaseTask rt = objectSpace.ObjectForIntId<ReleaseTask>(taskId);
            if (rt == null)
                throw new Exception("ReleaseTask is null");
            var list = objectSpace.SpaceQuery<FileItem>().Join<appVersion>(Chloe.JoinType.InnerJoin, (a, b) => a.Ass_appVersion == b && b.Ass_ReleaseTask == rt).Select((a, b) => a).ToCommList();
            return list;
        }
       
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public byte[] GetFileContentByGuid(string guid)
        {
            return SysFunc.getInstance(objectSpace).GetFile(guid);
           
        }
        /// <summary>
        /// 新增docker容器实例
        /// </summary>
        /// <param name="baseDir"></param>
        /// <param name="instanceId"></param>
        /// <param name="proxyPort"></param>
        /// <param name="hostid"></param>
        /// <param name="taskId"></param>
        /// <param name="msAppId"></param>
        /// <returns></returns>
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public int addNewDockerInstance(string baseDir,string instanceId,int proxyPort,int hostid,int taskId,int msAppId,int htId)
        {
            var host = objectSpace.ObjectForIntId<HostResource>(hostid);
            if (host == null)
                return -1;
            var rt = objectSpace.ObjectForIntId<ReleaseTask>(taskId);
            if (rt == null)
                return -1;
            var msapp = objectSpace.ObjectForIntId<MicroServiceApp>(msAppId);
            if (msapp == null)
                return -1;
            HostTask ht = objectSpace.ObjectForIntId<HostTask>(htId);
            if (ht == null)
                return -1;
            DockerInstance di = new DockerInstance(objectSpace);
            di.baseDir = baseDir;
            di.instanceId = instanceId;
            di.proxyPort = proxyPort;

            di.Ass_HostResource_Id = hostid;
            di.Ass_ReleaseTask_Id = taskId;
            di.Ass_MicroServiceApp_Id = msAppId;
            di.status = (int)EnumDockerInstanceStatus.prepare;
            ht.dockerInanceId = di.instanceId;
            UpdateDatabase();
            return di.Id;
        }
        /// <summary>
        /// 设置docker实例的状态
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public int setDockInstanceStatus(int diId,int status)
        {
            DockerInstance di = objectSpace.ObjectForIntId<DockerInstance>(diId);
            if (di == null)
                return -1;
            di.status = status;
            UpdateDatabase();
            return 0;

        }
  
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public int setTaskStart(int hrtId )
        {
            HostTask hrt = objectSpace.ObjectForIntId<HostTask>(hrtId);
            if (hrt == null)
                throw new Exception("HostReleaseTask is null");
            hrt.Status = (int)EnumHostTaskStatus.started;
            UpdateDatabase();
            return 0;
        }
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public int setTaskFial(int hrtId,string failedMsg)
        {
            HostTask hrt = objectSpace.ObjectForIntId<HostTask>(hrtId);
            if (hrt == null)
                throw new Exception("HostReleaseTask is null");
            hrt.Status = (int)EnumHostTaskStatus.failed;
            hrt.ProcessInfo = failedMsg;
            UpdateDatabase();
            return 0;
        }
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public int setTaskComplete(int hrtId )
        {
            HostTask hrt = objectSpace.ObjectForIntId<HostTask>(hrtId);
            if (hrt == null)
                throw new Exception("HostReleaseTask is null");
            hrt.Status = (int)EnumHostTaskStatus.finished;
            UpdateDatabase();
            return 0;
        }
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public List<object> getAllTask()
        {
            string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();

            UnitNodeSession uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];
            if (uns == null)
                return null;
            var hostid = uns.und.hostId;
            var list = objectSpace.SpaceQuery<HostTask>().Join<appVersion>(Chloe.JoinType.LeftJoin, (a, b) => a.Ass_appVersion == b && a.Id == hostid).Join<MicroServiceApp>(Chloe.JoinType.InnerJoin, (a, b, c) => b.Ass_MicroServiceApp == c).Join<ReleaseTask>(Chloe.JoinType.LeftJoin, (a, b, c,  e) => a.Ass_ReleaseTask == e).Select((a, b, c,  e) => new {HostTask=a,appVersion=b,versionApp=c,ReleaseTask=e }).ToCommList();
            return list as List<object>;

        }
        /// <summary>
        /// 获取本机任务对应app的所有dockerInstance
        /// </summary>
        /// <param name="hostTaskId"></param>
        /// <returns></returns>
        [Api]
        [FastLogFilter("根据hashcode获取文件")]
        [FastLoginFilter] // 登录了才能请求Api
        public IList<DockerInstance> getAppDockerInstnce(int hostTaskId)
        {
            HostTask ht = objectSpace.ObjectForIntId<HostTask>(hostTaskId);
            if (ht == null)
                throw new Exception("hostTask is null") ;
            if (ht.taskType != (int)EnumHostTaskType.releaseTask)
                throw new Exception("not releaseTask");


            var list = objectSpace.SpaceQuery<DockerInstance>().Join<MicroServiceApp>(Chloe.JoinType.InnerJoin, (a, b) => a.Ass_MicroServiceApp == b && a.Ass_HostResource_Id==ht.HostId).Join<ReleaseTask>(Chloe.JoinType.InnerJoin, (a, b, c) => c.Ass_MicroServiceApp == b && b.Id == ht.Ass_ReleaseTask_Id).Select((a, b, c) => a).ToCommList();
            return list;

        }

        /// <summary>
        /// 获取服务组件版本号
        /// </summary>       
        /// <returns></returns>
        [Api]
        [FastLogFilter("获取版本号")]
        public string GetVersion()
        {
            return this.GetType().Assembly.GetName().Version.ToString();
        }
        /// <summary>
        /// 节点取消注册
        /// </summary>
        /// <returns></returns>
        [Api]
        [FastLogFilter("注册节点")]
        public int NodeUnRegiste()
        {
            string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();

            UnitNodeSession uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];
            if (uns == null)
                return 0;
            var host = objectSpace.ObjectForIntId<HostResource>(uns.und.hostId);
            if (host != null)
            {
                host.inLine = false;
                UpdateDatabase();
            }
            return 0;
        }
        /// <summary>
        /// 节点下行通道注册
        /// </summary>
        /// <param name="macid"></param>
        /// <returns></returns>
        [Api]
        [FastLogFilter("注册节点")]
        public int NodeStoCRegiste(string macid)
        {
            try
            {


                    string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();
                if (nodeToken!=null)
                {
                    UnitNodeSession uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];
                    this.CurrentContext.Session.Tag.Set("ApiToken", macid);
                    this.CurrentContext.Session.Tag.Set("Logined", true);

                    uns.stocSession = this.CurrentContext.Session;
                    RunConfig.Instance.devlog.Info(String.Format("registe ok"));
                    return 0;

                }
                {
                    RunConfig.Instance.devlog.Info(String.Format("registe fail"));
                    return -1;
                }
            }

            catch (Exception exp)
            {
                RunConfig.Instance.devlog.Info(String.Format("registe  exception,thread id:{0},message:{1}",
                     System.Threading.Thread.CurrentThread.ManagedThreadId, exp.Message));
                return -2;
            }


        }

        /// <summary>
        /// 注册远程节点
        /// </summary>
        /// <param name="macid"></param>
        /// <returns></returns>
        [Api]
        [FastLogFilter("注册节点")]
        public int NodeRegiste(string macid)
        {
            try
            {
                RunConfig.Instance.devlog.Info(String.Format("Login ,thread id:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
                var host = objectSpace.SpaceQuery<HostResource>().Where(a => a.macId == macid).FirstOrDefault();
                if (host!=null)
                {
                    UnitNodeSession uns = null;
                    // 标记会话已登录成功
                    if (RunConfig.Instance.nodedeviceStat_dic.ContainsKey(macid))
                        uns = RunConfig.Instance.nodedeviceStat_dic[macid];
                    else
                    {
                        uns = new UnitNodeSession();
                        RunConfig.Instance.nodedeviceStat_dic.Add(macid, uns);
                        uns.und = new UnitNodeDevice();
                        uns.und.MacID = macid;
                        uns.und.hostId = host.Id;
                        uns.und.LastRegisterDt = DateTime.Now;
                        uns.und.AliveDt = DateTime.Now;
                    }
                 

                    this.CurrentContext.Session.Tag.Set("ApiToken", macid);
                    this.CurrentContext.Session.Tag.Set("Logined", true);
                    uns.und.AliveDt = DateTime.Now;
                    uns.session = this.CurrentContext.Session;
                    RunConfig.Instance.devlog.Info(String.Format("registe ok"));
                    return 0;
                }
                else
                {
                    RunConfig.Instance.devlog.Info(String.Format("registe fail"));
                    return -1;
                }
            }

            catch (Exception exp)
            {
                RunConfig.Instance.devlog.Info(String.Format("registe  exception,thread id:{0},message:{1}",
                     System.Threading.Thread.CurrentThread.ManagedThreadId, exp.Message));
                return -2;
            }

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Api]
        [FastLogFilter("芯跳上报")]
        [FastLoginFilter] // 登录了才能请求Api
        public int SayAlive()
        {
            try
            {
                string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();

                UnitNodeSession uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];
               
                lock (uns)
                {
                    uns.und.AliveDt = System.DateTime.Now;
                }
              
                if (uns.session != this.CurrentContext.Session)
                    uns.session = this.CurrentContext.Session;
                RunConfig.Instance.devlog.Info(String.Format("sayalive ok"));
                return 0;
            }
            catch (Exception exp)
            {
                RunConfig.Instance.devlog.Info(String.Format("SayAlive  exception,thread id:{0},message:{1}", 
                     System.Threading.Thread.CurrentThread.ManagedThreadId, exp.Message));
                return -2;
            }
        }
      
       
        [Api]
        [FastLogFilter("获取需要同步的数据")]
        [FastLoginFilter] // 登录了才能请求Api
        public string GetRsycData()
        {
            try
            {
               RunConfig.Instance.devlog.Info(String.Format("GetRsycData ,thread id:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId));

                string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();

                var uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];

                

                JArray jlist=new JArray();
                JObject jDataObj;
                JObject jtmpobj;

                return jlist.ToString();
            }
            catch (Exception exp)
            {
               RunConfig.Instance.devlog.Info(String.Format("GetRsycData  exception,thread id:{0},message:{1}",
                     System.Threading.Thread.CurrentThread.ManagedThreadId, exp.Message));
                throw exp;
            }
        }
        [Api]
        [FastLogFilter("在远程执行命令")]
        [FastLoginFilter] // 登录了才能请求Api
        public void DoRemoteCmd(string cmd,string macid)
        {
            UnitNodeDevice und = RunConfig.Instance.getUNDByMac(macid);
            if (und != null)
            {
                TimeSpan ts = DateTime.Now - und.AliveDt.Value;
                   if(ts.Seconds<RunConfig.Instance.alertTimeForClientSayAlive)
                   {
                       FastSession fs = RunConfig.Instance.nodedeviceStat_dic[und.MacID].session;
                       fs.Tag.Set("destSession", this.CurrentContext.Session);
                       fs.InvokeApi("DoShellCMD", cmd);
                   }
            
            }
           
        }
        [Api]
        [FastLogFilter("停止在远程执行命令")]
        [FastLoginFilter] // 登录了才能请求Api
        public void StopRemoteCmd(string cmd, string macid)
        {
            UnitNodeDevice und = RunConfig.Instance.getUNDByMac(macid);
            if (und != null)
            {
                TimeSpan ts = DateTime.Now - und.AliveDt.Value;
                if (ts.Seconds < RunConfig.Instance.alertTimeForClientSayAlive)
                {
                    FastSession fs = RunConfig.Instance.nodedeviceStat_dic[und.MacID].session;
                    fs.Tag.Remove("destSession");
                    fs.InvokeApi("StopShell", cmd);
                }

            }
            
        }
      


        /// <summary>
        /// 检查是否达到告警阀值，如果达到，添加告警；
        /// </summary>
        /// <param name="nds">节点数据</param>
        private void chackAndReportAlarm(osMetrics metrics)
        { 
        
        }
        /// <summary>
        /// 上报节点性能接口
        /// </summary>
        /// <param name="cpuusage">cpu使用</param>
        /// <param name="memuseage">内存使用</param>
        /// <param name="diskusage">磁盘使用</param>
        /// <param name="loadaverage">平均负载</param>
        /// <returns>
        /// 0：成功
        /// -1：有异常发生
        /// </returns>
        [Api("ReportPerformance")]
        [FastLogFilter("上报性能数据")]
        [FastLoginFilter] // 登录了才能请求Api
        public int ReportPerformance(osMetrics metrics)
        {
            try
            {
                Console.WriteLine("ReportPerformance");
                string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();
                var uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];
                CirQueue<osMetrics> nodeQueue = uns.und.metrics;
               
                nodeQueue.Enqueue(metrics);
                chackAndReportAlarm(metrics);
                return 0;
            }
            catch (Exception exp)
            {
                runLoger.Error("ReportPerformance Error:" + exp.Message);
                return -1;
            }
            
        }
        [Api("CmdResult")]
        [FastLogFilter("命令结果")]
        [FastLoginFilter] // 登录了才能请求Api
        public int CmdResult(string x)
        {
           // this.CurrentContext.Session.Tag.Set("cmdResult", x);
            //Program.RemoteCmdresult.Enqueue(x);
            FastSession resFs = this.CurrentContext.Session.Tag.Get("destSession").As<FastSession>();
            resFs.InvokeApi("CmdResult", x);
            return 0;
           
        }
        [Api]
        [FastLogFilter("获取该节点的所有参数信息")]
        [FastLoginFilter] // 登录了才能请求Api
        public HostResource getHostParam(osMetrics metrics)
        {
            string nodeToken = this.CurrentContext.Session.Tag.Get("ApiToken").AsString();
            UnitNodeSession uns = RunConfig.Instance.nodedeviceStat_dic[nodeToken];
            if (uns == null)
                return null;
            var host = objectSpace.ObjectForIntId<HostResource>(uns.und.hostId);
            Console.WriteLine(JsonConvert.SerializeObject(metrics));
            host.cpuCoreNumber = metrics.cpu.cpuCoreNumber;
            host.cpuName = metrics.cpu.cpuname;
            host.OsName = metrics.osName;
            UpdateDatabase();
            return host;
            //var result = JsonConvert.SerializeObject(host);
            //Console.WriteLine(result);
            //return result;
        }

        [Api]
        [FastLogFilter("获取该节点的所有参数信息")]
        [FastLoginFilter] // 登录了才能请求Api
        public string getTestString(int length)
        {
            return createStrForLength(length);

        }
        private string createStrForLength(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append("a");
            return sb.ToString();
        }

    }
}
