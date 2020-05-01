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
        ///区县</summary>
public   partial class Areas : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="f306166b-3a27-4efe-b7d1-62812e4deedc_OnConstructer")]
       public Areas(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="f306166b-3a27-4efe-b7d1-62812e4deedc_OffConstructer")]
       [SerializationConstructor]
       public Areas(): this(null)
{
 
       }

































































































































































































































































}
}
