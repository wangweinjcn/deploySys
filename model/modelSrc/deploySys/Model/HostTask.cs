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
namespace deploySys.Model
{
        ///
        ///<summary>
        ///
        ///主机发布任务</summary>
public   partial class HostTask : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="3e05e19a-09d8-410e-897f-80fff5d69cc7_OnConstructer")]
       public HostTask(IContextSpace space): base(space)
{
 Status=0;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="3e05e19a-09d8-410e-897f-80fff5d69cc7_OffConstructer")]
       [SerializationConstructor]
       public HostTask(): this(null)
{
 
       }












































}
}
