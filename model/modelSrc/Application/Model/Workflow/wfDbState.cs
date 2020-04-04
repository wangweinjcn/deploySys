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
public   partial class wfDbState : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="0b17e16c-d52f-46c1-a14f-bc0bfd810d53")]
        public   void   ApplyNewFromWfState(  wfState  st)

{
            this.GuId = st.GuId;
            this.regionName = st.RegionName;
            this.Name = st.name;
            this.fieldName = st.fieldName;
            this.IsRegion = st.isRegionState;
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="bc28a707-8ac0-4b97-b826-5bcb3e97394e_OnConstructer")]
       public wfDbState(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="bc28a707-8ac0-4b97-b826-5bcb3e97394e_OffConstructer")]
       [SerializationConstructor]
       public wfDbState(): this(null)
{
 
       }






























































































































































































































































































































































}
}
