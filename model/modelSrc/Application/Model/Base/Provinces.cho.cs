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
[UmlElement(Id="0bdd9bc5-317d-4e43-ad49-1e85b148d216")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"Provinces\",   \"Id\": \"0bdd9bc5-317d-4e43-ad49-1e85b148d216\",   \"FullName\": \"Application.Model.Base..Provinces\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"province\",       \"Id\": \"7a10be62-853f-497e-b7be-5e78db57c71c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"省份名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"provinceEn\",       \"Id\": \"7fce3915-54c4-4025-9669-29de2eebd758\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"省份名称英文\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"provinceKey\",       \"Id\": \"892b640e-ed3e-4fac-916e-ec9ed1158984\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"省份Key\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CountryId\",       \"Id\": \"d0feb251-7a9c-4be5-b372-cee2e6b42bb7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class Provinces: _baseObject {
[UmlElement(Id="Provinces_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///省份名称</summary>
[UmlElement(Id="7a10be62-853f-497e-b7be-5e78db57c71c")]
[Display(Name="省份名称")]
[StringLength(225,ErrorMessage ="超过最大长度")]
[Required]
public String  province{
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
        ///省份名称英文</summary>
[UmlElement(Id="7fce3915-54c4-4025-9669-29de2eebd758")]
[Display(Name="省份名称英文")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  provinceEn{
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
        ///省份Key</summary>
[UmlElement(Id="892b640e-ed3e-4fac-916e-ec9ed1158984")]
[Display(Name="省份Key")]
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
[UmlElement(Id="d0feb251-7a9c-4be5-b372-cee2e6b42bb7")]
[NotMapped]
[UmlDerived(false)]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  CountryId{
get { 
    
            String res =(String)DefaultForType(typeof(String));
            CountryIdDerive(ref res);
             return res;}

}

partial void CountryIdDerive(ref String res);

        ///
        ///<summary></summary>
[UmlElement(Id="53504164-58c7-47ee-b235-9cec1d36f60e")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Country),DbFieldName ="Ass_Country", AsscoisDestPropertyName="Ass_Provinces", AssociaID = "53504164-58c7-47ee-b235-9cec1d36f60e")]
public  Country  Ass_Country{
get { return (Country) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Country")]
public  int? Ass_Country_Id {
get{  var x= getAssPropertyId("Ass_Country"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Country");
}    }
public bool ShouldSerializeAss_Country ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="df9b4ed1-82b5-4894-8db6-2d17d1ba9e16")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<Citys>, _baseObjectList<Citys>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(Citys), AsscoisDestPropertyName = "Ass_Provinces", AssociaID = "df9b4ed1-82b5-4894-8db6-2d17d1ba9e16")]
public  ICSObjectList<Citys>  Ass_Citys{
get { return (ICSObjectList<Citys>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_Citys ()
{
return false;
}

}
}
