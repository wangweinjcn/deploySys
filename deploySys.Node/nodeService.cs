using System;
using System.Collections.Generic;
using System.Text;

namespace deploySys.Node
{
   public class nodeService
    {
        public nodeService()
        { }

        public void startService()
        {
            bool isregisted = false;
           var x= RunConfig.Instance;
           
            foreach (var macid in tools.getNetworkInterfacesMac())
            {
                if (nodeClient.Instance.Registe(macid) == 0)
                {
                    isregisted = true;
                    RunConfig.Instance.MacID = macid;
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
