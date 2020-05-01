using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Collections;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Configuration;
using log4net;

using System.Net.Http;
using System.Net;
using System.IO;
using System.Reflection;
using System.Collections.Concurrent;
using FrmLib.Extend;
using deploySys.Model;
using NettyRPC;

namespace deploySys.Node
{




    public class RunConfig
    {
        public ILog runlog { get; private set; }
        public List<TimeDoTask> alltask;
        private static RunConfig _instance;


        public SoftUpdateFiles clientFiles;
        public HostResource  serverHostResource{ get; private set; }
         public IPAddress hostAddr { get; private set; }
        public string MacID { get;  set; }
        public string ServiceName { get; private set; }

        public string RestartCmd { get; private set; }
        public string TaskConfigFile { get; private set; }
        public int AutoReconnectTimeSpan { get; private set; }
        public string rpcHost { get; private set; }
        public int rpcPort { get; private set; }
       

        public void Refresh()
        {
            Console.WriteLine("refresh configure");
            RunConfig._instance = null;
        }
        private static RunConfig getInstance()
        {
            if (RunConfig._instance == null)
            {
                RunConfig._instance = new RunConfig();
               
               
                Console.WriteLine("creattaks ok");   
             
               
              
            }
            return RunConfig._instance;

        }
        public static RunConfig Instance { get { return getInstance(); } }

        private  IList<TimeDoTask> initTasklist(string taskConfigFile)
        {
            if (string.IsNullOrEmpty(taskConfigFile))
                return null;
            var rootdir = System.AppDomain.CurrentDomain.BaseDirectory;
            string xmlfilename = System.IO.Path.Combine(rootdir, taskConfigFile);
            if (!File.Exists(xmlfilename))
                return null;

            List<TimeDoTask> alltask = new List<TimeDoTask>();
            FrmLib.Log.commLoger.runLoger.Debug(string.Format("now createTaskList"));

            Type[] nodeTaskHandles = FrmLib.Extend.tools_static.GetTypesFromAssemblysByType(typeof(ICronTask));


            XmlDocument tmpxmldoc = Static_xmltools.GetXmlDocument(xmlfilename);
            XmlNodeList alllist = null;
            string xpathstr = "/configuration/CronTask";
            alllist = Static_xmltools.SelectXmlNodes(tmpxmldoc, xpathstr);
            FrmLib.Log.commLoger.runLoger.Debug(string.Format("createTaskList count:{0}", alllist.Count));
            TimeDoTask tdt = null;
            TimeNowDoEventHandler myfunc = null;
            foreach (XmlNode onenode in alllist)
            {
                int tasktype = int.Parse(Static_xmltools.GetXmlAttr(onenode, "taskType"));
                string paramvalue = Static_xmltools.GetXmlAttr(onenode, "paramValue");
                string procesorName = Static_xmltools.GetXmlAttr(onenode, "procesorName");
                try
                {
                    var nodeTaskHandle = FrmLib.Extend.tools_static.getTypeHaveMethodByName(nodeTaskHandles, procesorName);
                    if (nodeTaskHandle == null)
                        continue;
                    myfunc = (TimeNowDoEventHandler)Delegate.CreateDelegate(typeof(TimeNowDoEventHandler), nodeTaskHandle, procesorName);
                    if (tasktype == (int)enum_taskType.interval)
                    {
                        tdt = new TimeDoTask(int.Parse(paramvalue), myfunc);
                    }
                    else
                        tdt = new TimeDoTask(paramvalue, myfunc, (enum_taskType)tasktype);
                    alltask.Add(tdt);
                }
                catch (Exception exp)
                {
                    FrmLib.Log.commLoger.runLoger.Error("create task error, info:" + onenode.InnerText);
                    return null;
                }
            }
            return alltask;
        }
        public void setHost()
        {
            
            this.serverHostResource = nodeClient.Instance.GetHost();
        }
        public void startTask()
        {
            var taskConfFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs", "CronTask.config");

            var tasklist = initTasklist(taskConfFileName);
            foreach (var task in tasklist)
                Task.Run(() => task.start());
        }

        private RunConfig()
        {
                     
            try
            {
               
               
                this.rpcHost = commSetting.Configuration["deploySys:rpcHost"];
                IPAddress ipaddr;
                var strlist = rpcHost.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (strlist.Count() == 0 || strlist.Count() > 4)
                    throw new Exception("");
                int tmp;
                if (int.TryParse(strlist[0], out tmp) && int.TryParse(strlist[1], out tmp))
                {
                    ipaddr = IPAddress.Parse(rpcHost);
                }
                else
                {
                    ipaddr = Dns.GetHostEntry(rpcHost).AddressList[0];
                }
                hostAddr = ipaddr;
                this.rpcPort = int.Parse(commSetting.Configuration["deploySys:rpcPort"]);
                clientFiles = SoftUpdateFiles.instance;
                clientFiles.initFiles(AppDomain.CurrentDomain.BaseDirectory, false, false);
               
               
             
               
            }
            catch (Exception e)
            {
                runlog.Fatal("init system error;" + System.Environment.NewLine + e.Message);
            }

        }
    }
}
