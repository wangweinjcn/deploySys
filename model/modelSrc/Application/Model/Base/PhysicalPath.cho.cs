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
        ///物理存储路径</summary>
[UmlElement(Id="1578fe49-51f2-45dc-8b68-6b1f2bb10bd0")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"PhysicalPath\",   \"Id\": \"1578fe49-51f2-45dc-8b68-6b1f2bb10bd0\",   \"FullName\": \"Application.Model.Base..PhysicalPath\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Level\",       \"Id\": \"3fd5a39a-fc4f-440c-b6e2-b5e81a324d67\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Name\",       \"Id\": \"b8c1bbc6-bcc3-429c-93bc-23b72b48d143\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"FileCount\",       \"Id\": \"c1424103-96a0-42d6-afde-a81a0f6b6a1a\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"int32\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[\r\n  {\r\n    \"required\": true,\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"必填内容\"\r\n  },\r\n  {\r\n    \"type\": \"integer\",\r\n    \"trigger\": \"blur\",\r\n    \"message\": \"请输入整数\"\r\n  }\r\n]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class PhysicalPath: _baseObject {
[UmlElement(Id="PhysicalPath_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="3fd5a39a-fc4f-440c-b6e2-b5e81a324d67")]
[Required]
public Int32  Level{
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
[UmlElement(Id="b8c1bbc6-bcc3-429c-93bc-23b72b48d143")]
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
[UmlElement(Id="c1424103-96a0-42d6-afde-a81a0f6b6a1a")]
[Required]
public Int32  FileCount{
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
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<FileVDir>, _baseObjectList<FileVDir>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(FileVDir), AsscoisDestPropertyName = "Ass_PhysicalPath", AssociaID = "b141d540-28f8-4f52-80ef-cb69b58de322")]
public  ICSObjectList<FileVDir>  Ass_FileVDir{
get { return (ICSObjectList<FileVDir>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_FileVDir ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="bc6cb0c7-3936-4d05-bfbf-d32ba34febc1")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<PhysicalPath>, _baseObjectList<PhysicalPath>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(PhysicalPath), AsscoisDestPropertyName = "Ass_LocalPath_parent", AssociaID = "bc6cb0c7-3936-4d05-bfbf-d32ba34febc1")]
public  ICSObjectList<PhysicalPath>  Ass_LocalPath_children{
get { return (ICSObjectList<PhysicalPath>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_LocalPath_children ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="bc6cb0c7-3936-4d05-bfbf-d32ba34febc1")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(PhysicalPath),DbFieldName ="Ass_LocalPath_parent", AsscoisDestPropertyName="Ass_LocalPath_children", AssociaID = "bc6cb0c7-3936-4d05-bfbf-d32ba34febc1")]
public  PhysicalPath  Ass_LocalPath_parent{
get { return (PhysicalPath) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_LocalPath_parent")]
public  int? Ass_LocalPath_parent_Id {
get{  var x= getAssPropertyId("Ass_LocalPath_parent"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_LocalPath_parent");
}    }
public bool ShouldSerializeAss_LocalPath_parent ()
{
return false;
}

}
}
