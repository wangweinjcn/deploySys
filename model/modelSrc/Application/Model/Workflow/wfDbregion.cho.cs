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
[UmlElement(Id="6eca3944-8f9a-4617-81bf-874df6a4ec56")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"wfDbregion\",   \"Id\": \"6eca3944-8f9a-4617-81bf-874df6a4ec56\",   \"FullName\": \"Application.Model.Workflow..wfDbregion\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"isroot\",       \"Id\": \"2542e131-0e14-46ed-a22d-c1e388f4f483\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"根区域\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"GuId\",       \"Id\": \"9a77dcfe-d399-41d3-86f3-858de351fb8b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"ID\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"fieldname\",       \"Id\": \"ea838c74-f7b4-4de3-8936-e39e321dd5f8\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"属性名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"name\",       \"Id\": \"f7db18b3-2764-43f1-8d7a-ee3f79356b1a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class wfDbregion: _baseObject {
[UmlElement(Id="wfDbregion_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///根区域</summary>
[UmlElement(Id="2542e131-0e14-46ed-a22d-c1e388f4f483")]
[Display(Name="根区域")]
[Required]
public Boolean  isroot{
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
        ///ID</summary>
[UmlElement(Id="9a77dcfe-d399-41d3-86f3-858de351fb8b")]
[Display(Name="ID")]
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
        ///<summary>
        ///
        ///属性名</summary>
[UmlElement(Id="ea838c74-f7b4-4de3-8936-e39e321dd5f8")]
[Display(Name="属性名")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  fieldname{
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
[UmlElement(Id="f7db18b3-2764-43f1-8d7a-ee3f79356b1a")]
[Display(Name="名称")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  name{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="4d206c81-8bb6-42c2-bba4-ee87f2b89c6a")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbregion>, _baseObjectList<wfDbregion>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbregion), AsscoisDestPropertyName = "Ass_wfDbregion_parent", AssociaID = "4d206c81-8bb6-42c2-bba4-ee87f2b89c6a")]
public  ICSObjectList<wfDbregion>  Ass_wfDbregion_children{
get { return (ICSObjectList<wfDbregion>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_wfDbregion_children ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="4d206c81-8bb6-42c2-bba4-ee87f2b89c6a")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(wfDbregion),DbFieldName ="Ass_wfDbregion_parent", AsscoisDestPropertyName="Ass_wfDbregion_children", AssociaID = "4d206c81-8bb6-42c2-bba4-ee87f2b89c6a")]
public  wfDbregion  Ass_wfDbregion_parent{
get { return (wfDbregion) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_wfDbregion_parent")]
public  int? Ass_wfDbregion_parent_Id {
get{  var x= getAssPropertyId("Ass_wfDbregion_parent"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_wfDbregion_parent");
}    }
public bool ShouldSerializeAss_wfDbregion_parent ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="4d6e2527-2fdb-42e5-a979-ca58fbfa3fe2")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbAction>, _baseObjectList<wfDbAction>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbAction), AsscoisDestPropertyName = "Ass_wfDbregion", AssociaID = "4d6e2527-2fdb-42e5-a979-ca58fbfa3fe2")]
public  ICSObjectList<wfDbAction>  Ass_actions{
get { return (ICSObjectList<wfDbAction>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_actions ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e7d3b2dc-8753-44b5-bdcd-c9c6a29c8034")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbState>, _baseObjectList<wfDbState>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbState), AsscoisDestPropertyName = "Ass_wfDbregion", AssociaID = "e7d3b2dc-8753-44b5-bdcd-c9c6a29c8034")]
public  ICSObjectList<wfDbState>  Ass_wfDbState{
get { return (ICSObjectList<wfDbState>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_wfDbState ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e7e6cc8c-f840-49f5-b4e9-bb43edd7b641")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(WfDbStateScheme),DbFieldName ="Ass_WfDbStateScheme", AsscoisDestPropertyName="Ass_wfDbregion", AssociaID = "e7e6cc8c-f840-49f5-b4e9-bb43edd7b641")]
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

}
}
