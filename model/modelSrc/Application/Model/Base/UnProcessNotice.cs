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
public   partial class UnProcessNotice : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="560416c4-87ca-4ffc-9c6a-3d8cb3ce2a52")]
        public  String  ToMessage()

{
            return null;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="809f9a3c-2a22-45d2-93cc-ab28da16770a")]
        public   void   OnInsert()

{

            SysUser curruser = GetSpace().SpaceQuery<SysUser>().Where(
                a => a.Email == this.NoticeNo || a.Mobile == this.NoticeNo).FirstOrDefault();
            if (curruser == null)
                return;


           MessageInSite mis = new MessageInSite(GetSpace());
            mis.Ass_SysUser = curruser;
            mis.Content = this.Content;
            mis.SmsTmpId = this.smsTmplId;
            mis.TypeKey = this.TypeKey;
            mis.haveRead = false;
            mis.Title = this.Title;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="53d1b807-8826-49bb-8dfd-f7bf7acb2e2c_OnConstructer")]
       public UnProcessNotice(IContextSpace space): base(space)
{
 TypeKey=0;
            this.NoticeNo = Guid.NewGuid().ToString(); 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="53d1b807-8826-49bb-8dfd-f7bf7acb2e2c_OffConstructer")]
       [SerializationConstructor]
       public UnProcessNotice(): this(null)
{
 

       }

















































































































































































































































































































































































































}
}
