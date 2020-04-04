using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using log4net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NettyRPC.Fast;
using Chloe;
using Chloe.Ext.Intf;
using Chloe.Ext;

namespace deploySys.Server.rpcApi
{
public    class choRpcApiService : FastApiService
{
 
    private bool _DoNotDeactivateEcoSpaceOnDone;
    protected ILog runLoger, devlog;
        private IObjectSpaceService _oss;
        private IObjectSpaceService csService
        {
            get
            {
                if (_oss == null)
                    _oss = ObjectSpaceService.getInstance();
                return _oss;
            }
        }
        protected void UpdateDatabase(bool useTransaction = true)
        {

            this.objectSpace.UpdateAllDirtyObjects(useTransaction);
        }
        protected override void OnActionExecuting(ActionContext context)
        {

            base.OnActionExecuting(context);
        }
     
        protected override void OnActionExecuted(ActionContext context)
        {
            System.Threading.Thread.CurrentPrincipal = null;
            if (_objectSpace != null && csService != null)
                csService.returnContextSpace(this._objectSpace);
            this._objectSpace = null;
            base.OnActionExecuted(context);
        }
        private IContextSpace _objectSpace;
        protected IContextSpace objectSpace
        {
            get
            {

                if (_objectSpace == null)
                {
                    _objectSpace = csService.getContextSpace();

                }
                return _objectSpace;
            }
        }
        public choRpcApiService()
    {
        try
        {

            DateTime oldTime = new DateTime(1970, 1, 1);
            RunConfig.Instance.devlog.Info(String.Format("EcoFastApiService creator,thread id:{0}",
                System.Threading.Thread.CurrentThread.ManagedThreadId));

         
           
            RunConfig.Instance.devlog.Info(String.Format("EcoFastApiService init ok"));
        }
        catch (Exception exp)
        {
           RunConfig.Instance.devlog.Info(String.Format("EcoFastApiService init,exception {0}", exp.Message));

        }
    }
   
   

  
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
           RunConfig.Instance.devlog.Info(String.Format("ecoFtpApiserver dispose ,thread id:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId));

            this.ManagedDispose(this._objectSpace);
            base.Dispose(disposing);
        }
    }


    protected virtual IDbContext ManagedCreate()
    {
       RunConfig.Instance.devlog.Info(String.Format("ManagedCreate,thread id:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId));
        IDbContext des = RunConfig.Instance.GetEcoSpace() ;
        return des;
    }
   
    protected virtual void ManagedDispose(IContextSpace es)
    {
            System.Threading.Thread.CurrentPrincipal = null;
            if (_objectSpace != null && csService != null)
                csService.returnContextSpace(this._objectSpace);
            this._objectSpace = null;
           
        }
    }
}
