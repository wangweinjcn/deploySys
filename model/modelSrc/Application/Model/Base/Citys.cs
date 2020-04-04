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
        ///城市</summary>
public   partial class Citys : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="16ce6fd5-e652-4585-9349-959692fda963")]
        public  Areas  GetAreasByKey(  String  areaKey)

{
            Citys cobj = this;
            var x = GetSpace().SpaceQuery<Areas>().Where(a => a.areaKey == areaKey && a.Ass_Citys==cobj).FirstOrDefault();
            return x;
 
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="c8c4afb5-8dcf-4c73-b3cc-bcbbb2c00d21")]
        public  Areas  AddAreas(  String  _areaKey,  String  _area,  String  _areaEn)

{
            Areas x = new Areas(GetSpace());
            x.areaKey = _areaKey;
            x.area = _area;
            x.areaEn = _areaEn;
            x.cityKey = this.cityKey;
            x.Ass_Citys = this;
            return x;
 
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="e0784b81-415e-4709-9dfa-99f124885cf2")]
        public  Areas  GetAreas(  String  _areaEn)

{
            Citys cobj = this;
            var x = GetSpace().SpaceQuery<Areas>().Where(a => a.areaEn == _areaEn && a.Ass_Citys == cobj).FirstOrDefault();
            return x;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="c5fb692d-20ad-4fae-adba-944b45a5b23a_OnConstructer")]
       public Citys(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="c5fb692d-20ad-4fae-adba-944b45a5b23a_OffConstructer")]
       [SerializationConstructor]
       public Citys(): this(null)
{
 
       }































































































































































































































































































































































































}
}
