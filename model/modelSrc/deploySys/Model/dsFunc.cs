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
      using   Application.Model.Base;
      using   System.IO;
      using   System.Reflection;
      using   System.Security.Cryptography;
        ///
        ///<summary></summary>
public   partial class dsFunc : Application.Model.Base.BaseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="b35f59a9-85fb-4a6d-b9c6-e43aded04d45")]
        public  static   void   initPathToFileItem(  appVersion  appVer,  String  baseDir,  String  processPath,  Boolean  createFile,  IList<string>  conffile,  IList<FileItem>  allFItems)

{
            if (createFile && appVer.GetSpace() == null)
                throw new Exception("createFile and  space is null ");
           
               
            var relationBDir = processPath.Replace(baseDir,"");
            relationBDir = relationBDir.TrimStart(System.IO.Path.DirectorySeparatorChar);
            DirectoryInfo Dir = new DirectoryInfo(processPath);
            FileInfo[] allfiles = Dir.GetFiles();
          
            foreach (var obj in allfiles)
            {
                if (obj.FullName.EndsWith(".pdb", StringComparison.InvariantCultureIgnoreCase))
                    continue;
                var fn = Path.GetFileName(obj.FullName);

                FileItem fi = new FileItem(appVer.GetSpace());
                var content = File.ReadAllBytes(obj.FullName);
                if (createFile)
                {
                    
                    SysFunc sf = SysFunc.getInstance(appVer.GetSpace());                   
                    var fobj = sf.AddNewFile(content, fn);
                     fi.Guid = fobj.Guid;
                }
                fi.fileName = fn;
                fi.fileSize = content.Length;
                fi.retationPath = relationBDir;
                fi.isConfFile = (from x in conffile where relationBDir.StartsWith(x,StringComparison.CurrentCultureIgnoreCase) 
                                 || fn.EndsWith(x,StringComparison.CurrentCultureIgnoreCase) select x).Count() > 0; //判断是否是配置文件
                fi.Ass_appVersion = appVer;
                allFItems.Add(fi);
              

                using (var calculator = MD5.Create())
                {
                    MemoryStream fstream = new MemoryStream(content);
                    byte[] hashByte1 =calculator.ComputeHash(fstream);//哈希算法根据文本得到哈希码的字节数组 
                    fi.hashCode = BitConverter.ToString(hashByte1);//将字节数组装换为字符串 
                    BinaryReader r = new BinaryReader(fstream);
                    r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开

                    byte[] fcontent = r.ReadBytes((int)r.BaseStream.Length);
                    if (fi.haveDllVersion())
                    {
                        try
                        {
                           var an= AssemblyName.GetAssemblyName(obj.FullName);
                           
                            fi.dllVersion = an.Version.ToString();
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine("may be not assembly:{0}", exp.Message);
                        }
                    }

                    fstream.Close();
                }

            }
            DirectoryInfo[] DirSub = Dir.GetDirectories();
            foreach (var obj in DirSub)
            {
                initPathToFileItem(appVer, baseDir, obj.FullName,createFile,conffile, allFItems);
            }
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="c2a412be-44c0-4b7e-99dd-7f7aebe63385")]
        public   void   InitParams()

{
            IContextSpace sp = this.GetSpace();
            SysFunc sf = SysFunc.getInstance(sp);
            sf.InitParams();
            var z = (from x in this.GetSpace().SpaceQuery<SysParams>() where (x.Value == "2" && x.Key == "Meetinginit") select x).ToList();
            if (z == null || z.Count == 0)
            {
                SysParams newobj = new SysParams(sp);
                newobj.Key = "Meetinginit";
                newobj.Value = "2";
                newobj.Category = "businese";
                PhysicalPath pp = new PhysicalPath(sp);
                pp.Level = 0;
                pp.Name = "/opt/data/fsw/depS";

                var role = sf.AddSysRole("平台管理员", "admins", true);
                role.AddUser("admin", "1234567890", "admin", "manager".ToMD5(), "admin@site.com", (int)EnumSex.other, (int)EnumUserType.SysUser, "", "*");
                sf.AddSysRole("运营管理", "OssAdmin", false);
                sf.AddSysRole("运营角色1", "OssRole1", false);

                sf.AddSysRole("客户", "custUser", false);
                sp.UpdateAllDirtyObjects();


            }

        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="7dec5c42-b6f7-4ec8-86f2-3791dcb08fd8_getInstance")]
       public static  dsFunc getInstance(IContextSpace space)
{
dsFunc instance = null;
            if (space.SingletonDic.ContainsKey(typeof(dsFunc).Name))
                instance = space.SingletonDic[typeof(dsFunc).Name] as dsFunc;
            else
            {
                lock (_lockObj)
                {
                    instance = new dsFunc(space);
                    if (!space.SingletonDic.TryAdd(typeof(dsFunc).Name, instance))
                    {
                        if (space.SingletonDic.ContainsKey(typeof(dsFunc).Name))
                            space.SingletonDic[typeof(dsFunc).Name] = instance;
                    }
                }
            }
            return instance;
 
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="7dec5c42-b6f7-4ec8-86f2-3791dcb08fd8_OnConstructer")]
       public dsFunc(IContextSpace space): base(space)
{
 
       }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="7dec5c42-b6f7-4ec8-86f2-3791dcb08fd8_OffConstructer")]
       [SerializationConstructor]
       public dsFunc(): this(null)
{
 
       }
       ///<summary>
       ///                                                                                                    获取指定目录下，指定文件名的目录                                                                                                    
       ///</summary>
        ///<param name="basepath"></param>
        ///<param name="filename"></param>
        ///<returns></returns>
public static string findMainFileDir(string basepath, string filename)     
 {
            if (!Directory.Exists(basepath))
                return null;
            DirectoryInfo Dir = new DirectoryInfo(basepath);
            FileInfo[] allfiles = Dir.GetFiles();
            foreach (var obj in allfiles)
            {
                var fn = Path.GetFileName(obj.FullName);
                if (string.Equals(filename, fn, StringComparison.CurrentCultureIgnoreCase))
                {
                    var fpath = Path.GetDirectoryName(obj.FullName);
                    return fpath;
                }
            }
            DirectoryInfo[] DirSub = Dir.GetDirectories();
            foreach (var obj in DirSub)
            {
                return findMainFileDir(obj.FullName, filename);
            }
            return null;
        }













}
}
