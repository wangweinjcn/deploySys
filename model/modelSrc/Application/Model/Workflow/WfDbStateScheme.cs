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
        ///订单状态迁移方案</summary>
public   partial class WfDbStateScheme : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="09fdf683-d8ee-4b8e-a5f7-05313f5692d5_OnConstructer")]
       public WfDbStateScheme(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="09fdf683-d8ee-4b8e-a5f7-05313f5692d5_OffConstructer")]
       [SerializationConstructor]
       public WfDbStateScheme(): this(null)
{
 
       }






























































































































































































































































































































































}
}
