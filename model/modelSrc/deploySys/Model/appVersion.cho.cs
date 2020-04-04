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
        ///<summary></summary>
[UmlElement(Id="4879b7c1-18c7-43f6-b00d-d074f1f14602")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"appVersion\",   \"Id\": \"4879b7c1-18c7-43f6-b00d-d074f1f14602\",   \"FullName\": \"deploySys.Model..appVersion\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"version\",       \"Id\": \"88ef596c-f1e4-4768-905f-d70b5f03bd71\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class appVersion: Application.Model.Base.BaseObject {
[UmlElement(Id="appVersion_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="88ef596c-f1e4-4768-905f-d70b5f03bd71")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  version{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="549c7a22-6fe4-4125-a6d8-00715a3c3378")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "Ass_ReleaseTask",  AssociaClass = typeof(ReleaseTask), AssociaID = "549c7a22-6fe4-4125-a6d8-00715a3c3378")]
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
[UmlElement(Id="68b573d0-9acc-448f-b120-b63e838c3e2f")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<FileItem>, _baseObjectList<FileItem>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(FileItem), AsscoisDestPropertyName = "Ass_appVersion", AssociaID = "68b573d0-9acc-448f-b120-b63e838c3e2f")]
public  ICSObjectList<FileItem>  Ass_FileItem{
get { return (ICSObjectList<FileItem>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_FileItem ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="776c41e6-f66a-4f66-a2dd-9e967ffc3fde")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(MicroServiceApp),DbFieldName ="Ass_MicroServiceApp", AsscoisDestPropertyName="Ass_appVersion", AssociaID = "776c41e6-f66a-4f66-a2dd-9e967ffc3fde")]
public  MicroServiceApp  Ass_MicroServiceApp{
get { return (MicroServiceApp) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_MicroServiceApp")]
public  int? Ass_MicroServiceApp_Id {
get{  var x= getAssPropertyId("Ass_MicroServiceApp"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_MicroServiceApp");
}    }
public bool ShouldSerializeAss_MicroServiceApp ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e0352474-e958-4efb-a0a6-ee75c5a64a85")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<HostTask>, _baseObjectList<HostTask>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(HostTask), AsscoisDestPropertyName = "Ass_appVersion", AssociaID = "e0352474-e958-4efb-a0a6-ee75c5a64a85")]
public  ICSObjectList<HostTask>  Ass_HostTask{
get { return (ICSObjectList<HostTask>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_HostTask ()
{
return false;
}

}
}
