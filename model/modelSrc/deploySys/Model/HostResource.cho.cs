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
      using   FrmLib.Extend;
        ///
        ///<summary></summary>
[UmlElement(Id="beebf626-a267-4ca7-9b56-e619cad7bae0")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"HostResource\",   \"Id\": \"beebf626-a267-4ca7-9b56-e619cad7bae0\",   \"FullName\": \"deploySys.Model..HostResource\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"IP\",       \"Id\": \"169621df-01d6-45b8-8b66-09b952239e8d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"局域网IP\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"lastRegisteDt\",       \"Id\": \"25df237e-428d-4e71-b96e-723142eaeaf1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"clientSessionId\",       \"Id\": \"2d6a1ebf-94e3-4e5c-8ada-bcea0d6a6fb1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"allocFromPort\",       \"Id\": \"3f46b7cf-db10-4c85-9992-3c3549b48330\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"WIP\",       \"Id\": \"4033e1fc-1419-4dd9-9d43-e4bb13764352\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"广域网地址\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"cpuCoreNumber\",       \"Id\": \"43babcb9-7c60-4f81-9976-ee86e111521e\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"canAutoAssign\",       \"Id\": \"45cdd533-01fb-406a-b9db-9999bac8f6ca\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"是否可以被自动分配（不参与自动分配）\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"nodePort\",       \"Id\": \"498d9140-259f-499d-ad4d-f8c7c2d200a7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"节点端口\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"nodeId\",       \"Id\": \"5bec0352-9d69-4845-874b-f84b714d2a39\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"节点Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"inLine\",       \"Id\": \"80bd99f5-9da9-4a61-be57-330bf69f0a87\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 5,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"allocEndPort\",       \"Id\": \"94823cf0-891f-4982-b088-23e0b57360fd\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 6,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"haveNginx\",       \"Id\": \"94bb77eb-2ff0-4311-86e1-ae514a638cab\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 6,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"aliveDt\",       \"Id\": \"ba473913-292f-4c2a-aeff-2f79ddbc2999\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 7,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"nginxIsRun\",       \"Id\": \"ccc2a7ca-3bbd-46ea-8b3c-d9693f82e65d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 7,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"OsName\",       \"Id\": \"cdfc687e-a062-4296-a674-20a6d988e618\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 8,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"操作系统名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"appBaseDir\",       \"Id\": \"d5bb3156-e0c6-4470-b601-fa6efb8ce29b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 8,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"docker 自动部署根目录\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"nginxConfPath\",       \"Id\": \"d774af75-3356-4283-aed3-d18032eeeabe\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 9,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"hostName\",       \"Id\": \"d8223486-4d23-4a7d-9e3f-7faad70cf0d3\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 9,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"macId\",       \"Id\": \"e185e7ef-5c6b-42bc-89c1-b19bd129dff0\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 10,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"硬件macId\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"cpuName\",       \"Id\": \"e2481224-080d-4e34-8658-50d3c6d67140\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 10,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"nginxInstanceId\",       \"Id\": \"e7484349-878e-48a9-9e62-e1f1e762efe9\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 11,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class HostResource: Application.Model.Base.BaseObject {
[UmlElement(Id="HostResource_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///局域网IP</summary>
[UmlElement(Id="169621df-01d6-45b8-8b66-09b952239e8d")]
[Display(Name="局域网IP")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  IP{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="25df237e-428d-4e71-b96e-723142eaeaf1")]
[NotMapped]
public DateTime?  lastRegisteDt{
get { 
 var pValue= getPropertyValue();
DateTime? res;
res= (DateTime?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="2d6a1ebf-94e3-4e5c-8ada-bcea0d6a6fb1")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  clientSessionId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="3f46b7cf-db10-4c85-9992-3c3549b48330")]
[Required]
public Int32  allocFromPort{
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
        ///广域网地址</summary>
[UmlElement(Id="4033e1fc-1419-4dd9-9d43-e4bb13764352")]
[Display(Name="广域网地址")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  WIP{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="43babcb9-7c60-4f81-9976-ee86e111521e")]
public Int32?  cpuCoreNumber{
get { 
 var pValue= getPropertyValue();
Int32? res;
res= (Int32?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///是否可以被自动分配（不参与自动分配）</summary>
[UmlElement(Id="45cdd533-01fb-406a-b9db-9999bac8f6ca")]
[Display(Name="是否可以被自动分配（不参与自动分配）")]
[Required]
public Boolean  canAutoAssign{
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
        ///节点端口</summary>
[UmlElement(Id="498d9140-259f-499d-ad4d-f8c7c2d200a7")]
[Display(Name="节点端口")]
[Required]
public Int32  nodePort{
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
        ///节点Id</summary>
[UmlElement(Id="5bec0352-9d69-4845-874b-f84b714d2a39")]
[Display(Name="节点Id")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  nodeId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="80bd99f5-9da9-4a61-be57-330bf69f0a87")]
[NotMapped]
public Boolean?  inLine{
get { 
 var pValue= getPropertyValue();
Boolean? res;
res= (Boolean?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="94823cf0-891f-4982-b088-23e0b57360fd")]
[Required]
public Int32  allocEndPort{
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
[UmlElement(Id="94bb77eb-2ff0-4311-86e1-ae514a638cab")]
public Boolean?  haveNginx{
get { 
 var pValue= getPropertyValue();
Boolean? res;
res= (Boolean?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="ba473913-292f-4c2a-aeff-2f79ddbc2999")]
[NotMapped]
public DateTime?  aliveDt{
get { 
 var pValue= getPropertyValue();
DateTime? res;
res= (DateTime?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="ccc2a7ca-3bbd-46ea-8b3c-d9693f82e65d")]
public Boolean?  nginxIsRun{
get { 
 var pValue= getPropertyValue();
Boolean? res;
res= (Boolean?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///操作系统名称</summary>
[UmlElement(Id="cdfc687e-a062-4296-a674-20a6d988e618")]
[Display(Name="操作系统名称")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  OsName{
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
        ///docker 自动部署根目录</summary>
[UmlElement(Id="d5bb3156-e0c6-4470-b601-fa6efb8ce29b")]
[Display(Name="docker 自动部署根目录")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  appBaseDir{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="d774af75-3356-4283-aed3-d18032eeeabe")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  nginxConfPath{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="d8223486-4d23-4a7d-9e3f-7faad70cf0d3")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  hostName{
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
        ///硬件macId</summary>
[UmlElement(Id="e185e7ef-5c6b-42bc-89c1-b19bd129dff0")]
[Display(Name="硬件macId")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  macId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="e2481224-080d-4e34-8658-50d3c6d67140")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  cpuName{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="e7484349-878e-48a9-9e62-e1f1e762efe9")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  nginxInstanceId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="765b3fb9-1e47-467f-ada8-b4efc51ef126")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Zone),DbFieldName ="Ass_Zone", AsscoisDestPropertyName="Ass_HostResource", AssociaID = "765b3fb9-1e47-467f-ada8-b4efc51ef126")]
public  Zone  Ass_Zone{
get { return (Zone) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Zone")]
public  int? Ass_Zone_Id {
get{  var x= getAssPropertyId("Ass_Zone"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Zone");
}    }
public bool ShouldSerializeAss_Zone ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="eb3605f1-51ff-48ff-885a-ead08b46166a")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<DockerInstance>, _baseObjectList<DockerInstance>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(DockerInstance), AsscoisDestPropertyName = "Ass_HostResource", AssociaID = "eb3605f1-51ff-48ff-885a-ead08b46166a")]
public  ICSObjectList<DockerInstance>  Ass_DockerInstance{
get { return (ICSObjectList<DockerInstance>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_DockerInstance ()
{
return false;
}

}
}
