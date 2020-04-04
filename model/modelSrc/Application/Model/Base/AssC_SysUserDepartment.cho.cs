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
        ///<summary></summary>
[UmlElement(Id="AssC_SysUserDepartment")]
[Table("AssC_SysUserDepartment")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": null,   \"Id\": null,   \"FullName\": null,   \"colCountPerRow\": 0,   \"ModelKeyName\": null,   \"Struct\": [] }")]
[MessagePackObject(true)]
public   partial class AssC_SysUserDepartment: _baseObject {
[UmlElement(Id="AssC_SysUserDepartment_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="2d40084d-43a9-4449-aab7-afea054c47ce")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "AssC_SysUser",  AssociaClass = typeof(SysUser), AssociaID = "2d40084d-43a9-4449-aab7-afea054c47ce")]
public  SysUser  AssC_SysUser{
get { return (SysUser) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("AssC_SysUser")]
public  int AssC_SysUser_Id {
get{  var x= getAssPropertyId("AssC_SysUser"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"AssC_SysUser");
}    }
public bool ShouldSerializeAssC_SysUser ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="ccc3e198-97df-4bf6-8764-27a1dd9e2065")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "AssC_Department",  AssociaClass = typeof(Department), AssociaID = "ccc3e198-97df-4bf6-8764-27a1dd9e2065")]
public  Department  AssC_Department{
get { return (Department) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("AssC_Department")]
public  int AssC_Department_Id {
get{  var x= getAssPropertyId("AssC_Department"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"AssC_Department");
}    }
public bool ShouldSerializeAssC_Department ()
{
return false;
}

}
}
