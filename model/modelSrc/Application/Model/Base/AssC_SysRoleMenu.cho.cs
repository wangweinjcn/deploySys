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
[UmlElement(Id="AssC_SysRoleMenu")]
[Table("AssC_SysRoleMenu")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": null,   \"Id\": null,   \"FullName\": null,   \"colCountPerRow\": 0,   \"ModelKeyName\": null,   \"Struct\": [] }")]
[MessagePackObject(true)]
public   partial class AssC_SysRoleMenu: _baseObject {
[UmlElement(Id="AssC_SysRoleMenu_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="a12ebca4-f55f-46c8-8263-7b30454cf262")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "AssC_SysRole",  AssociaClass = typeof(SysRole), AssociaID = "a12ebca4-f55f-46c8-8263-7b30454cf262")]
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
[UmlElement(Id="b4958372-d131-41a7-90d9-48b94ad6db65")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.compositeOfOneToOne, DbFieldName = "AssC_Menu",  AssociaClass = typeof(Menu), AssociaID = "b4958372-d131-41a7-90d9-48b94ad6db65")]
public  Menu  AssC_Menu{
get { return (Menu) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("AssC_Menu")]
public  int AssC_Menu_Id {
get{  var x= getAssPropertyId("AssC_Menu"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"AssC_Menu");
}    }
public bool ShouldSerializeAssC_Menu ()
{
return false;
}

}
}
