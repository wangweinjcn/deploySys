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
namespace deploySys.Model
{
      using   FrmLib.Extend;
        ///
        ///<summary></summary>
public   partial class HostResource : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="8741b8ca-eea9-4a5a-9bbe-e2ca58c7d5a4")]
        public  osMetrics  getPerformance()

{

            return null;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="b15a7116-a9e0-4cff-b84c-6dbf510ad121")]
        public  IList<int>  getAllocPorts(  Int32  count)

{
            var result = new List<int>();
            List<int> list1 = GetSpace().SpaceQuery<DockerInstance>().Where(a => a.Ass_HostResource == this && a.proxyPort>=this.allocFromPort).Select(a =>  a.proxyPort ).ToCommList().ToList();
             list1.Sort();
            var j = 0;
            for (int i = this.allocFromPort; i <= this.allocEndPort; i++)
            {

                if ( list1.Count()>j && list1[j] == i )
                {
                    j++;
                    continue;
                }
                else
                {
                    result.Add(i);
                    if (result.Count == count)
                        break;
                }
            }
            return result;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="beebf626-a267-4ca7-9b56-e619cad7bae0_OnConstructer")]
       public HostResource(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="beebf626-a267-4ca7-9b56-e619cad7bae0_OffConstructer")]
       [SerializationConstructor]
       public HostResource(): this(null)
{
 
       }





























































}
}
