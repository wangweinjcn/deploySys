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
[UmlElement(Id="a6c64133-e1b0-48a9-a664-966aaeb91734")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"ForwardHistory\",   \"Id\": \"a6c64133-e1b0-48a9-a664-966aaeb91734\",   \"FullName\": \"Application.Model.Base..ForwardHistory\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"ValidCount\",       \"Id\": \"16ab4862-34b6-46b8-973e-1859e64f022d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"计价或积分点击\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Guid\",       \"Id\": \"3254f65e-0205-497f-8775-70de1eb9c877\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"转发编号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UserToken\",       \"Id\": \"6376df23-77d7-4305-8778-091e20636e3f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"用户token；微信中的UserId\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"MediaType\",       \"Id\": \"98c3fe92-4c7e-4bee-81fb-78b3270358ea\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"媒体类型;0:微信\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ParentGuid\",       \"Id\": \"9dd13844-41a4-4daa-9c81-c0b5b9f7f731\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"父级转发编号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Memo\",       \"Id\": \"af8b6519-e671-4045-8d8c-b0cb16f90694\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Count\",       \"Id\": \"cd4ff9a6-41ca-4cf8-b545-2d0c666d0f1e\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"点击次数\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ForwardUrl\",       \"Id\": \"d2cbe47e-95b1-46e8-bf85-97ab90036b58\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class ForwardHistory: _baseObject {
[UmlElement(Id="ForwardHistory_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///计价或积分点击</summary>
[UmlElement(Id="16ab4862-34b6-46b8-973e-1859e64f022d")]
[Display(Name="计价或积分点击")]
[Required]
public Int32  ValidCount{
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
        ///转发编号</summary>
[UmlElement(Id="3254f65e-0205-497f-8775-70de1eb9c877")]
[Display(Name="转发编号")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Guid{
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
        ///用户token；微信中的UserId</summary>
[UmlElement(Id="6376df23-77d7-4305-8778-091e20636e3f")]
[Display(Name="用户token；微信中的UserId")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  UserToken{
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
        ///媒体类型;0:微信</summary>
[UmlElement(Id="98c3fe92-4c7e-4bee-81fb-78b3270358ea")]
[Display(Name="媒体类型;0:微信")]
[Required]
public Int32  MediaType{
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
        ///父级转发编号</summary>
[UmlElement(Id="9dd13844-41a4-4daa-9c81-c0b5b9f7f731")]
[Display(Name="父级转发编号")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  ParentGuid{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="af8b6519-e671-4045-8d8c-b0cb16f90694")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Memo{
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
        ///点击次数</summary>
[UmlElement(Id="cd4ff9a6-41ca-4cf8-b545-2d0c666d0f1e")]
[Display(Name="点击次数")]
[Required]
public Int32  Count{
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
[UmlElement(Id="d2cbe47e-95b1-46e8-bf85-97ab90036b58")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  ForwardUrl{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

}
}
