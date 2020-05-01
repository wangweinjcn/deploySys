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
      using   FrmLib.Extend;
        ///
        ///<summary></summary>
public   partial class BaseObjectWithPinyin : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="69df8b5b-333b-4756-a383-a32cc818ebc1_OnConstructer")]
       public BaseObjectWithPinyin(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="69df8b5b-333b-4756-a383-a32cc818ebc1_OffConstructer")]
       [SerializationConstructor]
       public BaseObjectWithPinyin(): this(null)
{
 
       }
        partial void NameCnWrite(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

           
        }

















































































































































































































































































































































































































}
}
