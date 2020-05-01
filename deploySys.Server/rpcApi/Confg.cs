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
using System.IO;

using Newtonsoft.Json;

using System.Globalization;
using Newtonsoft.Json.Converters;

using System.Reflection;
using NettyRPC.Fast;
using FrmLib.Extend;
using deploySys.Model;
using Chloe;
using System.Collections.Concurrent;
using deploySys.Server.lib;
using deploySys.Common;
using Ace;

namespace deploySys.Server
{
    public class UnitNodeDevice
    {
      

        public DateTime? AliveDt { get; internal set; }

        public DateTime LastRegisterDt { get; internal set; }
        public string SoftVersion { get; internal set; }
        public string MacID { get; internal set; }
        public int hostId { get; internal set; }
        public osMetrics lastMetrics;
        public CirQueue<osMetrics> metrics;
        public UnitNodeDevice()
        {
            metrics = new CirQueue<osMetrics>(RunConfig.Instance.maxNodeStateLength);
        }
    }
    public class UnitNodeSession
    {
        public FastSession session;
        public FastSession stocSession;
        public UnitNodeDevice und;
     
        public UnitNodeSession()
        {
            session = null;
            und = new UnitNodeDevice();
            
        }


       
    }
    public partial class RunConfig
    {

        private ConcurrentQueue<IDbContext> dbpool = new ConcurrentQueue<IDbContext>();
        SQLiteConnectionFactory dbconnfactory = null;
        /// <summary>
        /// 登录节点设备性能监控列表，主键是token,macid；主键值是UnitNodeSession对象
        /// </summary>
        public Dictionary<string, UnitNodeSession> nodedeviceStat_dic;

        /// <summary>
        /// 芯跳超时节点
        /// </summary>
        public List<UnitNodeDevice> NotAliveNode;
        /// <summary>
        /// 客户端文件集合
        /// </summary>
        public SoftUpdateFiles clientFiles { get; set; }
        /// <summary>
        /// 客户端软件存放目录
        /// </summary>
        public string clientSoftDir { get; set; }
        public string connstring{ get; set; }
        public Region currRegion { get; set; }
        /// <summary>
        /// 节点性能数据长度
        /// </summary>
        public int maxNodeStateLength { get; set; }
        /// <summary>
        /// rpc服务端口
        /// </summary>
        public int rpcPort { get; set; }
        /// <summary>
        /// rcp服务等待队列长度
        /// </summary>
        public int rpcBackLength { get; set; }

        /// <summary>
        /// 节点超时阈值（分钟）
        /// </summary>
        public int maxOfflineMinutes { get; private set; }
        public UnitNodeDevice getUNDByMac(string macid)
        {
            if (nodedeviceStat_dic.ContainsKey(macid))
                return nodedeviceStat_dic[macid].und;
            
            return null;
        }
        public IDbContext GetEcoSpace()
        {
            Console.WriteLine("get ecospace init");
            IDbContext conn;
            if (!dbpool.TryDequeue(out conn))
            {
                conn = dbconnfactory.CreateConnection() as IDbContext;
            }
            return conn;
        }

        public void ReturnEcoSpace(IDbContext es)
        {
            if (es != null)
                dbpool.Enqueue(es);
            else
                devlog.Debug(String.Format("return es ,but es is null"));
        }
        


        public bool isValidRemoteFastTcpToken(string remoteTcpToken,string macid)
        {
            return true;
        }
        public  FrmLib.Log.myLogger runlog { get { return FrmLib.Log.commLoger.runLoger; } }
        public FrmLib.Log.myLogger devlog { get { return FrmLib.Log.commLoger.devLoger; } }
        private static RunConfig _instance;


        public static RunConfig Instance { get { return getInstance(); } }

        public int alertTimeForClientSayAlive { get; private set; }
        public string TaskConfigFile { get; private set; }

        
  
        private static RunConfig getInstance()
        {
            if (RunConfig._instance == null)
            {
                RunConfig._instance = new RunConfig();
                _instance.loadNodeDataToDic();
            }
                return RunConfig._instance;

        }
      
        public void Refresh()
        {
            RunConfig._instance = null;
           
        }
        private void loadNodeDataToDic()
        {

        }
        /// <summary>
        /// 获取负载最轻的主机sessionId
        /// </summary>
        /// <param name="count">合计数量</param>
        /// <param name="zoneId">部署集群Id</param>
        /// <returns></returns>

        public IList<Tuple<string,int>> getMinLoadHosts(int zoneId,int count)
        {
            //返回主机sessionid和对应的端口
            return null;
        }
        public void delayInitClientFiles()
        {
                         this.clientFiles.initFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, clientSoftDir));
        }
        private RunConfig()
     {

         try
         {


               
            
             nodedeviceStat_dic = new Dictionary<string, UnitNodeSession>();

                this.rpcBackLength = int.Parse(Globals.Configuration["deploySys:rpcBackLength"]);
                this.rpcPort = int.Parse(Globals.Configuration["deploySys:rpcPort"]);
                this.maxNodeStateLength = int.Parse(Globals.Configuration["deploySys:maxNodeStateLength"]);
                this.clientSoftDir = (Globals.Configuration["deploySys:clientSoftDir"]);
                this.clientFiles = SoftUpdateFiles.instance;  

                maxOfflineMinutes = int.Parse(Globals.Configuration["deploySys:maxOfflineMinutes"]);
                delayInitClientFiles();
                runlog.Info("runconfig ok");
         }
         catch (Exception e)
         {
             runlog.Error("init system error;" + System.Environment.NewLine + e.Message);
         }

     }
    }

    
   
    public class CirQueue<T> : ConcurrentQueue<T>
    {
        private int _maxCount = 0;
        private ConcurrentQueue<T> _queue;
        private IList<T> lists;
        private T lastobj;
        public CirQueue(int maxCount)
        {
            _maxCount = maxCount;
            _queue = new ConcurrentQueue<T>();
            lists = new List<T>();
        }
        public int getMaxCount()
        {
            return _maxCount;
        }
        public IList<T> getlist()
        {
            return lists;
        }
        public void Enqueue(T obj)
        {
            if (_queue.Count >= _maxCount)
            {
                T tmpobj=Dequeue();
                if (tmpobj!=null)
                {
                    _queue.Enqueue(obj);
                    lists.Add(obj);
                }
                else
                    throw new KeyNotFoundException("TryDequeueError");

            }
            else
            {
                lists.Add(obj);
               
                _queue.Enqueue(obj);
            }
             lastobj = obj;
        }
        public T Dequeue()
        {
            T tmpobj;
            if (_queue.TryDequeue(out tmpobj))
            {
                lists.Remove(tmpobj);
                return tmpobj;
            }
            else
                return default(T);

        }
        public T getLastOne()
        {
            return lastobj;
        }
    }
}
