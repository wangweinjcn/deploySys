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
        ///状态变迁表</summary>
[UmlElement(Id="76e04324-27f7-44e4-9fcd-e0b590e0953f")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"wfDbAction\",   \"Id\": \"76e04324-27f7-44e4-9fcd-e0b590e0953f\",   \"FullName\": \"Application.Model.Workflow..wfDbAction\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"fromstateID\",       \"Id\": \"17c4c730-be1a-4047-8b20-9057333257d1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"起始状态\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"memo\",       \"Id\": \"1cc7445a-87cc-4d81-a70e-20965641ab61\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"备注\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"rolesCanDoJson\",       \"Id\": \"375a1712-367c-437a-bdee-a1bb175bf6e1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"（暂不使用）\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"isEnd\",       \"Id\": \"551a24f4-4de4-43e3-b5b7-e6774304597f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"结束活动标记\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"tostateName\",       \"Id\": \"55f66ab1-4a15-40cf-af14-9a66add3a167\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"目标状态名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"guard\",       \"Id\": \"56148b80-a565-4a6f-be12-86fac115ccde\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"[Display(Name =\"是否可执行函数\")]\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"fieldName\",       \"Id\": \"60464ff0-3e95-465c-82ca-5c8371d3ea46\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"属性名\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"effect\",       \"Id\": \"6d55ccaf-173a-4edd-a7ff-72994757fe69\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"[Display(Name =\"影响函数\")]\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"tostateID\",       \"Id\": \"8636212d-5c99-4d75-9939-16d38cdfa6a8\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"目标状态ID\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"NoticeKeyJson\",       \"Id\": \"96980ef2-56fa-4b2f-9a8a-06d377e98fae\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"[Display(Name =\"通知对象\")]\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"trigger\",       \"Id\": \"9898f0aa-c38c-4eab-906d-523e1ba513d7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 6,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"[Display(Name =\"触发函数\")]\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"isStart\",       \"Id\": \"c361a722-5d55-40b2-9e6a-a3728569a9e5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 6,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"开始活动标记\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"name\",       \"Id\": \"d6a7c5a3-1df8-4aec-9eea-eff81d222329\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 7,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"GuId\",       \"Id\": \"d790d3be-ebd8-4295-8317-e1c4fb12991a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 7,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"活动ID\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"regionName\",       \"Id\": \"f51c920d-b5c9-47cf-bd6b-3e7d2d06c4e5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 8,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"归属域名称\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class wfDbAction: _baseObject {
[UmlElement(Id="wfDbAction_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///起始状态</summary>
[UmlElement(Id="17c4c730-be1a-4047-8b20-9057333257d1")]
[Display(Name="起始状态")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  fromstateID{
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
        ///备注</summary>
[UmlElement(Id="1cc7445a-87cc-4d81-a70e-20965641ab61")]
[Display(Name="备注")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  memo{
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
        ///（暂不使用）</summary>
[UmlElement(Id="375a1712-367c-437a-bdee-a1bb175bf6e1")]
[Display(Name="（暂不使用）")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  rolesCanDoJson{
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
        ///结束活动标记</summary>
[UmlElement(Id="551a24f4-4de4-43e3-b5b7-e6774304597f")]
[Display(Name="结束活动标记")]
[Required]
public Boolean  isEnd{
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
        ///目标状态名称</summary>
[UmlElement(Id="55f66ab1-4a15-40cf-af14-9a66add3a167")]
[Display(Name="目标状态名称")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  tostateName{
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
        ///检测是否可以执行的函数，不设置，默认查找Cando_(trigger)</summary>
[UmlElement(Id="56148b80-a565-4a6f-be12-86fac115ccde")]
[Display(Name ="是否可执行函数")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  guard{
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
[UmlElement(Id="60464ff0-3e95-465c-82ca-5c8371d3ea46")]
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
        ///该活动的影响函数（被触发执行）,不设置默认执行on(trigger)</summary>
[UmlElement(Id="6d55ccaf-173a-4edd-a7ff-72994757fe69")]
[Display(Name ="影响函数")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  effect{
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
        ///目标状态ID</summary>
[UmlElement(Id="8636212d-5c99-4d75-9939-16d38cdfa6a8")]
[Display(Name="目标状态ID")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  tostateID{
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
        ///通知对象key列表，客户，还是客服，或是主管</summary>
[UmlElement(Id="96980ef2-56fa-4b2f-9a8a-06d377e98fae")]
[Display(Name ="通知对象")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  NoticeKeyJson{
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
        ///触发活动函数</summary>
[UmlElement(Id="9898f0aa-c38c-4eab-906d-523e1ba513d7")]
[Display(Name ="触发函数")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  trigger{
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
        ///开始活动标记</summary>
[UmlElement(Id="c361a722-5d55-40b2-9e6a-a3728569a9e5")]
[Display(Name="开始活动标记")]
[Required]
public Boolean  isStart{
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
        ///名称</summary>
[UmlElement(Id="d6a7c5a3-1df8-4aec-9eea-eff81d222329")]
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
        ///<summary>
        ///
        ///活动ID</summary>
[UmlElement(Id="d790d3be-ebd8-4295-8317-e1c4fb12991a")]
[Display(Name="活动ID")]
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
        ///归属域名称</summary>
[UmlElement(Id="f51c920d-b5c9-47cf-bd6b-3e7d2d06c4e5")]
[Display(Name="归属域名称")]
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
        ///<summary></summary>
[UmlElement(Id="4d6e2527-2fdb-42e5-a979-ca58fbfa3fe2")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(wfDbregion),DbFieldName ="Ass_wfDbregion", AsscoisDestPropertyName="Ass_actions", AssociaID = "4d6e2527-2fdb-42e5-a979-ca58fbfa3fe2")]
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

        ///
        ///<summary></summary>
[UmlElement(Id="83c29af3-1580-4bd2-9d3e-2dd79474fae8")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(wfDbState),DbFieldName ="Ass_froms", AsscoisDestPropertyName="Ass_fromAction", AssociaID = "83c29af3-1580-4bd2-9d3e-2dd79474fae8")]
public  wfDbState  Ass_froms{
get { return (wfDbState) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_froms")]
public  int? Ass_froms_Id {
get{  var x= getAssPropertyId("Ass_froms"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_froms");
}    }
public bool ShouldSerializeAss_froms ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="cca3c2a9-a3fb-4351-8964-6bc4a19c1a45")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(wfDbState),DbFieldName ="Ass_to", AsscoisDestPropertyName="Ass_toActions", AssociaID = "cca3c2a9-a3fb-4351-8964-6bc4a19c1a45")]
public  wfDbState  Ass_to{
get { return (wfDbState) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_to")]
public  int? Ass_to_Id {
get{  var x= getAssPropertyId("Ass_to"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_to");
}    }
public bool ShouldSerializeAss_to ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="efefd4a5-d617-4654-b4c8-cc284ffea2d5")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(WfDbStateScheme),DbFieldName ="Ass_WfDbStateScheme", AsscoisDestPropertyName="Ass_wfDbAction", AssociaID = "efefd4a5-d617-4654-b4c8-cc284ffea2d5")]
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
