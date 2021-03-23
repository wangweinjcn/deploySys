

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Timers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

using System.Net;
using log4net;
using FrmLib.Http;
using NettyRPC;
using NettyRPC.Core;
using FrmLib.Extend;
using deploySys.Model;
using Application.Model.Base;

namespace deploySys.Node
{
    /// <summary>
    /// 客户端
    /// 长连接单例模式
    /// </summary>
    public class nodeClient : RpcClient
    {
        private string dockerInstanceId;
        private string clientVersion { get {  return this.GetType().Assembly.GetName().Version.ToString();  } }
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static nodeClient  _instance ;


        private Queue cmdresult;
        private Process shellp = null;
        private bool nowReconnect = false;
        private int recountMaxCount=20;
        private static object _lockOjb = new object();
        private HttpHelper hh = new HttpHelper();
        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static nodeClient Instance
        {
            get

            {
                
                if (_instance == null)
                {
                    lock (_lockOjb)
                    {
                        if (_instance == null)
                        {
                            _instance = new nodeClient(new mpSerializer());
                            _instance.TimeOut = TimeSpan.FromSeconds(150);
                        }
                    }
                  
                }
              //  FrmLib.Log.commLoger.runLoger.Info("_instance.connected:"+_instance.connected.ToString());
                if (!_instance.connected )
                    _instance.refreshConnect();
                if (!_instance.connected)
                    return null;
                return _instance;
            }
         
        }


        private nodeClient() : base(RunConfig.Instance.hostAddr, RunConfig.Instance.rpcPort,null)
        { }
        private nodeClient(ISerializer _serializer):base(RunConfig.Instance.hostAddr,RunConfig.Instance.rpcPort, _serializer)
        {

        }


        private void refreshConnect()
        {
            //  Client._instance = null;

            FrmLib.Log.commLoger.runLoger.Info("refreshConnect");
            int reconnectCount = 0;
            lock (_lockOjb)
            {
                if (nowReconnect)
                    return;
                else
                    nowReconnect = true;
            }
            while (!connected)
            { try
                {
                    FrmLib.Extend.AsyncHelpers.RunSync(() => connect());
                  
                }
                catch (Exception ex)
                {
                    FrmLib.Log.commLoger.runLoger.Error("refreshConnect error:"+ FrmLib.Extend.tools_static.getExceptionMessage(ex));
                    reconnectCount++;
                }
                finally
                {
                    if (connected)
                        nowReconnect = false;
                    else
                        System.Threading.Thread.Sleep(reconnectCount *1000);
                   
                }
            }
            FrmLib.Log.commLoger.runLoger.Info("now refresh registe with "+RunConfig.Instance.MacID);
            if (string.IsNullOrEmpty(RunConfig.Instance.MacID))
            {
                FrmLib.Log.commLoger.runLoger.Info(" restart search macid");
                foreach (var macid in tools.getNetworkInterfacesMac())
                {
                    if (nodeClient.Instance.Registe(macid, dockerInstanceId) == 0)
                    {

                        RunConfig.Instance.MacID = macid;
                        break;
                    }
                }


            }
            else
            {
                FrmLib.Log.commLoger.runLoger.Info("now call remote  registe ");
                this.Registe(RunConfig.Instance.MacID, dockerInstanceId);
                FrmLib.Log.commLoger.runLoger.Info("now call remote  registe  ok");
            }

        }
        /// <summary>
        /// 获取指定目录下的文件列表（日志文件）
        /// </summary>
        /// <param name="logsDir"></param>
        /// <returns></returns>
        [Api]
        public string getlogs(string logsDir)
        {
            if (!System.IO.Directory.Exists(logsDir))
            {
                return null;
            }
            var  root = new DirectoryInfo(logsDir);
              FileInfo[] files=root.GetFiles();
            var list = (from x in files where x.Name.Contains("log") select new {x.FullName, x.Name,x.Length,x.CreationTime,x.LastWriteTime }).ToList();
            return JArray.FromObject(list).ToString();
        }
        /// <summary>
        /// 获取日志文件内容
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        [Api]
        public int deletelog(string fn)
        {
            try
            {
                FrmLib.Log.commLoger.devLoger.Debug(" delete fn:" + fn);
                if (File.Exists(fn))
                {
                    File.Delete(fn);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
            /// <summary>
            /// 获取日志文件内容
            /// </summary>
            /// <param name="fn"></param>
            /// <returns></returns>
            [Api]
        public string getlog(string fn)
        {
            FrmLib.Log.commLoger.devLoger.Debug("fn:"+fn);
            if (!File.Exists(fn))
                return null;
            var content = System.IO.File.ReadAllText(fn);
            FrmLib.Log.commLoger.devLoger.Debug("" + content);
            return content;
        }
        public byte[] getNginxConfFileContent(string fileName)
        {
            try
            {
                var x = InvokeApi<byte[]>("getNginxConfFileContent",fileName).GetAwaiter().GetResult();

                return x;
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return null;
            }
        }

        public HostResource  GetHost()
        {
            try
            {
                var x = InvokeApi<HostResource>("getHostParam", osMetrics.getvalue()).GetAwaiter().GetResult();
               
                return x;
            }
            catch (Exception ex)
            {
                 FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return null;
            }
        }
        public int unRegiste()
        {
            try
            {
                int ret = this.InvokeApi<int>("NodeUnRegiste").GetAwaiter().GetResult();
                return ret;
            }
            catch (Exception ex)
            {

                 FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }

        }
        public int Registe(string macid,string diId)
        {
            try
            {
                this.dockerInstanceId = diId;
                Console.WriteLine("instanceId:{0},macId:{1}", diId, macid);
                var x = InvokeApi<int>("NodeRegiste", macid,diId,this.clientVersion).GetAwaiter();
                int ret = x.GetResult();
                return ret;
            }
            catch (Exception ex)
            {
                
                 FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        
        }
        
        public int ReportPerf()
        {

            try
            {
                Console.WriteLine("ReportPerf");
                return this.InvokeApi<int>("ReportPerformance",osMetrics.getvalue()).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

                /*  JObject jobj = JObject.Parse(exp.Message);
                  JToken returocode;
                  if (jobj != null && jobj.TryGetValue("", out returocode))
                  {

                  }
                  FrmLib.Log.commLoger.runLoger.Error("node login some error:" + exp.Message);
                  */
                FrmLib.Log.commLoger.runLoger.Error("report perf:"+ FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }

        internal JArray getNodeTask()
        {
            var i = 0;
            try
            {
                Console.WriteLine("getAllTask");
                i = 10;
                var x = this.InvokeApi<string>("getAllTask").GetAwaiter().GetResult();
                i = 20;
                Console.WriteLine(x);
                if (!string.IsNullOrEmpty(x))
                {
                    i = 30;
                    var jarr = JArray.Parse(x);
                    return jarr;
                }
                return null;
            }
            catch (Exception ex)
            {
                 FrmLib.Log.commLoger.runLoger.Error(i.ToString()+ " :getNodeTask error; "+ FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return new JArray();
            }
        
        }

       
        public int reportTaskStart(int taskId)
        {
            try
            {
                return this.InvokeApi<int>("setTaskStart", taskId).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
        public int reportTaskFinish(int taskId,string siteConfFile)
        {
           try{
                return this.InvokeApi<int>("setTaskComplete",taskId,siteConfFile).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
       
        public int reportTaskFail(int taskId,string msg)
        {
          try{
                return this.InvokeApi<int>("setTaskFial",taskId,msg).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
        public int setDockInstanceStatus(int diId, EnumDockerInstanceStatus status)
        {
          try{  return this.InvokeApi<int>("setDockInstanceStatus", diId, (int)status).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
        /// <summary>
        /// 添加docker实例
        /// </summary>
        /// <param name="di"></param>
        /// <param name="hostid"></param>
        /// <param name="taskId"></param>
        /// <param name="msAppId"></param>
        /// <param name="htId"></param>
        /// <param name="isNninx"></param>
        /// <param name="nginxConfDir"></param>
        /// <returns></returns>
        public int addDockInstance(DockerInstance di,int hostid,int taskId,int msAppId,int htId,bool isNninx,string nginxConfDir="")
        {
         try{
                Console.WriteLine("addDockInstance");
                return this.InvokeApi<int>("addNewDockerInstance", di.baseDir,di.instanceId,di.proxyPort,hostid,taskId,msAppId,htId,isNninx,nginxConfDir).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
        public int SayAlive()
        {
          try{
                return  FrmLib.Extend.AsyncHelpers.RunSync<int>(() => this.InvokeApi<int>("SayAlive"));
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error("say alive error:"+FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
        
        
        
       
        /// <summary>
        /// 获取服务组件版本号
        /// </summary>       
        /// <returns></returns>      
        public Task<string> GetVersion()
        {
         try{   return this.InvokeApi<string>("GetVersion");
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return null;
            }
        }

        public List<InstallFileInfo> CompareClientFiles()
        {
            try
            {
                if (RunConfig.Instance.clientFiles == null)
                    return null;
                return this.InvokeApi<List<InstallFileInfo>>("CompareClientFiles", RunConfig.Instance.clientFiles.softfile).GetAwaiter().GetResult();
            }
            catch (Exception exp)
            {
                FrmLib.Log.commLoger.runLoger.Error(string.Format("CompareClientFiles error{0}",FrmLib.Extend.tools_static.getExceptionMessage( exp)));
                return new List<InstallFileInfo>();
            }
        }
        public Task<byte[]> GetTaskFileContentByGuid(string Guid)
        {
            // var task = this.InvokeApi<byte[]>("GetFileContentByGuid", Guid) ;
            // return task;
            try
            {
                // var bytes=  this.InvokeApi<byte[]>("GetClientUpdateFile", hashcode).GetAwaiter().GetResult();
                var response = hh.doAsycHttpRequest(RunConfig.Instance.httpGetFileUrl + "getTaskFile/" + Guid, new Dictionary<string, string>(), true);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("GetTaskFileContentByGuid httpRequest is error code: " + response.StatusCode);
                }
                var bytes = hh.asyncResponseToFile(response);

                return bytes;
            }
            catch (Exception exp)
            {
                FrmLib.Log.commLoger.runLoger.Error(string.Format("GetTaskFileContentByGuid error{0}", exp.Message));
                return null;
            }
        }
        public byte[] GetClientUpdateFile(string hashcode)
        {
            try
            {
                // var bytes=  this.InvokeApi<byte[]>("GetClientUpdateFile", hashcode).GetAwaiter().GetResult();
                var response = hh.doAsycHttpRequest(RunConfig.Instance.httpGetFileUrl+"getClientUpdateFile/" + hashcode, new Dictionary<string,string>(), true);
                if (!response.IsSuccessStatusCode)
                { 
                     throw new Exception("httpRequest is error code: "+response.StatusCode);
                }
                var bytes = hh.ResponseToFile(response);
                if (bytes == null)
                    throw new Exception("content is null");
                return bytes;
            }
            catch (Exception exp)
            {
                FrmLib.Log.commLoger.runLoger.Error(string.Format("GetClientUpdateFile error{0}", exp.Message));
                return null;
            }
        }

       
       
        
    }
}
