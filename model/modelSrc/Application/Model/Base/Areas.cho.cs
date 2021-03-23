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
        ///区县</summary>
[UmlElement(Id="f306166b-3a27-4efe-b7d1-62812e4deedc")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"Areas\",   \"Id\": \"f306166b-3a27-4efe-b7d1-62812e4deedc\",   \"FullName\": \"Application.Model.Base..Areas\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"area\",       \"Id\": \"25b059fc-766b-4a6f-825b-225d4f92492a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"areaKey\",       \"Id\": \"32d8029c-18bf-4c72-8ea9-91755e77b336\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"区县Key\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"cityKey\",       \"Id\": \"b8d3f054-2fe9-4185-b515-69c2eb16a2a7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"归属城市Key\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"areaEn\",       \"Id\": \"ffa772e1-ec8a-45f0-ac18-542cd4d41a60\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"英文名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class Areas: _baseObject {
[UmlElement(Id="Areas_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///名称</summary>
[UmlElement(Id="25b059fc-766b-4a6f-825b-225d4f92492a")]
[Display(Name="名称")]
[StringLength(225,ErrorMessage ="超过最大长度")]
[Required]
public String  area{
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
        ///区县Key</summary>
[UmlElement(Id="32d8029c-18bf-4c72-8ea9-91755e77b336")]
[Display(Name="区县Key")]
[StringLength(225,ErrorMessage ="超过最大长度")]
[Required]
public String  areaKey{
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
        ///归属城市Key</summary>
[UmlElement(Id="b8d3f054-2fe9-4185-b515-69c2eb16a2a7")]
[Display(Name="归属城市Key")]
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
        ///<summary>
        ///
        ///英文名</summary>
[UmlElement(Id="ffa772e1-ec8a-45f0-ac18-542cd4d41a60")]
[Display(Name="英文名")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  areaEn{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="58c16685-7e6b-443b-80fc-a4f1a92305ec")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<SysUser>, _baseObjectList<SysUser>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(SysUser), AsscoisDestPropertyName = "Ass_Areas", AssociaID = "58c16685-7e6b-443b-80fc-a4f1a92305ec")]
public  ICSObjectList<SysUser>  Ass_SysUser{
get { return (ICSObjectList<SysUser>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_SysUser ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="76bddabe-e4fe-471d-9245-1ed38f2a1fbb")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Citys),DbFieldName ="Ass_Citys", AsscoisDestPropertyName="Ass_Areas", AssociaID = "76bddabe-e4fe-471d-9245-1ed38f2a1fbb")]
public  Citys  Ass_Citys{
get { return (Citys) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Citys")]
public  int? Ass_Citys_Id {
get{  var x= getAssPropertyId("Ass_Citys"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Citys");
}    }
public bool ShouldSerializeAss_Citys ()
{
return false;
}

}
}
