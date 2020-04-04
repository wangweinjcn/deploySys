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
public   partial class MenuGroup : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="20e9cf48-88b1-4055-9aa3-1ed7a1a8d417")]
        public  Menu  AddMenu(  String  key,  String  name,  String  url)

{
            Menu menu = new Menu(GetSpace());
            menu.Key = key;
            menu.Name = name;
            menu.UrlAddress = url;
            this.Ass_Menu.Add(menu);
            menu.Ass_MenuGroup = this;
            return menu;
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="6bcaaa03-86a4-4371-8fa5-bbac7dace6c2_OnConstructer")]
       public MenuGroup(IContextSpace space): base(space)
{
            this.SortCode = 0;
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="6bcaaa03-86a4-4371-8fa5-bbac7dace6c2_OffConstructer")]
       [SerializationConstructor]
       public MenuGroup(): this(null)
{
            this.SortCode = 0;
        }































































































































































































































































































































































































}
}
