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
        ///访问历史记录</summary>
public   partial class VisitHistory : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="fc31fd40-9526-43e1-9fb9-4ed5ffebc0af_OnConstructer")]
       public VisitHistory(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="fc31fd40-9526-43e1-9fb9-4ed5ffebc0af_OffConstructer")]
       [SerializationConstructor]
       public VisitHistory(): this(null)
{
 
       }
























































































































































































































































}
}
