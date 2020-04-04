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
        ///物理存储路径</summary>
public   partial class PhysicalPath : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="2502411f-1694-4c04-8ff1-f3ff822471b9")]
        public  String  getFullPath()

{
            Stack<PhysicalPath> ss = new Stack<PhysicalPath>();
            ss.Push(this);
            PhysicalPath parent = this.Ass_LocalPath_parent;
            while (parent != null)
            {
                ss.Push(parent);
                parent = parent.Ass_LocalPath_parent;
            }
            string path = "";
            parent = ss.Pop();
            path = parent.Name;
            parent = ss.Pop();
            while (parent != null)
            {
                path = System.IO.Path.Combine(path, parent.Name);
                if (ss.Count > 0)
                    parent = ss.Pop();
                else
                    parent = null;

            }
            return path;

        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="1578fe49-51f2-45dc-8b68-6b1f2bb10bd0_OnConstructer")]
       public PhysicalPath(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="1578fe49-51f2-45dc-8b68-6b1f2bb10bd0_OffConstructer")]
       [SerializationConstructor]
       public PhysicalPath(): this(null)
{
 
       }
















































































































}
}
