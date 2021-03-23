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
        ///<summary>
        ///
        ///数据库</summary>
public   partial class Db : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="f4ee6247-a9c2-4c21-bc68-417ed02e0322_OnConstructer")]
       public Db(IContextSpace space): base(space)
{
 Ecoder="UTF8";

       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="f4ee6247-a9c2-4c21-bc68-417ed02e0322_OffConstructer")]
       [SerializationConstructor]
       public Db(): this(null)
{
 
       }







}
}
