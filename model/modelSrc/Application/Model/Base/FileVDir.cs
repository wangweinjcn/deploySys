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
      using   Application.Model.Base;
        ///
        ///<summary></summary>
public   partial class FileVDir : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="da75aee0-e094-42a0-839c-8cb8aef7f429")]
        public   void   OnUpdate()

{
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="23b5675e-e322-435a-bb55-baf04311e627_OnConstructer")]
       public FileVDir(IContextSpace space): base(space)
{
 isFile=true;

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="23b5675e-e322-435a-bb55-baf04311e627_OffConstructer")]
       [SerializationConstructor]
       public FileVDir(): this(null)
{
 
       }









































































































































}
}
