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
[UmlElement(Id="7c741383-5e60-4cc9-8da4-0f044b5d6aa3")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"MicroServiceApp\",   \"Id\": \"7c741383-5e60-4cc9-8da4-0f044b5d6aa3\",   \"FullName\": \"deploySys.Model..MicroServiceApp\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"LogRelationDir\",       \"Id\": \"06b25aef-abfa-4fce-92a7-6d64dd7fb8a9\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"应用日志目录\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"createContainerParams\",       \"Id\": \"0edfeec8-4ffd-4ca5-8686-70d634f09ac7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"创建桥接网络容器参数\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"regionIds\",       \"Id\": \"1481b453-0157-4ef1-b845-42c8ac87c760\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"buildScript\",       \"Id\": \"25dd5d2d-fce1-4a3c-a93f-f387796a1d30\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"OwnerId\",       \"Id\": \"4de5b3e7-bb82-4b65-bec8-0087acaba06e\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"app维护所有者\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"confFileNames\",       \"Id\": \"54820c00-09f3-45fa-a445-d4b37c81f81b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"配置文件名，逗号隔开；根据retationPath进行判断；支持配置文件目录\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"sslKey\",       \"Id\": \"5cff2d07-ebaa-4fbf-ae9a-cacdff264d99\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"sslKey证书内容\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"appName\",       \"Id\": \"60570ff6-535c-4b99-8846-b8e1b9bbfa24\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"rootDirMainFile\",       \"Id\": \"673acef9-c696-4cca-8864-c65d8f8cae1b\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"根目录下的主文件，应于判断目录位置，同时也是启动命令的参数\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"proxyPort\",       \"Id\": \"699f5791-e581-4770-b6a5-221a849f9b05\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 5,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"网关服务，端口转发服务器外部代理的端口号\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"serviceType\",       \"Id\": \"7ead9482-1bff-4685-839b-2f66df34ed09\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 6,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"0:webServcie，1：nginx站点；2：端口服务；3：需要域名解析的webservice\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UserName\",       \"Id\": \"8ade311c-23d0-4cd1-a4ee-98e0befcb432\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 6,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"stopCommand\",       \"Id\": \"8d263b63-2ee5-45f6-9a1c-1b6154f2ad6f\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 7,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"key\",       \"Id\": \"909ae5e9-d105-463c-a692-151d9d98e5e3\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 7,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"对应注册中心的appKey\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"port\",       \"Id\": \"984f2906-59ba-4052-8efd-ade7dbdd634c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 8,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"应用服务端口\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"useSsl\",       \"Id\": \"a5134bd1-8fb0-4ac9-8b8d-b6968597eeb5\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 8,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"对于web站点是否使用ssl\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"createContainerParams2\",       \"Id\": \"a66be721-df74-47b9-a1e6-c1dfc8ade63c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 9,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"创建主机网络容器参数\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"memo\",       \"Id\": \"ab958ed6-2a5b-4984-805f-a1cd3a83e486\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 9,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"hostname\",       \"Id\": \"aeafea33-df50-4094-8f36-9331648769c9\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 10,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"反向代理主机域名，多个逗号隔开\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"sslPem\",       \"Id\": \"e0d64650-099a-4c4a-ac5d-63392bb8cbc1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 10,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"sslPem证书\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"startCommand\",       \"Id\": \"e44336b4-178f-49f7-8579-2bdcbdececa1\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 11,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterID\",       \"Id\": \"10943d3a-0df0-4a18-9a9a-e4ad3b1d53c4\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdaterName\",       \"Id\": \"3f52d93b-e267-4d81-8274-88753240f48d\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreateDt\",       \"Id\": \"4d0e61b5-9672-4712-9ce5-21aed9e1da86\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成时间，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"UpdateDt\",       \"Id\": \"9d510603-bb0b-434a-91a5-22b9f3ba1616\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"datetime\",       \"dataType\": \"datetime\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"更新日期，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"IsDeleted\",       \"Id\": \"a91113f0-36d0-45b8-8b92-d768610a27e7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterName\",       \"Id\": \"d0f7ee84-39da-4af8-9f2b-545619022b02\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"创建人名字\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"CreaterID\",       \"Id\": \"d26b31fa-6b90-41d2-a27e-a3ad6b988d0c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"生成人id，自动生成\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class MicroServiceApp: Application.Model.Base.BaseObject {
[UmlElement(Id="MicroServiceApp_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary>
        ///
        ///应用日志目录</summary>
[UmlElement(Id="06b25aef-abfa-4fce-92a7-6d64dd7fb8a9")]
[Display(Name="应用日志目录")]
[Required]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  LogRelationDir{
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
        ///创建桥接网络容器参数</summary>
[UmlElement(Id="0edfeec8-4ffd-4ca5-8686-70d634f09ac7")]
[Display(Name="创建桥接网络容器参数")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  createContainerParams{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="1481b453-0157-4ef1-b845-42c8ac87c760")]
[NotMapped]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  regionIds{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="25dd5d2d-fce1-4a3c-a93f-f387796a1d30")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  buildScript{
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
        ///app维护所有者</summary>
[UmlElement(Id="4de5b3e7-bb82-4b65-bec8-0087acaba06e")]
[Display(Name="app维护所有者")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  OwnerId{
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
        ///配置文件名，逗号隔开；根据retationPath进行判断；支持配置文件目录</summary>
[UmlElement(Id="54820c00-09f3-45fa-a445-d4b37c81f81b")]
[Display(Name="配置文件名，逗号隔开；根据retationPath进行判断；支持配置文件目录")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  confFileNames{
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
        ///sslKey证书内容</summary>
[UmlElement(Id="5cff2d07-ebaa-4fbf-ae9a-cacdff264d99")]
[Display(Name="sslKey证书内容")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  sslKey{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="60570ff6-535c-4b99-8846-b8e1b9bbfa24")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  appName{
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
        ///根目录下的主文件，应于判断目录位置，同时也是启动命令的参数</summary>
[UmlElement(Id="673acef9-c696-4cca-8864-c65d8f8cae1b")]
[Display(Name="根目录下的主文件，应于判断目录位置，同时也是启动命令的参数")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  rootDirMainFile{
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
        ///网关服务，端口转发服务器外部代理的端口号</summary>
[UmlElement(Id="699f5791-e581-4770-b6a5-221a849f9b05")]
[Display(Name="网关服务，端口转发服务器外部代理的端口号")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  proxyPort{
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
        ///0:webServcie，1：nginx站点；2：端口服务；3：需要域名解析的webservice</summary>
[UmlElement(Id="7ead9482-1bff-4685-839b-2f66df34ed09")]
[Display(Name="0:webServcie，1：nginx站点；2：端口服务；3：需要域名解析的webservice")]
[Required]
public Int32  serviceType{
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
[UmlElement(Id="8ade311c-23d0-4cd1-a4ee-98e0befcb432")]
[NotMapped]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  UserName{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="8d263b63-2ee5-45f6-9a1c-1b6154f2ad6f")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  stopCommand{
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
        ///对应注册中心的appKey</summary>
[UmlElement(Id="909ae5e9-d105-463c-a692-151d9d98e5e3")]
[Display(Name="对应注册中心的appKey")]
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
        ///<summary>
        ///
        ///应用服务端口</summary>
[UmlElement(Id="984f2906-59ba-4052-8efd-ade7dbdd634c")]
[Display(Name="应用服务端口")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  port{
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
        ///对于web站点是否使用ssl</summary>
[UmlElement(Id="a5134bd1-8fb0-4ac9-8b8d-b6968597eeb5")]
[Display(Name="对于web站点是否使用ssl")]
public Boolean?  useSsl{
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
        ///创建主机网络容器参数</summary>
[UmlElement(Id="a66be721-df74-47b9-a1e6-c1dfc8ade63c")]
[Display(Name="创建主机网络容器参数")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  createContainerParams2{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="ab958ed6-2a5b-4984-805f-a1cd3a83e486")]
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
        ///反向代理主机域名，多个逗号隔开</summary>
[UmlElement(Id="aeafea33-df50-4094-8f36-9331648769c9")]
[Display(Name="反向代理主机域名，多个逗号隔开")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  hostname{
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
        ///sslPem证书</summary>
[UmlElement(Id="e0d64650-099a-4c4a-ac5d-63392bb8cbc1")]
[Display(Name="sslPem证书")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  sslPem{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="e44336b4-178f-49f7-8579-2bdcbdececa1")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  startCommand{
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
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<DockerInstance>, _baseObjectList<DockerInstance>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(DockerInstance), AsscoisDestPropertyName = "Ass_MicroServiceApp", AssociaID = "15a917b2-e839-4550-b284-af66cca313fb")]
public  ICSObjectList<DockerInstance>  Ass_DockerInstance{
get { return (ICSObjectList<DockerInstance>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_DockerInstance ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="210d20ab-7118-433c-aadf-451755ef98e2")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(AppType),DbFieldName ="Ass_AppType", AsscoisDestPropertyName="Ass_MicroServiceApp", AssociaID = "210d20ab-7118-433c-aadf-451755ef98e2")]
public  AppType  Ass_AppType{
get { return (AppType) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_AppType")]
public  int? Ass_AppType_Id {
get{  var x= getAssPropertyId("Ass_AppType"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_AppType");
}    }
public bool ShouldSerializeAss_AppType ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="776c41e6-f66a-4f66-a2dd-9e967ffc3fde")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<appVersion>, _baseObjectList<appVersion>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(appVersion), AsscoisDestPropertyName = "Ass_MicroServiceApp", AssociaID = "776c41e6-f66a-4f66-a2dd-9e967ffc3fde")]
public  ICSObjectList<appVersion>  Ass_appVersion{
get { return (ICSObjectList<appVersion>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_appVersion ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="7b2f56d6-eff4-4a8c-8174-9139f02301c2")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<webSite>, _baseObjectList<webSite>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(webSite), AsscoisDestPropertyName = "Ass_MicroServiceApp", AssociaID = "7b2f56d6-eff4-4a8c-8174-9139f02301c2")]
public  ICSObjectList<webSite>  Ass_webSite{
get { return (ICSObjectList<webSite>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_webSite ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="81faa811-090b-44b2-8c9a-6615c0e15f2b")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<Region>, _baseObjectList<Region>>))]
[AssociaField( RelationType =enum_relationType.moreToMore ,AssociaClass = typeof(RegionApps), AsscoisSourcePropertyName = "AssC_MicroServiceApp",AsscoisDestPropertyName = "AssC_Region", AssociaID = "81faa811-090b-44b2-8c9a-6615c0e15f2b")]
public  ICSObjectList<Region>  Ass_Region{
get { return (ICSObjectList<Region>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_Region ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="943526e8-deb6-436a-b5bc-41fb6c981d12")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<ReleaseTask>, _baseObjectList<ReleaseTask>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(ReleaseTask), AsscoisDestPropertyName = "Ass_MicroServiceApp", AssociaID = "943526e8-deb6-436a-b5bc-41fb6c981d12")]
public  ICSObjectList<ReleaseTask>  Ass_ReleaseTask{
get { return (ICSObjectList<ReleaseTask>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_ReleaseTask ()
{
return false;
}

}
}
