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
public   partial class ReleaseTask : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="c0a063d6-121c-495a-98c5-20acac43202a_OnConstructer")]
       public ReleaseTask(IContextSpace space): base(space)
{
 count=1;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="c0a063d6-121c-495a-98c5-20acac43202a_OffConstructer")]
       [SerializationConstructor]
       public ReleaseTask(): this(null)
{
 
       }




























































}
}
