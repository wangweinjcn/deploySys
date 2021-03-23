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
namespace Application.Model.Base
{
        ///
        ///<summary>
        ///
        ///外部接口调用任务</summary>
public   partial class ExternalDataTask : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="7329fb3a-d1eb-4893-9aef-4ddc6f2e197c_OnConstructer")]
       public ExternalDataTask(IContextSpace space): base(space)
{
 sendCount=0;
HaveDone=0;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="7329fb3a-d1eb-4893-9aef-4ddc6f2e197c_OffConstructer")]
       [SerializationConstructor]
       public ExternalDataTask(): this(null)
{
 
       }
























































































































































































































































}
}
