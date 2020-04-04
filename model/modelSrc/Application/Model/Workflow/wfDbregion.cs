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
namespace Application.Model.Workflow
{
      using   Chloe.Ext.stateMachine;
        ///
        ///<summary></summary>
public   partial class wfDbregion : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="6eca3944-8f9a-4617-81bf-874df6a4ec56_OnConstructer")]
       public wfDbregion(IContextSpace space): base(space)
{
 isroot=false;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="6eca3944-8f9a-4617-81bf-874df6a4ec56_OffConstructer")]
       [SerializationConstructor]
       public wfDbregion(): this(null)
{
 
       }

























































































































































































































































































}
}
