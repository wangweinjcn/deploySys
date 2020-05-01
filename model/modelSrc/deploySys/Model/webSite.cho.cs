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
[UmlElement(Id="6e430828-e9ac-4bbe-84f4-410950879489")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"webSite\",   \"Id\": \"6e430828-e9ac-4bbe-84f4-410950879489\",   \"FullName\": \"deploySys.Model..webSite\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"domain\",       \"Id\": \"20000e04-42aa-4ad3-b025-bbabda84de32\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"域名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"isWebService\",       \"Id\": \"78635d8a-8f67-42a9-9782-f78339114746\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"是否是webservice\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"version\",       \"Id\": \"b1f81db8-fa9d-4e02-af82-5d0539710d43\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"版本\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"status\",       \"Id\": \"e7e66e4a-dee6-40d0-b5a0-a723c14d1093\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"0:运行中；102：等待删除\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"siteDirName\",       \"Id\": \"e88a5f4c-c5f3-4c7e-ac37-0936d4a6b955\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"web站点目录\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class webSite: Application.Model.Base.BaseObject {
[UmlElement(Id="webSite_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///域名</summary>
[UmlElement(Id="20000e04-42aa-4ad3-b025-bbabda84de32")]
[Display(Name="域名")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  domain{
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
        ///是否是webservice</summary>
[UmlElement(Id="78635d8a-8f67-42a9-9782-f78339114746")]
[Display(Name="是否是webservice")]
[Required]
public Boolean  isWebService{
get { 
 var pValue= getPropertyValue();
Boolean res;
 if(pValue==null) 
    res= false;
  else
    res= (Boolean)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///版本</summary>
[UmlElement(Id="b1f81db8-fa9d-4e02-af82-5d0539710d43")]
[Display(Name="版本")]
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
        ///<summary>
        ///
        ///0:运行中；102：等待删除</summary>
[UmlElement(Id="e7e66e4a-dee6-40d0-b5a0-a723c14d1093")]
[Display(Name="0:运行中；102：等待删除")]
[Required]
public Int32  status{
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
        ///web站点目录</summary>
[UmlElement(Id="e88a5f4c-c5f3-4c7e-ac37-0936d4a6b955")]
[Display(Name="web站点目录")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  siteDirName{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="7b2f56d6-eff4-4a8c-8174-9139f02301c2")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(MicroServiceApp),DbFieldName ="Ass_MicroServiceApp", AsscoisDestPropertyName="Ass_webSite", AssociaID = "7b2f56d6-eff4-4a8c-8174-9139f02301c2")]
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
[UmlElement(Id="bc33100e-f4fc-4181-bf86-145133600b46")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(DockerInstance),DbFieldName ="Ass_DockerInstance", AsscoisDestPropertyName="Ass_webSite", AssociaID = "bc33100e-f4fc-4181-bf86-145133600b46")]
public  DockerInstance  Ass_DockerInstance{
get { return (DockerInstance) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_DockerInstance")]
public  int? Ass_DockerInstance_Id {
get{  var x= getAssPropertyId("Ass_DockerInstance"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_DockerInstance");
}    }
public bool ShouldSerializeAss_DockerInstance ()
{
return false;
}

}
}
