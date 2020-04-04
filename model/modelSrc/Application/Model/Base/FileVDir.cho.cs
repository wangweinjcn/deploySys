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
      using   Application.Model.Base;
        ///
        ///<summary></summary>
[UmlElement(Id="23b5675e-e322-435a-bb55-baf04311e627")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"FileVDir\",   \"Id\": \"23b5675e-e322-435a-bb55-baf04311e627\",   \"FullName\": \"Application.Model.Base..FileVDir\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Memo\",       \"Id\": \"41ecbe23-890f-423b-9518-7b0cd642baa7\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"DownloadCount\",       \"Id\": \"644586f4-cee9-44f1-adb9-03dd5c410b8c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"isFile\",       \"Id\": \"6b1c2969-b491-474d-bd77-c09a5370923a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"checkbox\",       \"dataType\": \"boolean\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  }\r\n]\",       \"DisplayTag\": \"是否是文件\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Guid\",       \"Id\": \"9b223f69-6265-4997-9c55-638edd4f2d16\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"FileSize\",       \"Id\": \"b608d516-0afb-4149-9d0d-75e7488f1551\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int64\",       \"posRow\": 3,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Name\",       \"Id\": \"b6729408-9b7f-48e0-aeae-7bd0a5dcfba9\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 3,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Version\",       \"Id\": \"c2fe452c-c25a-4cdf-bd2d-eda46c437aeb\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 4,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class FileVDir: _baseObject {
[UmlElement(Id="FileVDir_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="41ecbe23-890f-423b-9518-7b0cd642baa7")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Memo{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="644586f4-cee9-44f1-adb9-03dd5c410b8c")]
[Required]
public Int32  DownloadCount{
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
        ///是否是文件</summary>
[UmlElement(Id="6b1c2969-b491-474d-bd77-c09a5370923a")]
[Display(Name="是否是文件")]
[Required]
public Boolean  isFile{
get { 
 var pValue= getPropertyValue();
Boolean res;
 if(pValue==null) 
    res= true;
  else
    res= (Boolean)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="9b223f69-6265-4997-9c55-638edd4f2d16")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Guid{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="b608d516-0afb-4149-9d0d-75e7488f1551")]
[Required]
public Int64  FileSize{
get { 
 var pValue= getPropertyValue();
Int64 res;
 if(pValue==null) 
    res= 0;
  else
    res= (Int64)   pValue;
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="b6729408-9b7f-48e0-aeae-7bd0a5dcfba9")]
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
        ///<summary></summary>
[UmlElement(Id="c2fe452c-c25a-4cdf-bd2d-eda46c437aeb")]
[Required]
public Int32  Version{
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
[UmlElement(Id="b141d540-28f8-4f52-80ef-cb69b58de322")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(PhysicalPath),DbFieldName ="Ass_PhysicalPath", AsscoisDestPropertyName="Ass_FileVDir", AssociaID = "b141d540-28f8-4f52-80ef-cb69b58de322")]
public  PhysicalPath  Ass_PhysicalPath{
get { return (PhysicalPath) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_PhysicalPath")]
public  int? Ass_PhysicalPath_Id {
get{  var x= getAssPropertyId("Ass_PhysicalPath"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_PhysicalPath");
}    }
public bool ShouldSerializeAss_PhysicalPath ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e737265e-87ba-4884-90be-ea646f946203")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<FileVDir>, _baseObjectList<FileVDir>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(FileVDir), AsscoisDestPropertyName = "Ass_FileVDir_parent", AssociaID = "e737265e-87ba-4884-90be-ea646f946203")]
public  ICSObjectList<FileVDir>  Ass_FileVDir_children{
get { return (ICSObjectList<FileVDir>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_FileVDir_children ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="e737265e-87ba-4884-90be-ea646f946203")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(FileVDir),DbFieldName ="Ass_FileVDir_parent", AsscoisDestPropertyName="Ass_FileVDir_children", AssociaID = "e737265e-87ba-4884-90be-ea646f946203")]
public  FileVDir  Ass_FileVDir_parent{
get { return (FileVDir) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_FileVDir_parent")]
public  int? Ass_FileVDir_parent_Id {
get{  var x= getAssPropertyId("Ass_FileVDir_parent"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_FileVDir_parent");
}    }
public bool ShouldSerializeAss_FileVDir_parent ()
{
return false;
}

}
}
