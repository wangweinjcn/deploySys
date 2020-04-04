

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
    /// 下行通道客户端
    /// 长连接单例模式
    /// </summary>
    public class sToCNodeClient : RpcClient
    {
       
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static sToCNodeClient  _instance ;

        private System.Timers.Timer _timer;
        private Queue cmdresult;
        private Process shellp = null;
        private static object _lockOjb = new object();
        /// <summary>
        /// 获取唯一实例
        /// </summary>
        public static sToCNodeClient Instance
        {
            get

            {
                
                if (_instance == null)
                {
                    lock (_lockOjb)
                    {
                        if (_instance == null)
                        {
                            _instance = new sToCNodeClient();
                            _instance.TimeOut = TimeSpan.FromSeconds(300);
                        }
                    }
                  
                }
                if (!_instance.connected )
                    _instance.refreshConnect();
                return _instance;
            }
         
        }


        private sToCNodeClient() : base(RunConfig.Instance.hostAddr, RunConfig.Instance.rpcPort,new mpSerializer())
        { }
        private sToCNodeClient(ISerializer _serializer):base(RunConfig.Instance.hostAddr,RunConfig.Instance.rpcPort, _serializer)
        {

        }


        public DateTime lastCMDDodt;
        private void refreshConnect()
        {
            //  Client._instance = null;
           
            while (!connected)
            {
                FrmLib.Extend.AsyncHelpers.RunSync(()=> connect());
                connected = true;
                System.Threading.Thread.Sleep(10000);
            }
            if (!string.IsNullOrEmpty(RunConfig.Instance.MacID))
            {
                this.Registe(RunConfig.Instance.MacID);
            }

        }

        public int unRegiste()
        {
            try
            {
                int ret = this.InvokeApi<int>("NodeUnRegiste").GetAwaiter().GetResult();
                return ret;
            }
            catch (Exception exp)
            {

                FrmLib.Log.commLoger.runLoger.Error("node registe some error:" + exp.Message);
                throw exp;
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
            catch (Exception exp)
            {
                
                FrmLib.Log.commLoger.runLoger.Error("node registe some error:"+exp.Message);
                throw exp;
            }
        
        }
        


        [Api]
        public string getRunlogContent()
        {
            string filename = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log"), "runInfo.log");
            if (File.Exists(filename))
            {
                string str = File.ReadAllText(filename);
                return str;
            }
            else
                return "";
        }
        
        private void cmdResultSend(object sender, System.Timers.ElapsedEventArgs e)
        {
            string reportstr = null;
            while (cmdresult.Count > 0)
            {
                reportstr = reportstr + System.Environment.NewLine + cmdresult.Dequeue().ToString();

            }
            if (!string.IsNullOrEmpty(reportstr))
            {
                this.InvokeApi("CmdResult", reportstr);
            }
            if ((DateTime.Now - lastCMDDodt).Minutes > 30)//超过30分钟没有发送命令，停止shell；
                this.StopShell();
        }
        public void startshell()
        {
            shellp = new Process();
            cmdresult = new Queue();
            _timer = new System.Timers.Timer(2000) { AutoReset = true };
            _timer.Elapsed += cmdResultSend;
            shellp.StartInfo.FileName = "cmd.exe ";
            shellp.StartInfo.Arguments = "/k ";
            shellp.StartInfo.UseShellExecute = false;
            shellp.StartInfo.RedirectStandardInput = true;
            shellp.StartInfo.RedirectStandardOutput = true;

            shellp.StartInfo.RedirectStandardError = true;
            shellp.StartInfo.CreateNoWindow = false;
            shellp.Start();
     //       _timer.Start();   
        }
        
       
    

        [Api]
        public void StopShell()
        {
            shellp.Close();
            _timer.Stop();
        }

        /// <summary>
        /// 服务器发来的命令
        /// </summary>
        /// <param name="cmdStr">需要执行的命令</param>
        [Api]
        public void DoShellCMD(string cmdStr)
        {
            try
            {
                lastCMDDodt = DateTime.Now;
            FrmLib.Log.commLoger.runLoger.Debug(string.Format(cmdStr));
           if(shellp==null)
            startshell();
            shellp.StandardInput.AutoFlush = true;
            shellp.StandardInput.WriteLine("clear");
            shellp.StandardInput.WriteLine(cmdStr);
            //shellp.StandardInput.Close();
            shellp.OutputDataReceived += new DataReceivedEventHandler(OnDataReceived);
            shellp.BeginOutputReadLine();
                   
    
               
            }
            catch (Exception e)
            {
                FrmLib.Log.commLoger.runLoger.Error(string.Format(e.Message));
                this.InvokeApi("CmdResult", e.Message);
            }
        }
        private  void OnDataReceived(object Sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                FrmLib.Log.commLoger.runLoger.Debug(string.Format(e.Data));
                cmdresult.Enqueue(e.Data);
                FrmLib.Log.commLoger.runLoger.Debug(string.Format("quereCount:{0}", cmdresult.Count.ToString()));
                //  this.InvokeApi("CmdResult", e.Data);
            }
        }
       
        
    }
}
