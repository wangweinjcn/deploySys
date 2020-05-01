
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using NettyRPC.Fast;
//using FrmLib.Extend;
namespace deploySys.Server.rpcFilters
{
    /// <summary>
    /// 日志过滤器
    /// </summary>
    public class FastLogFilter : FastFilterAttribute
    {
        private string message;

        public FastLogFilter(string message)
        {
            this.message = message;
        }

        protected override void OnExecuting(ActionContext filterContext)
        {

            FastSession fs = filterContext.Session;
           
        }
    }
}
