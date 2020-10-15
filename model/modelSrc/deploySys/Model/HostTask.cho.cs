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
        ///主机发布任务</summary>
[UmlElement(Id="3e05e19a-09d8-410e-897f-80fff5d69cc7")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"HostTask\",   \"Id\": \"3e05e19a-09d8-410e-897f-80fff5d69cc7\",   \"FullName\": \"deploySys.Model..HostTask\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"allocPort\",       \"Id\": \"6bc38701-e62d-4a38-91cf-2f775ffcca6f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"分配的端口号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"taskType\",       \"Id\": \"816377b2-c668-459a-ad69-5d8dd8e853f5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"任务类型，EnumHostTaskType\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"HostId\",       \"Id\": \"9298e273-16bc-4b0d-84d0-0fafa964bc5d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"分配的主机Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"taskParms\",       \"Id\": \"ae6d2441-1d06-41f2-adf2-d223e67b9ff4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"任务的参数；删除站点时，放置站点的site目录名；在更新时，放置特定instanceId，或者websitedirname\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"dockerInanceId\",       \"Id\": \"af452447-a61e-49b7-84d9-d2cc1cd272b1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"运行的实例Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Status\",       \"Id\": \"db83c68c-b358-4748-a0f0-8d57bcc0d056\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"主机任务状态，0：待处理；10:发布中：20：发布完成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ProcessInfo\",       \"Id\": \"e436dcb1-332b-4f7a-81b4-d7c1171e018e\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class HostTask: Application.Model.Base.BaseObject {
[UmlElement(Id="HostTask_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///分配的端口号</summary>
[UmlElement(Id="6bc38701-e62d-4a38-91cf-2f775ffcca6f")]
[Display(Name="分配的端口号")]
[Required]
public Int32  allocPort{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///任务类型，EnumHostTaskType</summary>
[UmlElement(Id="816377b2-c668-459a-ad69-5d8dd8e853f5")]
[Display(Name="任务类型，EnumHostTaskType")]
[Required]
public Int32  taskType{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///分配的主机Id</summary>
[UmlElement(Id="9298e273-16bc-4b0d-84d0-0fafa964bc5d")]
[Display(Name="分配的主机Id")]
[Required]
public Int32  HostId{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///任务的参数；删除站点时，放置站点的site目录名；在更新时，放置特定instanceId，或者websitedirname</summary>
[UmlElement(Id="ae6d2441-1d06-41f2-adf2-d223e67b9ff4")]
[Display(Name="任务的参数；删除站点时，放置站点的site目录名；在更新时，放置特定instanceId，或者websitedirname")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  taskParms{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///运行的实例Id</summary>
[UmlElement(Id="af452447-a61e-49b7-84d9-d2cc1cd272b1")]
[Display(Name="运行的实例Id")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  dockerInanceId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///主机任务状态，0：待处理；10:发布中：20：发布完成</summary>
[UmlElement(Id="db83c68c-b358-4748-a0f0-8d57bcc0d056")]
[Display(Name="主机任务状态，0：待处理；10:发布中：20：发布完成")]
[Required]
public Int32  Status{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="e436dcb1-332b-4f7a-81b4-d7c1171e018e")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  ProcessInfo{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="2df0c254-5ace-44a8-9f13-b8af3877068e")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(ReleaseTask),DbFieldName ="Ass_ReleaseTask", AsscoisDestPropertyName="Ass_HostTask", AssociaID = "2df0c254-5ace-44a8-9f13-b8af3877068e")]
public  ReleaseTask  Ass_ReleaseTask{
get { return (ReleaseTask) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_ReleaseTask")]
public  int? Ass_ReleaseTask_Id {
get{  var x= getAssPropertyId("Ass_ReleaseTask"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_ReleaseTask");
}    }
public bool ShouldSerializeAss_ReleaseTask ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e0352474-e958-4efb-a0a6-ee75c5a64a85")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(appVersion),DbFieldName ="Ass_appVersion", AsscoisDestPropertyName="Ass_HostTask", AssociaID = "e0352474-e958-4efb-a0a6-ee75c5a64a85")]
public  appVersion  Ass_appVersion{
get { return (appVersion) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_appVersion")]
public  int? Ass_appVersion_Id {
get{  var x= getAssPropertyId("Ass_appVersion"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_appVersion");
}    }
public bool ShouldSerializeAss_appVersion ()
{
return false;
}

}
}
