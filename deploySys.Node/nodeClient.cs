

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

using NettyRPC;
using NettyRPC.Core;
using FrmLib.Extend;
using deploySys.Model;

namespace deploySys.Node
{
    /// <summary>
    /// 客户端
    /// 长连接单例模式
    /// </summary>
    public class nodeClient : RpcClient
    {
       
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static nodeClient  _instance ;


        private Queue cmdresult;
        private Process shellp = null;
        private bool nowReconnect = false;
        private int recountMaxCount=20;
        private static object _lockOjb = new object();
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
                            _instance = new nodeClient();
                            _instance.TimeOut = TimeSpan.FromSeconds(30);
                        }
                    }
                  
                }
                if (!_instance.connected )
                    _instance.refreshConnect();
                if (!_instance.connected)
                    return null;
                return _instance;
            }
         
        }


        private nodeClient() : base(RunConfig.Instance.hostAddr, RunConfig.Instance.rpcPort,new mpSerializer())
        { }
        private nodeClient(ISerializer _serializer):base(RunConfig.Instance.hostAddr,RunConfig.Instance.rpcPort, _serializer)
        {

        }


        private void refreshConnect()
        {
            //  Client._instance = null;
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
                    FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
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
            if (!string.IsNullOrEmpty(RunConfig.Instance.MacID))
            {
                this.Registe(RunConfig.Instance.MacID);
            }

        }

        public HostResource  GetHost()
        {
            try
            {
                var x = InvokeApi<HostResource>("getHostParam", osMetrics.getvalue()).GetAwaiter().GetResult();
                Console.WriteLine(JsonConvert.SerializeObject(x));
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
        public int Registe(string macid)
        {
            try
            {
                var x = InvokeApi<int>("NodeRegiste", macid).GetAwaiter();
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
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }

        internal JArray getNodeTask()
        {
            try
            {
                var x = this.InvokeApi<List<Object>>("getAllTask").GetAwaiter().GetResult();
                return JArray.FromObject(x);
            }
            catch (Exception ex)
            {
                 FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
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
        public int reportTaskFinish(int taskId)
        {
           try{  return this.InvokeApi<int>("setTaskComplete",taskId).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                return -100;
            }
        }
        public int reportTaskFail(int taskId,string msg)
        {
          try{   return this.InvokeApi<int>("setTaskFial",taskId,msg).GetAwaiter().GetResult();
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
        public int addDockInstance(DockerInstance di,int hostid,int taskId,int msAppId,int htId)
        {
         try{    return this.InvokeApi<int>("addNewDockerInstance", di.baseDir,di.instanceId,di.proxyPort,hostid,taskId,msAppId,htId).GetAwaiter().GetResult();
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
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
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
                FrmLib.Log.commLoger.runLoger.Error(string.Format("CompareClientFiles error{0}",exp.Message));
                return new List<InstallFileInfo>();
            }
        }
        public byte[] GetClientUpdateFile(string hashcode)
        {
            try
            {
                return  this.InvokeApi<byte[]>("GetClientUpdateFile", hashcode).GetAwaiter().GetResult();
            }
            catch (Exception exp)
            {
                FrmLib.Log.commLoger.runLoger.Error(string.Format("GetClientUpdateFile error{0}", exp.Message));
                return null;
            }
        }

       
       
        
    }
}
