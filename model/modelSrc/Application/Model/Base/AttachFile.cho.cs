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
[UmlElement(Id="e3d42b25-7a5a-4d61-8eb8-dbc78e373ce0")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"AttachFile\",   \"Id\": \"e3d42b25-7a5a-4d61-8eb8-dbc78e373ce0\",   \"FullName\": \"Application.Model.Base..AttachFile\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Memo\",       \"Id\": \"08093b6c-c360-460a-83bd-9aa16e831442\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"FileUrl\",       \"Id\": \"376ede9b-9036-4f5a-9e24-ca08d095d316\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"（保留）\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ClassName\",       \"Id\": \"444842fe-7f30-4f48-bf82-c20a03641a3f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"FileName\",       \"Id\": \"5c486691-eb27-46bd-a793-d5f3f6607f00\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"AttachId\",       \"Id\": \"5c4ced33-fcb8-4012-8ab7-3302b21c41c6\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"附件存储Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ClassId\",       \"Id\": \"bff1e62b-ed2a-4002-aca1-dea774fabcd7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class AttachFile: _baseObject {
[UmlElement(Id="AttachFile_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="08093b6c-c360-460a-83bd-9aa16e831442")]
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
        ///（保留）</summary>
[UmlElement(Id="376ede9b-9036-4f5a-9e24-ca08d095d316")]
[Display(Name="（保留）")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  FileUrl{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="444842fe-7f30-4f48-bf82-c20a03641a3f")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  ClassName{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="5c486691-eb27-46bd-a793-d5f3f6607f00")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  FileName{
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
        ///附件存储Id</summary>
[UmlElement(Id="5c4ced33-fcb8-4012-8ab7-3302b21c41c6")]
[Display(Name="附件存储Id")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  AttachId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="bff1e62b-ed2a-4002-aca1-dea774fabcd7")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  ClassId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

}
}
