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
public   partial class FileItem : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="3c2c0348-7fba-4db8-ae6e-913c69c9b589")]
        public  InstallFileInfo  toInstallFileInfo()

{
            InstallFileInfo fi = new InstallFileInfo();
            fi.basepath = this.retationPath;
            fi.filename = this.fileName;
            fi.hashcode = this.hashCode;
            fi.Dllversion = this.dllVersion;
            return fi;
 
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="a1982c48-b4b6-4814-af45-4243b369716e")]
        public   void   valueFrom(  InstallFileInfo  fi)

{
 
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="bca18ee0-7fe8-4d45-a0ab-4c6dd6437a79")]
        public  Boolean  needUpdate(  FileItem  destfile)

{
            if (this.hashCode == destfile.hashCode)
                return false;
            else
            {
                if (haveDllVersion())
                {
                    if (this.fileName == destfile.fileName && tools_static.CompareVersion(this.dllVersion, destfile.dllVersion) > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="dc18dc69-a919-42d7-9867-a245034e85a7")]
        public  Boolean  haveDllVersion()

{
            var extname = System.IO.Path.GetExtension(this.fileName);
            if (string.Equals(extname, ".dll", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(extname, ".exe", StringComparison.CurrentCultureIgnoreCase))
                return true;
            else
                return false;

        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="ae42d7f3-0413-4f1f-9057-ba7d78d88613_OnConstructer")]
       public FileItem(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="ae42d7f3-0413-4f1f-9057-ba7d78d88613_OffConstructer")]
       [SerializationConstructor]
       public FileItem(): this(null)
{
 
       }















}
}
