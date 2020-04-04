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
[UmlElement(Id="AssC_SysRoleSysUser")]
[Table("AssC_SysRoleSysUser")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": null,   \"Id\": null,   \"FullName\": null,   \"colCountPerRow\": 0,   \"ModelKeyName\": null,   \"Struct\": [] }")]
[MessagePackObject(true)]
public   partial class AssC_SysRoleSysUser: _baseObject {
[UmlElement(Id="AssC_SysRoleSysUser_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="ad98fdf7-b8c5-4912-9467-70b3168350f0")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "AssC_SysRole",  AssociaClass = typeof(SysRole), AssociaID = "ad98fdf7-b8c5-4912-9467-70b3168350f0")]
public  SysRole  AssC_SysRole{
get { return (SysRole) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("AssC_SysRole")]
public  int AssC_SysRole_Id {
get{  var x= getAssPropertyId("AssC_SysRole"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"AssC_SysRole");
}    }
public bool ShouldSerializeAssC_SysRole ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="00e6f1f4-5ae7-4076-8bb9-632c2f037f94")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "AssC_SysUser",  AssociaClass = typeof(SysUser), AssociaID = "00e6f1f4-5ae7-4076-8bb9-632c2f037f94")]
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

}
}
