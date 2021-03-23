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
public   partial class Menu : Application.Model.Base.BaseObject{
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="0765c757-0662-45f2-bd78-2c3baddf2077_OnConstructer")]
       public Menu(IContextSpace space): base(space)
{
            this.SortCode = 0;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="0765c757-0662-45f2-bd78-2c3baddf2077_OffConstructer")]
       [SerializationConstructor]
       public Menu(): this(null)
{
            this.SortCode = 0;

        }


























































































































































































































































































































































































































}
}