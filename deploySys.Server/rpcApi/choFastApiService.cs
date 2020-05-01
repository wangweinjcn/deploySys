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

          
        }
        catch (Exception exp)
        {
           RunConfig.Instance.runlog.Error(String.Format("EcoFastApiService init,exception {0}", exp.Message));

        }
    }
   
   

  
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
         
            this.ManagedDispose(this._objectSpace);
            base.Dispose(disposing);
        }
    }


    protected virtual IDbContext ManagedCreate()
    {
      
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
