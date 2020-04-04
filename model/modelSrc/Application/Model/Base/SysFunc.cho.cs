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
      using   FrmLib.Extend;
        ///
        ///<summary></summary>
[UmlElement(Id="33abb285-b98a-47ef-a6c1-6c9ea8f475dc")]
[NotMapped]
[ClassViewConf("{   \"Name\": \"SysFunc\",   \"Id\": \"33abb285-b98a-47ef-a6c1-6c9ea8f475dc\",   \"FullName\": \"Application.Model.Base..SysFunc\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [] }")]
[MessagePackObject(true)]
public   partial class SysFunc: _baseObject {
[UmlElement(Id="SysFunc_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
private static Object _lockObj=new Object();
}
}
