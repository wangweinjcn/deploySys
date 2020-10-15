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
        ///数据库server</summary>
public   partial class DbServer : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="fcae1f1c-162b-45fd-aa99-72a7e0f702bc_OnConstructer")]
       public DbServer(IContextSpace space): base(space)
{

            this.dbType = "MySql";
            this.Port = 3306;
            
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="fcae1f1c-162b-45fd-aa99-72a7e0f702bc_OffConstructer")]
       [SerializationConstructor]
       public DbServer(): this(null)
{
 
       }





}
}
