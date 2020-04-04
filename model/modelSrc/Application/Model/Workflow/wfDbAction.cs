using   Chloe.Entity;  
using   Chloe.Annotations;  
using   Chloe.Ext; 
using   Chloe.Ext.Aop; 
using   Chloe.Ext.Attribute;
using   Chloe.Descriptors; 
using   Chloe.Ext.Intf; 
using   System; 
using   System.Collections.Generic; 
using   System.Linq;
using   Newtonsoft.Json;
using   MessagePack;
using  System.ComponentModel.DataAnnotations;
#if NETFX
using System.Web.ModelBinding;
#endif
#if NETCORE
using  Microsoft.AspNetCore.Mvc.ModelBinding;
#endif
namespace Application.Model.Workflow
{
      using   Chloe.Ext.stateMachine;
        ///
        ///<summary>
        ///
        ///状态变迁表</summary>
public   partial class wfDbAction : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="1e2a0d46-2e27-447e-bace-479b22fbd43b")]
        public   void   exportToWfAction(out  wfAction  action)

{
            action = new wfAction();
            action.GuId = this.GuId;
            action.name = this.name;
            action.trigger = this.trigger;
            action.effect = this.effect;
            action.guard = this.guard;
            action.isStart = this.isStart;
            action.isEnd = this.isEnd;
            action.fieldName = this.fieldName;
            action.fromstateID = this.fromstateID;
            action.NoticeKeysJson = this.NoticeKeyJson;
            action.rolesCanDoJson = this.rolesCanDoJson;
            action.tostateID = this.tostateID;
            action.regionName = this.regionName;
            if (this.isStart)
                action.startorEnd = 0;
            else
            {
                if (this.isEnd)
                    action.startorEnd = -1;
                else
                    action.startorEnd = 1;
            }
            action.memo = this.memo;
           


        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="7280cf20-56ab-4b44-ad24-5b71fd6637c4")]
        public   void   ApplyNewFromWfAction(  wfAction  act)

{
            this.GuId = act.GuId;
            this.name = act.name;
            this.memo = act.memo;
            this.trigger = act.trigger;
            this.guard = act.guard;
            this.effect = act.effect;
            if (act.startorEnd == 0)
                this.isStart = true;
            else
                this.isStart = false;

            if (act.startorEnd == -1)
                this.isEnd = true;
            else
                this.isEnd = false;
            this.fieldName = act.fieldName;
            this.regionName = act.regionName;
            this.tostateID = act.tostateID;
            this.fromstateID = act.fromstateID;
            
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="76e04324-27f7-44e4-9fcd-e0b590e0953f_OnConstructer")]
       public wfDbAction(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="76e04324-27f7-44e4-9fcd-e0b590e0953f_OffConstructer")]
       [SerializationConstructor]
       public wfDbAction(): this(null)
{
 
       }






























































































































































































































































































































































}
}
