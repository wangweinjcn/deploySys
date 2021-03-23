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
public   partial class CustomFieldScheme : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="968ce673-108d-48ac-97b0-9ae8b668f27f_OnConstructer")]
       public CustomFieldScheme(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="968ce673-108d-48ac-97b0-9ae8b668f27f_OffConstructer")]
       [SerializationConstructor]
       public CustomFieldScheme(): this(null)
{
 
       }



























































































































































































































































































































































































































}
}
