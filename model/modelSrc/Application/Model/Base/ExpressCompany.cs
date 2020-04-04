﻿using   Chloe.Entity;  
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
public   partial class ExpressCompany : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="873debc0-260c-40b6-a1f2-8a4d1b2177c5_OnConstructer")]
       public ExpressCompany(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="873debc0-260c-40b6-a1f2-8a4d1b2177c5_OffConstructer")]
       [SerializationConstructor]
       public ExpressCompany(): this(null)
{
 
       }































































































































































































































































































































































































}
}
