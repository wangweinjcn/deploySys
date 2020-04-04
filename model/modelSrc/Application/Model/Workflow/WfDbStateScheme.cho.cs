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
        ///<summary>
        ///
        ///订单状态迁移方案</summary>
[UmlElement(Id="09fdf683-d8ee-4b8e-a5f7-05313f5692d5")]
[MonitorProperty]
[StateMachineDB]
[ClassViewConf("{   \"Name\": \"WfDbStateScheme\",   \"Id\": \"09fdf683-d8ee-4b8e-a5f7-05313f5692d5\",   \"FullName\": \"Application.Model.Workflow..WfDbStateScheme\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"key\",       \"Id\": \"15a47527-e90b-4cc8-b5a7-9efaf8d6500f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"键值\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"State\",       \"Id\": \"1f77d428-7d8c-4f6f-957e-112c5b9b9ec4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ClassName\",       \"Id\": \"4e24a327-3c23-4769-b614-cc427fc2fc9b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"类名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Name\",       \"Id\": \"80f76768-177b-41bb-8fc2-e6f48864776c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"FieldName\",       \"Id\": \"9a6aa047-e772-4b0b-aef1-d4320f228f95\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"属性名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class WfDbStateScheme: _baseObject {
[UmlElement(Id="WfDbStateScheme_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///键值</summary>
[UmlElement(Id="15a47527-e90b-4cc8-b5a7-9efaf8d6500f")]
[Display(Name="键值")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  key{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="1f77d428-7d8c-4f6f-957e-112c5b9b9ec4")]
[BindNever]
[NotApplyFromOffice]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  State{
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
        ///类名</summary>
[UmlElement(Id="4e24a327-3c23-4769-b614-cc427fc2fc9b")]
[Display(Name="类名")]
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
        ///<summary>
        ///
        ///名称</summary>
[UmlElement(Id="80f76768-177b-41bb-8fc2-e6f48864776c")]
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
        ///属性名</summary>
[UmlElement(Id="9a6aa047-e772-4b0b-aef1-d4320f228f95")]
[Display(Name="属性名")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  FieldName{
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
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbState>, _baseObjectList<wfDbState>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbState), AsscoisDestPropertyName = "Ass_WfDbStateScheme", AssociaID = "37f8df83-8ff7-48c5-a792-423cc52e4cb1")]
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
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbregion>, _baseObjectList<wfDbregion>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbregion), AsscoisDestPropertyName = "Ass_WfDbStateScheme", AssociaID = "e7e6cc8c-f840-49f5-b4e9-bb43edd7b641")]
public  ICSObjectList<wfDbregion>  Ass_wfDbregion{
get { return (ICSObjectList<wfDbregion>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_wfDbregion ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="efefd4a5-d617-4654-b4c8-cc284ffea2d5")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<wfDbAction>, _baseObjectList<wfDbAction>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(wfDbAction), AsscoisDestPropertyName = "Ass_WfDbStateScheme", AssociaID = "efefd4a5-d617-4654-b4c8-cc284ffea2d5")]
public  ICSObjectList<wfDbAction>  Ass_wfDbAction{
get { return (ICSObjectList<wfDbAction>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_wfDbAction ()
{
return false;
}

}
}
