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
        ///内部OSS账号的归属部门</summary>
[UmlElement(Id="904817ce-d9d6-4ab4-a89d-13fc50072a83")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"Department\",   \"Id\": \"904817ce-d9d6-4ab4-a89d-13fc50072a83\",   \"FullName\": \"Application.Model.Base..Department\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"Key\",       \"Id\": \"59c32074-a135-4a54-9c04-deead6d2f857\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"OwnerUserId\",       \"Id\": \"d6dd5c63-ca6c-468b-8925-d3cd14eba3a8\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"部门经理用户ID\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Name\",       \"Id\": \"e2c7c93b-3ac6-40eb-9e44-465fabc3bebd\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 2,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class Department: _baseObject {
[UmlElement(Id="Department_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="59c32074-a135-4a54-9c04-deead6d2f857")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Key{
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
        ///部门经理用户ID</summary>
[UmlElement(Id="d6dd5c63-ca6c-468b-8925-d3cd14eba3a8")]
[Display(Name="部门经理用户ID")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  OwnerUserId{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="e2c7c93b-3ac6-40eb-9e44-465fabc3bebd")]
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
[UmlElement(Id="1ebef277-56d4-4f94-9344-684d4cec410a")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<SysUser>, _baseObjectList<SysUser>>))]
[AssociaField( RelationType =enum_relationType.moreToMore ,AssociaClass = typeof(AssC_SysUserDepartment), AsscoisSourcePropertyName = "AssC_Department",AsscoisDestPropertyName = "AssC_SysUser", AssociaID = "1ebef277-56d4-4f94-9344-684d4cec410a")]
public  ICSObjectList<SysUser>  Ass_SysUser{
get { return (ICSObjectList<SysUser>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_SysUser ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="95ba25c8-4c9f-43f6-89fe-d5de9067f7ca")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<Department>, _baseObjectList<Department>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(Department), AsscoisDestPropertyName = "Ass_Department_parent", AssociaID = "95ba25c8-4c9f-43f6-89fe-d5de9067f7ca")]
public  ICSObjectList<Department>  Ass_Department_children{
get { return (ICSObjectList<Department>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_Department_children ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="95ba25c8-4c9f-43f6-89fe-d5de9067f7ca")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(Department),DbFieldName ="Ass_Department_parent", AsscoisDestPropertyName="Ass_Department_children", AssociaID = "95ba25c8-4c9f-43f6-89fe-d5de9067f7ca")]
public  Department  Ass_Department_parent{
get { return (Department) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_Department_parent")]
public  int? Ass_Department_parent_Id {
get{  var x= getAssPropertyId("Ass_Department_parent"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_Department_parent");
}    }
public bool ShouldSerializeAss_Department_parent ()
{
return false;
}

}
}
