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
        ///<summary>
        ///
        ///城市</summary>
[UmlElement(Id="c5fb692d-20ad-4fae-adba-944b45a5b23a")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"Citys\",   \"Id\": \"c5fb692d-20ad-4fae-adba-944b45a5b23a\",   \"FullName\": \"Application.Model.Base..Citys\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"cityEn\",       \"Id\": \"1565e3cd-222b-4b7a-bd80-4d3e9d134b31\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"城市名称英文\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"provinceKey\",       \"Id\": \"263d1bac-cbe5-48cd-996d-91f18c463823\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"城市归属省份Key\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CountryKey\",       \"Id\": \"3117903d-d1ca-4186-81de-dbf745cc6626\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"cityKey\",       \"Id\": \"37a0a17e-0411-4abd-8780-b55b9cfe895c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"城市Key\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Function\",       \"Id\": \"40b50021-d9d9-4fca-b225-9ba0938f40c3\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Status\",       \"Id\": \"993ad9fd-2538-4cbb-9f46-11337257ab6f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"DateFormate\",       \"Id\": \"b9dd3391-48c6-4db1-a977-c7ade3de79e5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Coordinates\",       \"Id\": \"bf7d1fc7-96cc-4925-b39b-a1166d8ae4d5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"city\",       \"Id\": \"c059f403-3286-40db-8da9-eb21184b1656\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"城市\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Location\",       \"Id\": \"f1af62b1-30ce-4bda-b0c2-a003110270e5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class Citys: _baseObject {
[UmlElement(Id="Citys_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///城市名称英文</summary>
[UmlElement(Id="1565e3cd-222b-4b7a-bd80-4d3e9d134b31")]
[Display(Name="城市名称英文")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  cityEn{
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
        ///城市归属省份Key</summary>
[UmlElement(Id="263d1bac-cbe5-48cd-996d-91f18c463823")]
[Display(Name="城市归属省份Key")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  provinceKey{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="3117903d-d1ca-4186-81de-dbf745cc6626")]
[StringLength(2,ErrorMessage ="超过最大长度")]
public String  CountryKey{
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
        ///城市Key</summary>
[UmlElement(Id="37a0a17e-0411-4abd-8780-b55b9cfe895c")]
[Display(Name="城市Key")]
[Required]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  cityKey{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="40b50021-d9d9-4fca-b225-9ba0938f40c3")]
[StringLength(16,ErrorMessage ="超过最大长度")]
public String  Function{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="993ad9fd-2538-4cbb-9f46-11337257ab6f")]
[StringLength(2,ErrorMessage ="超过最大长度")]
public String  Status{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="b9dd3391-48c6-4db1-a977-c7ade3de79e5")]
[StringLength(4,ErrorMessage ="超过最大长度")]
public String  DateFormate{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="bf7d1fc7-96cc-4925-b39b-a1166d8ae4d5")]
[StringLength(20,ErrorMessage ="超过最大长度")]
public String  Coordinates{
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
        ///城市</summary>
[UmlElement(Id="c059f403-3286-40db-8da9-eb21184b1656")]
[Display(Name="城市")]
[Required]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  city{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="f1af62b1-30ce-4bda-b0c2-a003110270e5")]
[StringLength(5,ErrorMessage ="超过最大长度")]
public String  Location{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="76bddabe-e4fe-471d-9245-1ed38f2a1fbb")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<Areas>, _baseObjectList<Areas>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(Areas), AsscoisDestPropertyName = "Ass_Citys", AssociaID = "76bddabe-e4fe-471d-9245-1ed38f2a1fbb")]
public  ICSObjectList<Areas>  Ass_Areas{
get { return (ICSObjectList<Areas>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_Areas ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="df9b4ed1-82b5-4894-8db6-2d17d1ba9e16")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Provinces),DbFieldName ="Ass_Provinces", AsscoisDestPropertyName="Ass_Citys", AssociaID = "df9b4ed1-82b5-4894-8db6-2d17d1ba9e16")]
public  Provinces  Ass_Provinces{
get { return (Provinces) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Provinces")]
public  int Ass_Provinces_Id {
get{  var x= getAssPropertyId("Ass_Provinces"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Provinces");
}    }
public bool ShouldSerializeAss_Provinces ()
{
return false;
}

}
}
