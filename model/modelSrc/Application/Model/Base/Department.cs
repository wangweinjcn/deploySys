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
        ///内部OSS账号的归属部门</summary>
public   partial class Department : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="a0afe9b6-4b39-4fe9-8e69-7ce27a430ec8")]
        public   void   AddUser(  SysUser  user)

{
            var x = GetSpace().SpaceQuery<AssC_SysUserDepartment>().Where(a => a.AssC_SysUser == user).FirstOrDefault();
            if (x == null)
            {
                this.Ass_SysUser.Add(user);
            }
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="904817ce-d9d6-4ab4-a89d-13fc50072a83_OnConstructer")]
       public Department(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="904817ce-d9d6-4ab4-a89d-13fc50072a83_OffConstructer")]
       [SerializationConstructor]
       public Department(): this(null)
{
 
       }
























































































































































































































































































































































































































}
}
