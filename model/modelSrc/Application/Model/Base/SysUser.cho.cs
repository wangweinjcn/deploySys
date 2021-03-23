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
[UmlElement(Id="a85115fa-5ccf-4197-8fde-cfdf7f4d3f25")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"SysUser\",   \"Id\": \"a85115fa-5ccf-4197-8fde-cfdf7f4d3f25\",   \"FullName\": \"Application.Model.Base..SysUser\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Email\",       \"Id\": \"0ac2e81b-0e1d-40d3-b4cc-b82974daf73c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[EmailAddress(ErrorMessage =\"电子邮箱格式错误\")]\",       \"DisplayTag\": \"电子邮件\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UserName\",       \"Id\": \"3ac8c883-9a89-4b2d-bab0-2720e207e3e2\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"用户姓名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Mobile\",       \"Id\": \"6047377c-088c-4921-9284-4d995beea09b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"手机号码\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Password\",       \"Id\": \"69e16c6e-1dd3-4154-bb44-03777ba3e0ef\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[JsonIgnore]\",       \"DisplayTag\": \"密码（加密）\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsEnabled\",       \"Id\": \"6bcd9db5-a52d-427d-8744-64a71e190bb1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"是否启用\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UserType\",       \"Id\": \"772ec9d8-fe52-46ff-bd08-9eb1d03fd91c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"用户类型;0:系统用户；1：后台管理账号；2：客户\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"RecomendId\",       \"Id\": \"7ca054f3-ebf8-470c-95d7-190e6806efd4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"推荐人Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Birthday\",       \"Id\": \"80ddab6e-e820-4cb3-aa41-945fb11052ad\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"生日\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IdentityId\",       \"Id\": \"9938d600-de9a-45d0-a093-b95e01131f65\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"身份证号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Photo\",       \"Id\": \"a23bd072-b073-43dd-ad5e-94dc34b64a11\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"个人头像连接\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"TelNo\",       \"Id\": \"a461304b-ef01-49d2-a1f7-25b31a6a4b24\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 6,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ComName\",       \"Id\": \"a784326d-cfd2-4681-84e7-18a9a1aec8c9\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 6,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"公司信息\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"loginType\",       \"Id\": \"bb784e7d-9bc2-4e7d-bb8a-38002c1146a0\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 7,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[Range(0,3,ErrorMessage =\"取值范围错误\")]\",       \"DisplayTag\": \"登录类型，0：本地账户；1：微信；2：QQ，\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"AppSiteKey\",       \"Id\": \"c60824fb-f849-46c7-bac7-26356c6364ef\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 7,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"登录子系统key值\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"secretKey\",       \"Id\": \"c981b950-f4cf-40e1-8f43-9a6888484f48\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 8,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[JsonIgnore]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Description\",       \"Id\": \"d5e0bdb8-95ce-4667-9740-94657849cedc\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 8,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"描述\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"LoginId\",       \"Id\": \"d99e433c-89b6-4e2d-9dee-cc356c6a63c0\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 9,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"登录名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"sex\",       \"Id\": \"f5f23c88-3f5f-4a6d-8283-60220e638b5f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 9,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"0:女，1：男；-1未知\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"OtherLoginId\",       \"Id\": \"fd5ce21a-ddfc-4c81-a293-7f4fb877a4ad\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 10,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"其他登录ID\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class SysUser: Application.Model.Base.BaseObject {
[UmlElement(Id="SysUser_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///电子邮件</summary>
[UmlElement(Id="0ac2e81b-0e1d-40d3-b4cc-b82974daf73c")]
[Display(Name="电子邮件")]
[StringLength(100,ErrorMessage ="长度错误",MinimumLength =6)]
[EmailAddress(ErrorMessage ="电子邮箱格式错误")]
public String  Email{
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
        ///用户姓名</summary>
[UmlElement(Id="3ac8c883-9a89-4b2d-bab0-2720e207e3e2")]
[Display(Name="用户姓名")]
[StringLength(100,ErrorMessage ="超过最大长度")]
[Required]
public String  UserName{
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
        ///手机号码</summary>
[UmlElement(Id="6047377c-088c-4921-9284-4d995beea09b")]
[Display(Name="手机号码")]
[Required]
[Phone(ErrorMessage ="电话号码格式错误")]
[StringLength(12,ErrorMessage ="超过最大长度")]
public String  Mobile{
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
        ///密码（加密）</summary>
[UmlElement(Id="69e16c6e-1dd3-4154-bb44-03777ba3e0ef")]
[BindNever]
[NotApplyFromOffice]
[Display(Name="密码（加密）")]
[JsonIgnore]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Password{
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
        ///是否启用</summary>
[UmlElement(Id="6bcd9db5-a52d-427d-8744-64a71e190bb1")]
[Display(Name="是否启用")]
[Required]
public Boolean  IsEnabled{
get { 
 var pValue= getPropertyValue();
Boolean res;
 if(pValue==null) 
    res= true;
  else
    res= (Boolean)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///用户类型;0:系统用户；1：后台管理账号；2：客户</summary>
[UmlElement(Id="772ec9d8-fe52-46ff-bd08-9eb1d03fd91c")]
[Display(Name="用户类型;0:系统用户；1：后台管理账号；2：客户")]
[Required]
public Int32  UserType{
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
        ///推荐人Id</summary>
[UmlElement(Id="7ca054f3-ebf8-470c-95d7-190e6806efd4")]
[Display(Name="推荐人Id")]
public Int32?  RecomendId{
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
        ///生日</summary>
[UmlElement(Id="80ddab6e-e820-4cb3-aa41-945fb11052ad")]
[Display(Name="生日")]
public DateTime?  Birthday{
get { 
 var pValue= getPropertyValue();
DateTime? res;
res= (DateTime?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///身份证号</summary>
[UmlElement(Id="9938d600-de9a-45d0-a093-b95e01131f65")]
[Display(Name="身份证号")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  IdentityId{
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
        ///个人头像连接</summary>
[UmlElement(Id="a23bd072-b073-43dd-ad5e-94dc34b64a11")]
[Display(Name="个人头像连接")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Photo{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="a461304b-ef01-49d2-a1f7-25b31a6a4b24")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  TelNo{
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
        ///公司信息</summary>
[UmlElement(Id="a784326d-cfd2-4681-84e7-18a9a1aec8c9")]
[Display(Name="公司信息")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  ComName{
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
        ///登录类型，0：本地账户；1：微信；2：QQ，</summary>
[UmlElement(Id="bb784e7d-9bc2-4e7d-bb8a-38002c1146a0")]
[Display(Name="登录类型，0：本地账户；1：微信；2：QQ，")]
[Range(0,3,ErrorMessage ="取值范围错误")]
[Required]
public Int32  loginType{
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
        ///登录子系统key值</summary>
[UmlElement(Id="c60824fb-f849-46c7-bac7-26356c6364ef")]
[Display(Name="登录子系统key值")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  AppSiteKey{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="c981b950-f4cf-40e1-8f43-9a6888484f48")]
[BindNever]
[NotApplyFromOffice]
[JsonIgnore]
[StringLength(100,ErrorMessage ="超过最大长度")]
public String  secretKey{
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
        ///描述</summary>
[UmlElement(Id="d5e0bdb8-95ce-4667-9740-94657849cedc")]
[Display(Name="描述")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Description{
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
        ///登录名</summary>
[UmlElement(Id="d99e433c-89b6-4e2d-9dee-cc356c6a63c0")]
[Display(Name="登录名")]
[StringLength(20,ErrorMessage ="长度错误",MinimumLength =2)]
[RegularExpression(@"^[0-9a-zA-z_]{2,20}$", ErrorMessage = "格式错误")]
[Required]
public String  LoginId{
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
        ///0:女，1：男；-1未知</summary>
[UmlElement(Id="f5f23c88-3f5f-4a6d-8283-60220e638b5f")]
[Display(Name="0:女，1：男；-1未知")]
[Range(-1,3,ErrorMessage ="取值范围错误")]
[Required]
public Int32  sex{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= -1;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///其他登录ID</summary>
[UmlElement(Id="fd5ce21a-ddfc-4c81-a293-7f4fb877a4ad")]
[Display(Name="其他登录ID")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  OtherLoginId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="1ebef277-56d4-4f94-9344-684d4cec410a")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<Department>, _baseObjectList<Department>>))]
[AssociaField( RelationType =enum_relationType.moreToMore ,AssociaClass = typeof(AssC_SysUserDepartment), AsscoisSourcePropertyName = "AssC_SysUser",AsscoisDestPropertyName = "AssC_Department", AssociaID = "1ebef277-56d4-4f94-9344-684d4cec410a")]
public  ICSObjectList<Department>  Ass_Department{
get { return (ICSObjectList<Department>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_Department ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="58c16685-7e6b-443b-80fc-a4f1a92305ec")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Areas),DbFieldName ="Ass_Areas", AsscoisDestPropertyName="Ass_SysUser", AssociaID = "58c16685-7e6b-443b-80fc-a4f1a92305ec")]
public  Areas  Ass_Areas{
get { return (Areas) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Areas")]
public  int? Ass_Areas_Id {
get{  var x= getAssPropertyId("Ass_Areas"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Areas");
}    }
public bool ShouldSerializeAss_Areas ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="7aaa055c-4e54-491f-864d-6678a0814ea8")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<ReceiveAddress>, _baseObjectList<ReceiveAddress>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(ReceiveAddress), AsscoisDestPropertyName = "Ass_SysUser", AssociaID = "7aaa055c-4e54-491f-864d-6678a0814ea8")]
public  ICSObjectList<ReceiveAddress>  Ass_ReceiveAddress{
get { return (ICSObjectList<ReceiveAddress>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_ReceiveAddress ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="8a9934cf-26fe-44e9-953c-ae52c108d2f5")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<MessageInSite>, _baseObjectList<MessageInSite>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(MessageInSite), AsscoisDestPropertyName = "Ass_SysUser", AssociaID = "8a9934cf-26fe-44e9-953c-ae52c108d2f5")]
public  ICSObjectList<MessageInSite>  Ass_MessageInSite{
get { return (ICSObjectList<MessageInSite>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_MessageInSite ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="d31e97fa-e443-418b-8c7d-5a50fcc5847b")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<LoginHistory>, _baseObjectList<LoginHistory>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(LoginHistory), AsscoisDestPropertyName = "Ass_SysUser", AssociaID = "d31e97fa-e443-418b-8c7d-5a50fcc5847b")]
public  ICSObjectList<LoginHistory>  Ass_LoginHistory{
get { return (ICSObjectList<LoginHistory>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_LoginHistory ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="ea16d933-6c20-438a-9ddb-914af434c678")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<SysRole>, _baseObjectList<SysRole>>))]
[AssociaField( RelationType =enum_relationType.moreToMore ,AssociaClass = typeof(AssC_SysRoleSysUser), AsscoisSourcePropertyName = "AssC_SysUser",AsscoisDestPropertyName = "AssC_SysRole", AssociaID = "ea16d933-6c20-438a-9ddb-914af434c678")]
public  ICSObjectList<SysRole>  Ass_SysRole{
get { return (ICSObjectList<SysRole>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_SysRole ()
{
return false;
}

}
}
