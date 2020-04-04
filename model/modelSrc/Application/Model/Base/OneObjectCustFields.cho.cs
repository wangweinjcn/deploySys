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
[UmlElement(Id="3c12e362-0215-43bd-92db-336c12cae687")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"OneObjectCustFields\",   \"Id\": \"3c12e362-0215-43bd-92db-336c12cae687\",   \"FullName\": \"Application.Model.Base..OneObjectCustFields\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [] }")]
[MessagePackObject(true)]
public   partial class OneObjectCustFields: _baseObject {
[UmlElement(Id="OneObjectCustFields_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="4f09845e-7414-4351-ba14-ae4af77023a8")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
 [JsonConverter(typeof(InterfaceConverter<ICSObjectList<CustFieldValue>, _baseObjectList<CustFieldValue>>))]
[AssociaField( RelationType =enum_relationType.moreOfOneToMore,   AssociaClass = typeof(CustFieldValue), AsscoisDestPropertyName = "Ass_OneObjectCustFields", AssociaID = "4f09845e-7414-4351-ba14-ae4af77023a8")]
public  ICSObjectList<CustFieldValue>  Ass_CustFieldValue{
get { return (ICSObjectList<CustFieldValue>) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_CustFieldValue ()
{
return false;
}

}
}
