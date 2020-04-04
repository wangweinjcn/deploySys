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
[UmlElement(Id="1076bde0-9eca-4111-a6b2-bb72d6b5e3ca")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"CustField\",   \"Id\": \"1076bde0-9eca-4111-a6b2-bb72d6b5e3ca\",   \"FullName\": \"Application.Model.Base..CustField\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Key\",       \"Id\": \"9f67873d-622d-4098-acad-0f0a81c2a2dd\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Name\",       \"Id\": \"a7bc1969-bbe4-4cda-b569-02fc5c535b92\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Memo\",       \"Id\": \"d25ab87c-e3ae-4596-81b7-f4d2416b8612\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"_type\",       \"Id\": \"fa9099d5-ef0c-4a7c-b9b6-af9a6448f151\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class CustField: _baseObject {
[UmlElement(Id="CustField_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="9f67873d-622d-4098-acad-0f0a81c2a2dd")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Key{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="a7bc1969-bbe4-4cda-b569-02fc5c535b92")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Name{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="d25ab87c-e3ae-4596-81b7-f4d2416b8612")]
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
        ///<summary></summary>
[UmlElement(Id="fa9099d5-ef0c-4a7c-b9b6-af9a6448f151")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  _type{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="4c1d2db4-97b4-4ba6-8d55-cc8929119c24")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "Ass_CustFieldValue",  AssociaClass = typeof(CustFieldValue), AssociaID = "4c1d2db4-97b4-4ba6-8d55-cc8929119c24")]
public  CustFieldValue  Ass_CustFieldValue{
get { return (CustFieldValue) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_CustFieldValue")]
public  int? Ass_CustFieldValue_Id {
get{  var x= getAssPropertyId("Ass_CustFieldValue"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_CustFieldValue");
}    }
public bool ShouldSerializeAss_CustFieldValue ()
{
return false;
}

}
}
