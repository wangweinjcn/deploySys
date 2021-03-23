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
public   partial class CustField : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="1076bde0-9eca-4111-a6b2-bb72d6b5e3ca_OnConstructer")]
       public CustField(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="1076bde0-9eca-4111-a6b2-bb72d6b5e3ca_OffConstructer")]
       [SerializationConstructor]
       public CustField(): this(null)
{
 
       }


























































































































































































































































































































































































































}
}
