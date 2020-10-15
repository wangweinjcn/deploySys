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
public   partial class HistoryBaseObject : Application.Model.Base.BaseObject{
       ///<summary>
       ///修改自动触发函数，但在函数体中必须注意不能给同一个类标记monitor的属性赋值；
       ///</summary>
        [UmlElement(Id="59dc7cb1-06ad-42cb-ad84-7a1988306cb2")]
        protected  virtual   void   OnChange()

{
            try
            { 
            ChangeHistory ch = new ChangeHistory(GetSpace());
            ch.ObjID = this.Id;
            ch.TypeName = this.GetType().Name;
            ch.ChangeDt = DateTime.Now;
            Tuple<string, string> uinfor  = SysFunc.getCurrentUID();
                ch.Changer = uinfor == null ? "" : uinfor.Item1;
            ch.ChangerName = uinfor == null ? "" : uinfor.Item2;
            if (this.isNew())
                ch.ChangeType = "Add";
            else
                ch.ChangeType = "Modify";
            ch.OldJson = this.CsObjToJson().ToString();
        }
            catch (Exception e)
            {
                throw new Exception("HistoryModifyEvent exception:" + e.Message);
            }

        }
       ///<summary>
       ///修改自动触发函数，但在函数体中必须注意不能给同一个类标记monitor的属性赋值；
       ///</summary>
        [UmlElement(Id="d15b680e-3617-4b0d-a9a7-b99c25bf9a03")]
        protected  virtual   void   OnDelete()

{
            try
            {
                ChangeHistory ch = new ChangeHistory(GetSpace());
                ch.ObjID = this.Id;
                ch.TypeName = this.GetType().Name;
                ch.ChangeDt = DateTime.Now;
                Tuple<string, string> uinfor  = SysFunc.getCurrentUID();
                ch.Changer = uinfor == null ? "" : uinfor.Item1;
                ch.ChangerName = uinfor == null ? "" : uinfor.Item2;
                ch.ChangeType = "delete";
                ch.OldJson = this.CsObjToJson(this, null, false, true).ToString();
            }
            catch (Exception e)
            {
                throw new Exception("HistoryDeleteEvent exception:" + e.Message);
            }
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="c048645c-0470-44ad-be2c-7d99ce09b7ea_OnConstructer")]
       public HistoryBaseObject(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="c048645c-0470-44ad-be2c-7d99ce09b7ea_OffConstructer")]
       [SerializationConstructor]
       public HistoryBaseObject(): this(null)
{
 
       }
























































































































































































































































































































































































































}
}
