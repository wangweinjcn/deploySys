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
      using   FrmLib.Encrypt;
      using   FrmLib.Extend;
      using   Newtonsoft.Json.Linq;
        ///
        ///<summary></summary>
public   partial class SysRole : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="1d035e8e-2250-4864-8758-fee9be9d4fa6")]
        public  SysUser  AddUser(  String  loginid,  String  mobile,  String  name,  String  password,  String  email,  Int32  sex,  Int32  logintype,  String  otherLoginId,  String  siteKey)

{
            SysFunc sf = SysFunc.getInstance(GetSpace());
            SysUser user = sf.AddUser(loginid, mobile, name, password, email, sex, logintype, otherLoginId,siteKey);
            this.Ass_SysUser.Add(user);
            return user;

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="62fc6750-73b3-4894-8b1a-d9fc76f4eedd")]
        public   void   AddMenu(  Menu  menu)

{
            this.Ass_Menu.Add(menu);
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="043a8aef-a5cd-4326-a15c-410d0a26f242_OnConstructer")]
       public SysRole(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="043a8aef-a5cd-4326-a15c-410d0a26f242_OffConstructer")]
       [SerializationConstructor]
       public SysRole(): this(null)
{
 
       }

















































































































































































































































































































































































































}
}
