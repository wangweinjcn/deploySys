using System;
using System.Collections.Generic;
using System.Text;

namespace deploySys.Node
{
   public class nodeService
    {
        /// <summary>
        /// 运行的容器Id
        /// 
        /// </summary>
       private  static string diId=null;
        public static string getInstanceId()
        {
            return diId;
        }
        public nodeService(string _Id)
        {
            diId = _Id;
        }

        public void startService()
        {
            bool isregisted = false;
           var x= RunConfig.Instance;
           
            foreach (var macid in tools.getNetworkInterfacesMac())
            {
                if (nodeClient.Instance.Registe(macid,diId) == 0)
                {
                    isregisted = true;
                    RunConfig.Instance.MacID = macid;
                    FrmLib.Log.commLoger.runLoger.Info("regist macid is :"+RunConfig.Instance.MacID + " old macid:"+macid);
                    RunConfig.Instance.setHost();
                    RunConfig.Instance.startTask();
                    break;
                }
            }
            if (!isregisted)
                throw new Exception("节点未登记");
           
        }
        public void stopService()
        {

        }
    }
}
