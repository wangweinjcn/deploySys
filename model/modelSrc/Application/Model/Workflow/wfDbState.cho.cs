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
namespace Application.Model.Workflow
{
      using   Chloe.Ext.stateMachine;
        ///
        ///<summary></summary>
[UmlElement(Id="bc28a707-8ac0-4b97-b826-5bcb3e97394e")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"wfDbState\",   \"Id\": \"bc28a707-8ac0-4b97-b826-5bcb3e97394e\",   \"FullName\": \"Application.Model.Workflow..wfDbState\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"fieldName\",       \"Id\": \"5b6704ed-bfc3-45e3-96df-9fe66a57886f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"属性名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Name\",       \"Id\": \"65dbfc4b-2b8e-487b-9204-e43a7c888c69\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CanEditRolesJson\",       \"Id\": \"72ea64ef-e209-47ea-9c13-f66811bec51c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"[Display(Name =\"编辑权限角色\")]\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"readOnlyFieldsJson\",       \"Id\": \"812d0c1d-373d-4833-acbd-0fa1fa0bc860\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"只读字段列表\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsRegion\",       \"Id\": \"91d33b12-894b-4a96-b7a3-e2945904ffc2\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"域状态\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"regionName\",       \"Id\": \"b05a6d94-7165-494e-8e33-a77d08c35ecd\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"域名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"reqiredFieldsJson\",       \"Id\": \"cac11bbc-3c49-416a-acd2-72669db6dbfd\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"必填字段列表\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"GuId\",       \"Id\": \"e7454d12-45f4-47c8-b7bc-213deafe48dd\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"状态ID\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class wfDbState: _baseObject {
[UmlElement(Id="wfDbState_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///属性名</summary>
[UmlElement(Id="5b6704ed-bfc3-45e3-96df-9fe66a57886f")]
[Display(Name="属性名")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  fieldName{
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
        ///名称</summary>
[UmlElement(Id="65dbfc4b-2b8e-487b-9204-e43a7c888c69")]
[Display(Name="名称")]
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
        ///<summary>
        ///
        ///编辑权限角色key列表</summary>
[UmlElement(Id="72ea64ef-e209-47ea-9c13-f66811bec51c")]
[Display(Name ="编辑权限角色")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  CanEditRolesJson{
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
        ///只读字段列表</summary>
[UmlElement(Id="812d0c1d-373d-4833-acbd-0fa1fa0bc860")]
[Display(Name="只读字段列表")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  readOnlyFieldsJson{
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
        ///域状态</summary>
[UmlElement(Id="91d33b12-894b-4a96-b7a3-e2945904ffc2")]
[Display(Name="域状态")]
[Required]
public Boolean  IsRegion{
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
        ///域名称</summary>
[UmlElement(Id="b05a6d94-7165-494e-8e33-a77d08c35ecd")]
[Display(Name="域名称")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  regionName{
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
        ///必填字段列表</summary>
[UmlElement(Id="cac11bbc-3c49-416a-acd2-72669db6dbfd")]
[Display(Name="必填字段列表")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  reqiredFieldsJson{
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
        ///状态ID</summary>
[UmlElement(Id="e7454d12-45f4-47c8-b7bc-213deafe48dd")]
[Display(Name="状态ID")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  GuId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="37f8df83-8ff7-48c5-a792-423cc52e4cb1")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(WfDbStateScheme),DbFieldName ="Ass_WfDbStateScheme", AsscoisDestPropertyName="Ass_wfDbState", AssociaID = "37f8df83-8ff7-48c5-a792-423cc52e4cb1")]
public  WfDbStateScheme  Ass_WfDbStateScheme{
get { return (WfDbStateScheme) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_WfDbStateScheme")]
public  int? Ass_WfDbStateScheme_Id {
get{  var x= getAssPropertyId("Ass_WfDbStateScheme"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_WfDbStateScheme");
}    }
public bool ShouldSerializeAss_WfDbStateScheme ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="83c29af3-1580-4bd2-9d3e-2dd79474fae8")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbAction>, _baseObjectList<wfDbAction>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbAction), AsscoisDestPropertyName = "Ass_froms", AssociaID = "83c29af3-1580-4bd2-9d3e-2dd79474fae8")]
public  ICSObjectList<wfDbAction>  Ass_fromAction{
get { return (ICSObjectList<wfDbAction>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_fromAction ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="cca3c2a9-a3fb-4351-8964-6bc4a19c1a45")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbAction>, _baseObjectList<wfDbAction>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbAction), AsscoisDestPropertyName = "Ass_to", AssociaID = "cca3c2a9-a3fb-4351-8964-6bc4a19c1a45")]
public  ICSObjectList<wfDbAction>  Ass_toActions{
get { return (ICSObjectList<wfDbAction>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_toActions ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e7d3b2dc-8753-44b5-bdcd-c9c6a29c8034")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(wfDbregion),DbFieldName ="Ass_wfDbregion", AsscoisDestPropertyName="Ass_wfDbState", AssociaID = "e7d3b2dc-8753-44b5-bdcd-c9c6a29c8034")]
public  wfDbregion  Ass_wfDbregion{
get { return (wfDbregion) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_wfDbregion")]
public  int? Ass_wfDbregion_Id {
get{  var x= getAssPropertyId("Ass_wfDbregion"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_wfDbregion");
}    }
public bool ShouldSerializeAss_wfDbregion ()
{
return false;
}

}
}
