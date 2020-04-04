
using NettyRPC.Fast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace deploySys.Server.rpcFilters
{
    /// <summary>
    /// 表示登录过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class FastLoginFilter : FastFilterAttribute
    {
        /// <summary>
        /// 登录过滤器
        /// </summary>
        public FastLoginFilter()
        {
            this.Order = -1;
        }

        protected override void OnExecuting(ActionContext filterContext)
        {
            var valid = filterContext.Session.Tag.Get("Logined").AsBoolean();
            if (valid == false)
            {
                // filterContext.Session.Close();
                filterContext.Result = "未登录就尝试请求其它服务";
            }

        }

    }
}
