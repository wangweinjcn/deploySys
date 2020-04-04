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
        ///<summary>
        ///
        ///自定义序列号器；需要增加key和paramasvalue的唯一索引</summary>
public   partial class IDClass : _baseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="b69b0cc1-e118-47dd-ad36-02a9036e74db_OnConstructer")]
       public IDClass(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="b69b0cc1-e118-47dd-ad36-02a9036e74db_OffConstructer")]
       [SerializationConstructor]
       public IDClass(): this(null)
{
 
       }































































































































































































































































































































































































}
}
