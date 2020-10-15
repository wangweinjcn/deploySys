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
public   partial class AppSuggest : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="49cd216b-34df-4004-8369-c81dfbb4079c_OnConstructer")]
       public AppSuggest(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="49cd216b-34df-4004-8369-c81dfbb4079c_OffConstructer")]
       [SerializationConstructor]
       public AppSuggest(): this(null)
{
 
       }









































































































































}
}
