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
        ///<summary></summary>
public   partial class MessageInSite : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="b7f6554d-d8e0-4b7f-ba90-ce8fcb0047c3_OnConstructer")]
       public MessageInSite(IContextSpace space): base(space)
{
 haveRead=false;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="b7f6554d-d8e0-4b7f-ba90-ce8fcb0047c3_OffConstructer")]
       [SerializationConstructor]
       public MessageInSite(): this(null)
{
 
       }














































































































































































































































































}
}
