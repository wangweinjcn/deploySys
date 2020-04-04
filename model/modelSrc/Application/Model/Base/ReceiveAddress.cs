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
public   partial class ReceiveAddress : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="87364018-263c-448e-9f7c-3710be195d10_OnConstructer")]
       public ReceiveAddress(IContextSpace space): base(space)
{
 IsDefault=false;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="87364018-263c-448e-9f7c-3710be195d10_OffConstructer")]
       [SerializationConstructor]
       public ReceiveAddress(): this(null)
{
 
       }

















































































































}
}
