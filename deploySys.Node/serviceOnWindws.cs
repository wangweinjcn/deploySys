using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace deploySys.Node
{
    public class serviceOnWindws : ServiceControl
    {
        /// <summary>
        /// 待解决
        /// </summary>
        nodeService ns = new nodeService("");
        public bool Start(HostControl hostControl)
        {
          //  throw new NotImplementedException("功能受限，待定处理");
            ns.startService();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ns.stopService();
            return true;
        }
    }

}
