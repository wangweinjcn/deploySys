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
[UmlElement(Id="e1968076-472b-409b-b981-1f3b7f90bcf3")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"DockerInstance\",   \"Id\": \"e1968076-472b-409b-b981-1f3b7f90bcf3\",   \"FullName\": \"deploySys.Model..DockerInstance\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"IP\",       \"Id\": \"1e808cfb-ec06-4bfe-b3df-32f9dd3c0f5b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"baseDir\",       \"Id\": \"246ef127-4637-4f4c-bedf-c8ccea220797\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"当前实例的工作目录，相对路径\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"version\",       \"Id\": \"374e8159-9115-46f7-8a51-0fb0db1efb46\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"status\",       \"Id\": \"6d10cc2a-34a8-481e-ad70-2832c87272de\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"docker状态; 0:预备分配，10：待启动，20：运行中，-1：停止\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"proxyPort\",       \"Id\": \"985519a9-1573-48ea-9c97-8f2cacb29ef1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"isNginx\",       \"Id\": \"a478cc90-3f88-4d84-86a7-7d9ca673142d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"nginx实例，每台主机上只允许一个\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"instanceId\",       \"Id\": \"b8419b26-d397-4ae0-b560-74be4865cd3c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"虚拟机Id\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"isNodeMonitor\",       \"Id\": \"f1f42bf7-ca67-4cd6-b0b0-460c573c5f21\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"是否是node节点实例\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"domain\",       \"Id\": \"f99555d3-1d74-497e-ba89-10970412ed90\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class DockerInstance: Application.Model.Base.BaseObject {
[UmlElement(Id="DockerInstance_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="1e808cfb-ec06-4bfe-b3df-32f9dd3c0f5b")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  IP{
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
        ///当前实例的工作目录，相对路径</summary>
[UmlElement(Id="246ef127-4637-4f4c-bedf-c8ccea220797")]
[Display(Name="当前实例的工作目录，相对路径")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  baseDir{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="374e8159-9115-46f7-8a51-0fb0db1efb46")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  version{
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
        ///docker状态; 0:预备分配，10：待启动，20：运行中，-1：停止</summary>
[UmlElement(Id="6d10cc2a-34a8-481e-ad70-2832c87272de")]
[Display(Name="docker状态; 0:预备分配，10：待启动，20：运行中，-1：停止")]
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
        ///<summary></summary>
[UmlElement(Id="985519a9-1573-48ea-9c97-8f2cacb29ef1")]
[Required]
public Int32  proxyPort{
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
        ///nginx实例，每台主机上只允许一个</summary>
[UmlElement(Id="a478cc90-3f88-4d84-86a7-7d9ca673142d")]
[Display(Name="nginx实例，每台主机上只允许一个")]
public Boolean?  isNginx{
get { 
 var pValue= getPropertyValue();
Boolean? res;
res= (Boolean?)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary>
        ///
        ///虚拟机Id</summary>
[UmlElement(Id="b8419b26-d397-4ae0-b560-74be4865cd3c")]
[Display(Name="虚拟机Id")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  instanceId{
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
        ///是否是node节点实例</summary>
[UmlElement(Id="f1f42bf7-ca67-4cd6-b0b0-460c573c5f21")]
[Display(Name="是否是node节点实例")]
[Required]
public Boolean  isNodeMonitor{
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
[UmlElement(Id="f99555d3-1d74-497e-ba89-10970412ed90")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  domain{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="15a917b2-e839-4550-b284-af66cca313fb")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(MicroServiceApp),DbFieldName ="Ass_MicroServiceApp", AsscoisDestPropertyName="Ass_DockerInstance", AssociaID = "15a917b2-e839-4550-b284-af66cca313fb")]
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
[UmlElement(Id="6ff83ff6-45f4-42e7-a5bf-dc92849f1690")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(ReleaseTask),DbFieldName ="Ass_ReleaseTask", AsscoisDestPropertyName="Ass_DockerInstance", AssociaID = "6ff83ff6-45f4-42e7-a5bf-dc92849f1690")]
public  ReleaseTask  Ass_ReleaseTask{
get { return (ReleaseTask) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_ReleaseTask")]
public  int? Ass_ReleaseTask_Id {
get{  var x= getAssPropertyId("Ass_ReleaseTask"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_ReleaseTask");
}    }
public bool ShouldSerializeAss_ReleaseTask ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="bc33100e-f4fc-4181-bf86-145133600b46")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<webSite>, _baseObjectList<webSite>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(webSite), AsscoisDestPropertyName = "Ass_DockerInstance", AssociaID = "bc33100e-f4fc-4181-bf86-145133600b46")]
public  ICSObjectList<webSite>  Ass_webSite{
get { return (ICSObjectList<webSite>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_webSite ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="eb3605f1-51ff-48ff-885a-ead08b46166a")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(HostResource),DbFieldName ="Ass_HostResource", AsscoisDestPropertyName="Ass_DockerInstance", AssociaID = "eb3605f1-51ff-48ff-885a-ead08b46166a")]
public  HostResource  Ass_HostResource{
get { return (HostResource) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_HostResource")]
public  int? Ass_HostResource_Id {
get{  var x= getAssPropertyId("Ass_HostResource"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_HostResource");
}    }
public bool ShouldSerializeAss_HostResource ()
{
return false;
}

}
}
