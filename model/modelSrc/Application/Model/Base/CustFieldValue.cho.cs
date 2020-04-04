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
[UmlElement(Id="c26c61ca-ffc6-4281-9521-f69365c6bfbc")]
[MonitorProperty]
[ClassViewConf("{   \"Name\": \"CustFieldValue\",   \"Id\": \"c26c61ca-ffc6-4281-9521-f69365c6bfbc\",   \"FullName\": \"Application.Model.Base..CustFieldValue\",   \"colCountPerRow\": 2,   \"ModelKeyName\": \"Id\",   \"Struct\": [     {       \"Name\": \"LargeValue\",       \"Id\": \"3a017cf7-3e95-4cd9-ae0d-2aea435a242c\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 2,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     },     {       \"Name\": \"Value\",       \"Id\": \"a241bb1c-4e3e-4b21-b07b-10a27d90dd46\",       \"Disable\": false,       \"isTableFields\": false,       \"Visible\": false,       \"rowCount\": 0,       \"colCount\": 0,       \"editorType\": \"input\",       \"dataType\": \"string\",       \"posRow\": 1,       \"posCol\": 1,       \"tabOrder\": 0,       \"ValidTag\": \"[]\",       \"DisplayTag\": \"\",       \"Value\": null,       \"options\": null,       \"optionFunc\": null     }   ] }")]
[MessagePackObject(true)]
public   partial class CustFieldValue: _baseObject {
[UmlElement(Id="CustFieldValue_ID")]
[Column(IsPrimaryKey = true)]
[AutoIncrement]
[NotApplyFromOffice]
public override int Id { get; set; }
        ///
        ///<summary></summary>
[UmlElement(Id="3a017cf7-3e95-4cd9-ae0d-2aea435a242c")]
[StringLength(2147483647,ErrorMessage ="超过最大长度")]
public String  LargeValue{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="a241bb1c-4e3e-4b21-b07b-10a27d90dd46")]
[StringLength(225,ErrorMessage ="超过最大长度")]
public String  Value{
get { 
 var pValue= getPropertyValue();
String res;
res= (String)   pValue; 
return res;}
 set{ setPropertyValue(value);
}}

        ///
        ///<summary></summary>
[UmlElement(Id="4c1d2db4-97b4-4ba6-8d55-cc8929119c24")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.nosaveOfOneToOne,   AssociaClass = typeof(CustField), AssociaID = "4c1d2db4-97b4-4ba6-8d55-cc8929119c24")]
public  CustField  Ass_CustField{
get { return (CustField) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
public bool ShouldSerializeAss_CustField ()
{
return false;
}

        ///
        ///<summary></summary>
[UmlElement(Id="4f09845e-7414-4351-ba14-ae4af77023a8")]
[MonitorProperty]
[BindNever]
[IgnoreMember]
[AssociaField( RelationType =enum_relationType.oneOfOneToMore,   AssociaClass = typeof(OneObjectCustFields),DbFieldName ="Ass_OneObjectCustFields", AsscoisDestPropertyName="Ass_CustFieldValue", AssociaID = "4f09845e-7414-4351-ba14-ae4af77023a8")]
public  OneObjectCustFields  Ass_OneObjectCustFields{
get { return (OneObjectCustFields) getPropertyValue()  ;}
 set{ setPropertyValue(value);}}
[Column("Ass_OneObjectCustFields")]
public  int? Ass_OneObjectCustFields_Id {
get{  var x= getAssPropertyId("Ass_OneObjectCustFields"); 
if(x==System.DBNull.Value || x==null ) 
      return -1;
  else 
     return Convert.ToInt32(x);  } 
 set {
setAssicationId(value,"Ass_OneObjectCustFields");
}    }
public bool ShouldSerializeAss_OneObjectCustFields ()
{
return false;
}

}
}
