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
public   partial class Region : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="3edb18bc-1038-4833-b6d4-77caa3a070ea_OnConstructer")]
       public Region(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="3edb18bc-1038-4833-b6d4-77caa3a070ea_OffConstructer")]
       [SerializationConstructor]
       public Region(): this(null)
{
 
       }


























































}
}
