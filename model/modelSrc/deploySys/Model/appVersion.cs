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
        ///
        ///<summary></summary>
public   partial class appVersion : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="03cd8666-cf6f-4c79-9ae7-89b59e499399")]
        public  IList<FileItem>  CompareDiffFile(  IList<FileItem>  newFileList,  IList<FileItem>  oldFileList)

{
            List<FileItem> resultlist = new List<FileItem>();
            
            foreach (var obj in newFileList)
            {
                var remoteObj = (from x in oldFileList where string.Equals(obj.retationPath, x.retationPath, StringComparison.CurrentCultureIgnoreCase) && string.Equals(obj.fileName, x.fileName, StringComparison.CurrentCultureIgnoreCase) select x).FirstOrDefault();

                if (remoteObj!=null)
                {
                    
                    if (obj.needUpdate(remoteObj))
                        resultlist.Add(obj);
                    oldFileList.Remove(remoteObj);
                }
                else
                    resultlist.Add(obj);
            }
            foreach (var obj in oldFileList)
            {
                obj.remoteShouldDeleted = true;
                resultlist.Add(obj);
            }
            return resultlist;

        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="4879b7c1-18c7-43f6-b00d-d074f1f14602_OnConstructer")]
       public appVersion(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="4879b7c1-18c7-43f6-b00d-d074f1f14602_OffConstructer")]
       [SerializationConstructor]
       public appVersion(): this(null)
{
 
       }



















































}
}
