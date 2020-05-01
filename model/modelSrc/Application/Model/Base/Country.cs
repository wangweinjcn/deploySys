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
      using   FrmLib.Extend;
        ///
        ///<summary></summary>
public   partial class Country : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="25979b86-38ee-488a-9e37-9c18720a6d00")]
        public  Provinces  AddProvinces(  String  _province,  String  _provinceKey,  String  _pen)

{
            var x = new Provinces(GetSpace());
            x.province = _province;
            x.provinceKey = _provinceKey;
            x.provinceEn = _pen;
            x.Ass_Country = this;
            return x;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="84ae73f3-92c0-4528-871a-8ca0da1b1a11")]
        public  Provinces  GetProvinces(  String  provincesen)

{
            var x = GetSpace().SpaceQuery<Provinces>().Where(a => a.provinceEn == provincesen).FirstOrDefault();
            return x;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="e573b618-b3b1-4c2d-a748-6ee54fa62f8a")]
        public  Provinces  GetProvincesByKey(  String  provincesenKey)

{
            var cobj = this;
            var x = GetSpace().SpaceQuery<Provinces>().Where(a =>
            a.provinceKey == provincesenKey && a.Ass_Country == cobj).FirstOrDefault();
            return x;
        }
        partial void NameCnWrite(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
           

        }
        partial void NameEnWrite(string value)
        {
           
           
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="1ae05350-740c-4565-9722-463ad89361bc_OnConstructer")]
       public Country(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="1ae05350-740c-4565-9722-463ad89361bc_OffConstructer")]
       [SerializationConstructor]
       public Country(): this(null)
{
 
       }

















































































































































































































































































































































































































}
}
