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
public   partial class OneObjectCustFields : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="3c12e362-0215-43bd-92db-336c12cae687_OnConstructer")]
       public OneObjectCustFields(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="3c12e362-0215-43bd-92db-336c12cae687_OffConstructer")]
       [SerializationConstructor]
       public OneObjectCustFields(): this(null)
{
 
       }


























































































































































































































































































































































































































}
}