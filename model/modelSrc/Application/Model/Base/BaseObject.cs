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
public   partial class BaseObject : _baseObject{
       ///<summary>
       ///修改自动触发函数，但在函数体中必须注意不能给同一个类标记monitor的属性赋值；
       ///</summary>
        [UmlElement(Id="7dbbd30a-fdd0-41c0-9e9b-4178e3fb7853")]
        public   void   OnChange()

{
            Tuple<string, string> us = null;
            us = SysFunc.getCurrentUID();
            this.UpdateDt = System.DateTime.Now;
            this.UpdaterID = us==null?"-1": us.Item1;
            this.UpdaterName = us == null ? "admin" : us.Item2;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="74e296ed-9c80-4bb9-9c0c-db3ac4abfb41_OnConstructer")]
       public BaseObject(IContextSpace space): base(space)
{
            this.CreateDt = System.DateTime.Now;
            this.UpdateDt = System.DateTime.Now;
            Tuple<string, string> us = null;
            us = SysFunc.getCurrentUID();
            this.CreaterID = us == null ? "-1" : us.Item1;
            this.CreaterName = us == null ? "admin" : us.Item2;
            this.UpdaterID= this.CreaterID;
            this.UpdaterName = this.CreaterName;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="74e296ed-9c80-4bb9-9c0c-db3ac4abfb41_OffConstructer")]
       [SerializationConstructor]
       public BaseObject(): this(null)
{
            this.CreateDt = System.DateTime.Now;
            this.UpdateDt = System.DateTime.Now;
            Tuple<string, string> us = null;
            us = SysFunc.getCurrentUID();
            this.CreaterID = us == null ? "-1" : us.Item1;
            this.CreaterName = us == null ? "admin" : us.Item2;
            this.UpdaterID = this.CreaterID;
            this.UpdaterName = this.CreaterName;

        }

















































































































































































































































































































































































































}
}
