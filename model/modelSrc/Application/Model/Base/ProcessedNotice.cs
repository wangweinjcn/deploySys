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
public   partial class ProcessedNotice : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="376d105c-5843-4c1a-86ac-a5f5da29e37f_OnConstructer")]
       public ProcessedNotice(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="376d105c-5843-4c1a-86ac-a5f5da29e37f_OffConstructer")]
       [SerializationConstructor]
       public ProcessedNotice(): this(null)
{
 
       }
       ///<summary>
       ///
       ///</summary>
public  String  ToMessage()       

{
            return null;
 
        }
       ///<summary>
       ///
       ///</summary>
public   void   OnInsert()       

{
 
        }





































































































































































































































































}
}
