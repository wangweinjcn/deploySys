using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;

namespace deploySys.Node
{
    public class serviceOnWindws : ServiceControl
    {
        nodeService ns = new nodeService();
        public bool Start(HostControl hostControl)
        {
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
