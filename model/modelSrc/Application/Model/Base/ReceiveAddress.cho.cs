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
[UmlElement(Id="87364018-263c-448e-9f7c-3710be195d10")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"ReceiveAddress\",   \"Id\": \"87364018-263c-448e-9f7c-3710be195d10\",   \"FullName\": \"Application.Model.Base..ReceiveAddress\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Mobile\",       \"Id\": \"198f2417-6d0f-427c-90bc-f8981959aa9a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"手机号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Address\",       \"Id\": \"45ea5870-ddc1-43cf-970d-7d84e8499c8f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"地址\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"AreaId\",       \"Id\": \"5765383c-c0f8-472c-9a62-32f6cc6b138a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"区县Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Tag\",       \"Id\": \"749b7e10-f9eb-422b-adf7-88b475aba8c1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"收货地址标识\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"PostCode\",       \"Id\": \"84163560-ef70-4d21-8c9d-e88ae2886946\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"邮编\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Receiver\",       \"Id\": \"b3568bb2-3b46-4347-80c8-bce195cac763\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"收货人\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IdentyId\",       \"Id\": \"d27d1431-d52c-4268-910d-a6d5b6892fc5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"身份证，跨境必须，海关申报\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDefault\",       \"Id\": \"edd7633f-8d5c-478b-ac75-0c959b932d66\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class ReceiveAddress: _baseObject {
[UmlElement(Id="ReceiveAddress_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///手机号</summary>
[UmlElement(Id="198f2417-6d0f-427c-90bc-f8981959aa9a")]
[Display(Name="手机号")]
[StringLength(225,ErrorMessage ="超过最大长度")]
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
        ///地址</summary>
[UmlElement(Id="45ea5870-ddc1-43cf-970d-7d84e8499c8f")]
[Display(Name="地址")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Address{
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
        ///区县Id</summary>
[UmlElement(Id="5765383c-c0f8-472c-9a62-32f6cc6b138a")]
[Display(Name="区县Id")]
public Int32?  AreaId{
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
        ///收货地址标识</summary>
[UmlElement(Id="749b7e10-f9eb-422b-adf7-88b475aba8c1")]
[Display(Name="收货地址标识")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Tag{
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
        ///邮编</summary>
[UmlElement(Id="84163560-ef70-4d21-8c9d-e88ae2886946")]
[Display(Name="邮编")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  PostCode{
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
        ///收货人</summary>
[UmlElement(Id="b3568bb2-3b46-4347-80c8-bce195cac763")]
[Display(Name="收货人")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Receiver{
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
        ///身份证，跨境必须，海关申报</summary>
[UmlElement(Id="d27d1431-d52c-4268-910d-a6d5b6892fc5")]
[Display(Name="身份证，跨境必须，海关申报")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  IdentyId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="edd7633f-8d5c-478b-ac75-0c959b932d66")]
[Required]
public Boolean  IsDefault{
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
        ///<summary></summary>
[UmlElement(Id="7aaa055c-4e54-491f-864d-6678a0814ea8")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(SysUser),DbFieldName ="Ass_SysUser", AsscoisDestPropertyName="Ass_ReceiveAddress", AssociaID = "7aaa055c-4e54-491f-864d-6678a0814ea8")]
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
