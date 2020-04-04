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
[UmlElement(Id="cb3b0475-a736-4c35-b363-71c2d19e3917")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"LoginHistory\",   \"Id\": \"cb3b0475-a736-4c35-b363-71c2d19e3917\",   \"FullName\": \"Application.Model.Base..LoginHistory\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Token\",       \"Id\": \"1798e0c0-b90c-42e1-a592-bcccfd01ced2\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"token值\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ExpireDt\",       \"Id\": \"6d3016cd-2b65-4247-880e-ad1e6c57f539\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"过期时间\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"valid\",       \"Id\": \"6f7e1c0a-343c-4e6f-ab75-f940d704be61\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"有效标记\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IpFrom\",       \"Id\": \"8603f34b-f024-45ec-ad34-3766e31a8526\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"Ip地址\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class LoginHistory: Application.Model.Base.BaseObject {
[UmlElement(Id="LoginHistory_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///token值</summary>
[UmlElement(Id="1798e0c0-b90c-42e1-a592-bcccfd01ced2")]
[Display(Name="token值")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Token{
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
        ///过期时间</summary>
[UmlElement(Id="6d3016cd-2b65-4247-880e-ad1e6c57f539")]
[Display(Name="过期时间")]
[Required]
public DateTime  ExpireDt{
get { 
 var pValue= getPropertyValue();
DateTime res;
 if(pValue==null) 
    res= DateTime.Now;
  else
    res= (DateTime)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///有效标记</summary>
[UmlElement(Id="6f7e1c0a-343c-4e6f-ab75-f940d704be61")]
[Display(Name="有效标记")]
[Required]
public Boolean  valid{
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
        ///Ip地址</summary>
[UmlElement(Id="8603f34b-f024-45ec-ad34-3766e31a8526")]
[Display(Name="Ip地址")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  IpFrom{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="d31e97fa-e443-418b-8c7d-5a50fcc5847b")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(SysUser),DbFieldName ="Ass_SysUser", AsscoisDestPropertyName="Ass_LoginHistory", AssociaID = "d31e97fa-e443-418b-8c7d-5a50fcc5847b")]
public  SysUser  Ass_SysUser{
get { return (SysUser) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_SysUser")]
public  int? Ass_SysUser_Id {
get{  var x= getAssPropertyId("Ass_SysUser"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_SysUser");
}    }
public bool ShouldSerializeAss_SysUser ()
{
return false;
}

}
}
