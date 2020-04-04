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
public   partial class ForwardHistory : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="a6c64133-e1b0-48a9-a664-966aaeb91734_OnConstructer")]
       public ForwardHistory(IContextSpace space): base(space)
{
 ValidCount=0;
MediaType=0;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="a6c64133-e1b0-48a9-a664-966aaeb91734_OffConstructer")]
       [SerializationConstructor]
       public ForwardHistory(): this(null)
{
 
       }























































































































































































































}
}
