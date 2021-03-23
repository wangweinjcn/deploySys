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
public   partial class Zone : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="3946047e-01fb-45bc-9c44-9df9c2096abd")]
        public   void   Method1()

{
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="23930703-074f-4c37-993d-90192aafdeeb_OnConstructer")]
       public Zone(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="23930703-074f-4c37-993d-90192aafdeeb_OffConstructer")]
       [SerializationConstructor]
       public Zone(): this(null)
{
 
       }




























































}
}
