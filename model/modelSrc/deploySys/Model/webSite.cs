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
        ///<summary></summary>
public   partial class webSite : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="6e430828-e9ac-4bbe-84f4-410950879489_OnConstructer")]
       public webSite(IContextSpace space): base(space)
{
 isWebService=false;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="6e430828-e9ac-4bbe-84f4-410950879489_OffConstructer")]
       [SerializationConstructor]
       public webSite(): this(null)
{
 
       }















}
}
