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
      using   Newtonsoft.Json.Linq;
        ///
        ///<summary></summary>
public   partial class SysUser : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="161d49dd-38a2-45aa-8630-cd7112ed3ded")]
        public   void   SetRole(  SysRole  role)

{

            if (!this.Ass_SysRole.Contains(role))
                this.Ass_SysRole.Add(role);
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="7558da46-8da7-4e66-b172-021f373c46c4")]
        public  LoginHistory  AddNewLoginHistory(  String  ip,  String  token,  DateTime  expdt)

{

            var list=GetSpace().SpaceQuery<LoginHistory>()
                .Where(a => a.valid == true && a.Ass_SysUser == this).Select(a=>a.Id).ToCommList();
            GetSpace().BatchUpdate<LoginHistory>(a =>list.Contains(a.Id)
            , a => new LoginHistory() { valid = false });
            LoginHistory lh = new LoginHistory(GetSpace());
            lh.IpFrom = ip;
            lh.Token = token;
            lh.ExpireDt = expdt;
            lh.Ass_SysUser = this;
            lh.valid = true;
            return lh;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="7d5f4f0b-03e2-47ee-add1-b32a2f118ae5")]
        public  Boolean  IsAdmin()

{

            foreach (var obj in this.Ass_SysRole)
            {
                if (obj.IsAdmin)
                    return true;
            }
            return false;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="a47ba546-6b7f-4304-9c9b-5e223af747a9")]
        public  JArray  GetMyRoles()

{
            JArray jarr = BaseContextSpace.CsObjToJson(this.Ass_SysRole) as JArray;

            return jarr;
 
        }
       ///<summary>
       ///menutype:0 ace菜单；1：vue菜单
       ///</summary>
        [UmlElement(Id="b5508bed-c78d-41a3-8ef4-0246ae49effd")]
        public  JToken  GetMyMenu(  Int32  menutype=0)

{
            JArray jobj =null;
            if (IsAdmin())
            {
                SysFunc sf = SysFunc.getInstance(GetSpace());
                jobj = sf.AllMenu(menutype) as JArray;
            }
            else
            {
                List<Menu> allmenu=new List<Menu>();
                foreach (var robj in this.Ass_SysRole)
                {
                    foreach (var menuobj in robj.Ass_Menu)
                    {
                        if (!allmenu.Contains(menuobj))
                            allmenu.Add(menuobj);
                    }
                     

                }
               

                string oldmgname = "";
                JArray listmenu=new JArray();
               Stack< MenuGroup> mgstack=new Stack<MenuGroup>();
                TreeClass menulist = new TreeClass();
                foreach (var obj in allmenu)
                {
                    MenuGroup mgtemp = obj.Ass_MenuGroup;
                    while (mgtemp != null)
                    {
                        mgstack.Push(mgtemp);
                        mgtemp = mgtemp.Ass_Parent;
                           
                    }
                    mgtemp = mgstack.Pop();
                    treeNode pmg=null,node=null;
                    while (mgtemp != null)
                    {
                        node= menulist.getChildNode(mgtemp.Id.ToString(), null);
                        if (node == null)
                            node = menulist.insertChild(mgtemp.Id.ToString(),mgtemp.SortCode, mgtemp.CsObjToJson(),true, pmg);
                        pmg = node;
                        if (mgstack.Count > 0)
                            mgtemp = mgstack.Pop();
                        else
                            mgtemp = null;
                    }
                    menulist.insertChild("menu_" + obj.Id.ToString(), obj.SortCode, obj.CsObjToJson(),false, node);
                  

                }
                switch (menutype)
                {
                    case 0:
                        jobj = menulist.tojson();
                        break;
                    case 1:
                        jobj = menulist.toVtableJson();
                        break;

                }
            }
            return jobj;
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="a85115fa-5ccf-4197-8fde-cfdf7f4d3f25_OnConstructer")]
       public SysUser(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="a85115fa-5ccf-4197-8fde-cfdf7f4d3f25_OffConstructer")]
       [SerializationConstructor]
       public SysUser(): this(null)
{
 
       }

















































































































































































































































































































































































































}
}
