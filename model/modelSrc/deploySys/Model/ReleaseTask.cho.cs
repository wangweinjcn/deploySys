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
namespace deploySys.Model
{
        ///
        ///<summary></summary>
[UmlElement(Id="c0a063d6-121c-495a-98c5-20acac43202a")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"ReleaseTask\",   \"Id\": \"c0a063d6-121c-495a-98c5-20acac43202a\",   \"FullName\": \"deploySys.Model..ReleaseTask\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"FileName\",       \"Id\": \"02fbfc1f-10ac-4449-ac0a-0f637c4ab9b7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"status\",       \"Id\": \"065cfc0d-c0e8-4708-ad63-09ac962f5ad6\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"任务状态，-1:分配失败;0：新增；10：审核待发布；20：已发布；30:已注册（与服务注册中心联通,可选)\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"count\",       \"Id\": \"09ab4968-9473-408a-942a-d469dd56cf05\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"发布个数\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"ProcessInfo\",       \"Id\": \"0f433ee4-de0b-4343-bf73-4af1fa549fc7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"overridePolicy\",       \"Id\": \"2729ae4c-9585-4e2c-a361-71921cea0adb\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新策略；0：发布包只是部分文件，直接覆盖目标目录；1：发布包为全文件包，比对覆盖更新的文件，并删除不存在的文件；2：发布包为全文件包，不比对直接覆盖目标目录\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"selectHostPolicy\",       \"Id\": \"4fae492c-962c-4218-baf2-763d8bd67eb7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"主机选择策略；0：负载最低；1：实例最少\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"dockerNetType\",       \"Id\": \"67360655-7904-4495-adb9-e5e81149695f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"docker运行的网络模式；0：bridge ；1：host \",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Version\",       \"Id\": \"86306d44-3e7a-4499-a627-e07df9b1d8c0\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"git的版本号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"FileGuid\",       \"Id\": \"917e376f-0680-44ce-800d-4aa863f0da0e\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"发布的包文件\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"memo\",       \"Id\": \"bb170ae2-e1dc-4fd9-92d1-4dc9370186f8\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"releaseType\",       \"Id\": \"e62ff918-8b62-49f7-9219-29e83499ce70\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 6,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"发布类型:0：新增实例；1：更新实例\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"HostIds\",       \"Id\": \"fe8ebb11-2f6f-4880-97a5-15e9ae9e2762\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 6,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"confFileOverride\",       \"Id\": \"ff0b9fcb-af79-427e-8f92-cd2278cc913b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 7,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"配置文件是否更新；默认否\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class ReleaseTask: Application.Model.Base.BaseObject {
[UmlElement(Id="ReleaseTask_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="02fbfc1f-10ac-4449-ac0a-0f637c4ab9b7")]
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
        ///任务状态，-1:分配失败;0：新增；10：审核待发布；20：已发布；30:已注册（与服务注册中心联通,可选)</summary>
[UmlElement(Id="065cfc0d-c0e8-4708-ad63-09ac962f5ad6")]
[Display(Name="任务状态，-1:分配失败;0：新增；10：审核待发布；20：已发布；30:已注册（与服务注册中心联通,可选)")]
[Required]
public Int32  status{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///发布个数</summary>
[UmlElement(Id="09ab4968-9473-408a-942a-d469dd56cf05")]
[Display(Name="发布个数")]
[Required]
public Int32  count{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 1;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="0f433ee4-de0b-4343-bf73-4af1fa549fc7")]
[StringLength(1024,ErrorMessage ="超过最大长度")]
public String  ProcessInfo{
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
        ///更新策略；0：发布包只是部分文件，直接覆盖目标目录；1：发布包为全文件包，比对覆盖更新的文件，并删除不存在的文件；2：发布包为全文件包，不比对直接覆盖目标目录</summary>
[UmlElement(Id="2729ae4c-9585-4e2c-a361-71921cea0adb")]
[Display(Name="更新策略；0：发布包只是部分文件，直接覆盖目标目录；1：发布包为全文件包，比对覆盖更新的文件，并删除不存在的文件；2：发布包为全文件包，不比对直接覆盖目标目录")]
[Required]
public Int32  overridePolicy{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///主机选择策略；0：负载最低；1：实例最少</summary>
[UmlElement(Id="4fae492c-962c-4218-baf2-763d8bd67eb7")]
[Display(Name="主机选择策略；0：负载最低；1：实例最少")]
[Required]
public Int32  selectHostPolicy{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///docker运行的网络模式；0：bridge ；1：host </summary>
[UmlElement(Id="67360655-7904-4495-adb9-e5e81149695f")]
[Display(Name="docker运行的网络模式；0：bridge ；1：host ")]
[Required]
public Int32  dockerNetType{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///git的版本号</summary>
[UmlElement(Id="86306d44-3e7a-4499-a627-e07df9b1d8c0")]
[Display(Name="git的版本号")]
[Required]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Version{
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
        ///发布的包文件</summary>
[UmlElement(Id="917e376f-0680-44ce-800d-4aa863f0da0e")]
[Display(Name="发布的包文件")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  FileGuid{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="bb170ae2-e1dc-4fd9-92d1-4dc9370186f8")]
[StringLength(1024,ErrorMessage ="超过最大长度")]
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
        ///发布类型:0：新增实例；1：更新实例</summary>
[UmlElement(Id="e62ff918-8b62-49f7-9219-29e83499ce70")]
[Display(Name="发布类型:0：新增实例；1：更新实例")]
[Required]
public Int32  releaseType{
get { 
 var pValue= getPropertyValue();
Int32 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int32)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="fe8ebb11-2f6f-4880-97a5-15e9ae9e2762")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  HostIds{
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
        ///配置文件是否更新；默认否</summary>
[UmlElement(Id="ff0b9fcb-af79-427e-8f92-cd2278cc913b")]
[Display(Name="配置文件是否更新；默认否")]
[Required]
public Boolean  confFileOverride{
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
[UmlElement(Id="2df0c254-5ace-44a8-9f13-b8af3877068e")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<HostTask>, _baseObjectList<HostTask>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(HostTask), AsscoisDestPropertyName = "Ass_ReleaseTask", AssociaID = "2df0c254-5ace-44a8-9f13-b8af3877068e")]
public  ICSObjectList<HostTask>  Ass_HostTask{
get { return (ICSObjectList<HostTask>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_HostTask ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="549c7a22-6fe4-4125-a6d8-00715a3c3378")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.nosaveOfOneToOne,   AssociaClass = typeof(appVersion), AssociaID = "549c7a22-6fe4-4125-a6d8-00715a3c3378")]
public  appVersion  Ass_appVersion{
get { return (appVersion) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_appVersion ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="6ff83ff6-45f4-42e7-a5bf-dc92849f1690")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<DockerInstance>, _baseObjectList<DockerInstance>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(DockerInstance), AsscoisDestPropertyName = "Ass_ReleaseTask", AssociaID = "6ff83ff6-45f4-42e7-a5bf-dc92849f1690")]
public  ICSObjectList<DockerInstance>  Ass_DockerInstance{
get { return (ICSObjectList<DockerInstance>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_DockerInstance ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="943526e8-deb6-436a-b5bc-41fb6c981d12")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(MicroServiceApp),DbFieldName ="Ass_MicroServiceApp", AsscoisDestPropertyName="Ass_ReleaseTask", AssociaID = "943526e8-deb6-436a-b5bc-41fb6c981d12")]
public  MicroServiceApp  Ass_MicroServiceApp{
get { return (MicroServiceApp) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_MicroServiceApp")]
public  int? Ass_MicroServiceApp_Id {
get{  var x= getAssPropertyId("Ass_MicroServiceApp"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_MicroServiceApp");
}    }
public bool ShouldSerializeAss_MicroServiceApp ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="a804e7a2-c736-4a25-98cc-5d7696c96c45")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Region),DbFieldName ="Ass_Region", AsscoisDestPropertyName="Ass_ReleaseTask", AssociaID = "a804e7a2-c736-4a25-98cc-5d7696c96c45")]
public  Region  Ass_Region{
get { return (Region) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Region")]
public  int? Ass_Region_Id {
get{  var x= getAssPropertyId("Ass_Region"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Region");
}    }
public bool ShouldSerializeAss_Region ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="ac0c3a86-ee6b-46ba-b72e-6d20fb528c33")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Zone),DbFieldName ="Ass_Zone", AsscoisDestPropertyName="Ass_ReleaseTask", AssociaID = "ac0c3a86-ee6b-46ba-b72e-6d20fb528c33")]
public  Zone  Ass_Zone{
get { return (Zone) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Zone")]
public  int? Ass_Zone_Id {
get{  var x= getAssPropertyId("Ass_Zone"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Zone");
}    }
public bool ShouldSerializeAss_Zone ()
{
return false;
}

}
}
