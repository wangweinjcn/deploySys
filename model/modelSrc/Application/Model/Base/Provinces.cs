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
public   partial class Provinces : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="59a98d73-0716-4a01-9283-7b69b23a2b6e")]
        public  Citys  GetCitys(  String  cityEn)

{
            var pobj = this;
            var x = GetSpace().SpaceQuery<Citys>().Where(a => a.Ass_Provinces == pobj
            && a.cityEn == cityEn).FirstOrDefault();
            return x;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="9423f93c-9367-457d-a780-6dfb599356d5")]
        public  Citys  GetCitysByKey(  String  cityKey)

{
            var pobj = this;
            var x = GetSpace().SpaceQuery<Citys>().Where(a => a.Ass_Provinces == pobj
            && a.cityKey == cityKey).FirstOrDefault();
            return x;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="dafd537c-d942-4d39-a67c-a3ac2fbc1056")]
        public  Citys  AddCitys(  String  _city,  String  _cityKey,  String  _Ename)

{
            Citys  x= new Citys(GetSpace());
            x.city = _city;
            x.cityEn = _Ename;
            x.cityKey = _cityKey;
            x.provinceKey = this.provinceKey;
            x.Ass_Provinces = this;
            return x;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="0bdd9bc5-317d-4e43-ad49-1e85b148d216_OnConstructer")]
       public Provinces(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="0bdd9bc5-317d-4e43-ad49-1e85b148d216_OffConstructer")]
       [SerializationConstructor]
       public Provinces(): this(null)
{
 
       }
        partial void CountryIdDerive(ref string res)
        {

      /*      if (this.Ass_Country != null)
                res = this.Ass_Country.Id.ToString();
                */
        }
























































































































































































































































































































































































































}
}
