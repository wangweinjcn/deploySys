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
public   partial class RegionApps : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="15424766-6d26-40c1-a16e-b01559d90032_OnConstructer")]
       public RegionApps(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="15424766-6d26-40c1-a16e-b01559d90032_OffConstructer")]
       [SerializationConstructor]
       public RegionApps(): this(null)
{
 
       }























































}
}
