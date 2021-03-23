using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Timers;
using System.Runtime.InteropServices;

namespace FrmLib.Extend
{
    /// <summary>
    /// 除去.bak 和.log文件都处理
    /// </summary>
    public class InstallFileInfo
    {
        /// <summary>
        /// 文件名（不含路径）
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string relationDir { get; set; }
        /// <summary>
        /// dotnet程序集的版本号
        /// </summary>
        public string Dllversion { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime lastwriteDt { get; set; }
        /// <summary>
        /// 文件相对路径和文件名的哈希值
        /// </summary>
        public string hashcode { get; set; }
        /// <summary>
        /// 文件内容哈希
        /// </summary>
        public string contentHash { get; set; }
        public byte[] filecontent { get; set; }
        public bool isdelete { get; set; }
        public string oldHashCode { get; set; }
        public InstallFileInfo(string _filename, string _basepath, string _hashcode) : this()
        {
            this.filename = _filename;
            this.relationDir = _basepath;
            this.hashcode = _hashcode;


        }
        public InstallFileInfo()
        {
            this.isdelete = false;
        }
        public InstallFileInfo(string basePath, string _filename, string _relationPath, bool fillcontent) : this()
        {

            this.filename = _filename;
            this.relationDir = _relationPath;
            updateMe(basePath, fillcontent);
        }
        /// <summary>
        /// GZip解压函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] GZipDecompress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    byte[] bytes = new byte[40960];
                    int n;
                    while ((n = gZipStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        stream.Write(bytes, 0, n);
                    }
                    gZipStream.Close();
                }
                return stream.ToArray();
            }
        }
        /// <summary>
        /// GZip压缩函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] GZipCompress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
                {
                    gZipStream.Write(data, 0, data.Length);
                    gZipStream.Close();
                }
                return stream.ToArray();
            }
        }
        public void updateMe(string _basePath, bool needfillcontent)
        {
            //  FrmLib.Log.commLoger.devLoger.Debug(string.Format("updateMe:{0},{1},{2}",basepath,filename,needfillcontent));
            string fullfilename = System.IO.Path.Combine(_basePath, relationDir, filename);
            var hashString = string.Format("{0}-{1}", relationDir.Replace(System.IO.Path.DirectorySeparatorChar, '-'), filename.Replace(System.IO.Path.DirectorySeparatorChar, '-'));
            if(!File.Exists(fullfilename))
                return;
            this.lastwriteDt = File.GetLastWriteTime(fullfilename);               
            
            {
                this.hashcode = hashString.ToMD5();
                this.contentHash = tools_static.getMD5ByHashAlgorithm(fullfilename);//将字节数组装换为字符串 
                FileStream fstream = File.Open(fullfilename, FileMode.Open, FileAccess.Read, FileShare.Read);
               
                BinaryReader r = new BinaryReader(fstream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                
                byte[] fcontent = r.ReadBytes((int)r.BaseStream.Length);
                if (filename.EndsWith(".dll") || filename.EndsWith(".exe"))
                {
                    try
                    {
                        var am = AssemblyName.GetAssemblyName(fullfilename);
                        this.Dllversion = am.Version.ToString();
                        
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("may be not assembly:{0}", exp.Message);
                    }
                }
                if (needfillcontent)
                {
                    filecontent = GZipCompress(fcontent);
                   //  FrmLib.Log.commLoger.devLoger.Debug(string.Format("filecontent:{0},{1},{2}",filename,fcontent==null?-1:fcontent.Length,filecontent==null?-1:filecontent.Length));
                }

                fstream.Close();
            }
        }
        
          public bool NeedUpdate(InstallFileInfo destfile)
        {
            if (this.contentHash == destfile.contentHash)
                return false;
            else
            {
                if ((this.filename.EndsWith(".dll") || this.filename.EndsWith(".exe")) && this.Dllversion!=null && destfile.Dllversion!=null )
                {
                    if (this.filename == destfile.filename && tools_static.CompareVersion(this.Dllversion, destfile.Dllversion) > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
        }
    }
    public class configUpdate
    {
        public Dictionary<string, string> addkeyDic;
        public List<string> removekeyList;
        public configUpdate()
        {
            addkeyDic = new Dictionary<string, string>();
            removekeyList = new List<string>();
        }
    }
    /// <summary>
    /// 文件的自动更新
    /// 
    /// </summary>
   public class SoftUpdateFiles
    {
       public List<InstallFileInfo> softfile { get; set; }
       public configUpdate softConf;
        /// <summary>
        /// 起始目录，用于生成相对路径
        /// </summary>
        private string dir { get; set; }
        private bool needFillContent;
       private static SoftUpdateFiles _instance;
        private static object _lockObj = new object();
        private bool enableWatcher = false;
        FileSystemWatcher watcher;
#if NETCORE
      //  private Microsoft.Extensions.FileProviders.PhysicalFileProvider _fileProvider;
#endif
        //是否有效
        private bool isEnable { get;  set; }
        private Timer checkFileChangeTimer { get;  set; }
        //上次文件变更时间
        private DateTime lastChange;


        private void watchDir()
        {
         
            
                watcher = new FileSystemWatcher();
                watcher.Path = dir;
                watcher.IncludeSubdirectories = true;
                watcher.NotifyFilter = NotifyFilters.Attributes |
                                       NotifyFilters.CreationTime |
                                       NotifyFilters.DirectoryName |
                                       NotifyFilters.FileName |
                                       NotifyFilters.LastAccess |
                                       NotifyFilters.LastWrite |
                                       NotifyFilters.Security |
                                       NotifyFilters.Size;
                watcher.Filter = "*.*";
                watcher.Changed += new FileSystemEventHandler((o, e) =>
                {
                    FrmLib.Log.commLoger.runLoger.Info(e.FullPath + ";now changed");
                    if (!File.Exists(e.FullPath))
                        return;
                    if (!validFileType(e.FullPath))
                        return;
                   
                    lastChange = DateTime.Now;
                    var fi = procesOneFile(this.dir,e.FullPath);
                    var oldfi = (from x in softfile where x.relationDir == fi.relationDir && x.filename == fi.filename select x).FirstOrDefault();
                    if (oldfi != null)
                        softfile.Remove(oldfi);
                    softfile.Add(fi);

                });
                watcher.Deleted += new FileSystemEventHandler((o, e) =>
                {
                    FrmLib.Log.commLoger.runLoger.Info(e.FullPath + ";now Deleted");
                    if (!validFileType(e.FullPath))
                        return;
                   
                    lastChange = DateTime.Now;
                    var fi = procesOneFile(this.dir,e.FullPath);
                    var oldfi = (from x in softfile where x.relationDir == fi.relationDir && x.filename == fi.filename select x).FirstOrDefault();
                    if (oldfi != null)
                        softfile.Remove(oldfi);
                });
                watcher.Created += new FileSystemEventHandler((o, e) =>
                {
                    FrmLib.Log.commLoger.runLoger.Info(e.FullPath + ";now created");
                    if (!File.Exists(e.FullPath))
                        return;
                    if (!validFileType(e.FullPath))
                        return;
                   
                    lastChange = DateTime.Now;
                    var fi = procesOneFile(this.dir,e.FullPath);
                    softfile.Add(fi);

                });
            
            //Start monitoring.

        }
       private static SoftUpdateFiles getinstnace()
       {
            if (_instance == null)
            {
                lock (_lockObj)
                {
                    if(_instance==null)
                        _instance = new SoftUpdateFiles();
                }
            }
           return _instance;
       
       }
       private bool validFileType(string onefile)
       {
            if (onefile.EndsWith(".bak") || onefile.EndsWith(".log")
                 || onefile.EndsWith(".swp") || onefile.EndsWith(".pdb"))
                return false;
            return true;
           if (onefile.EndsWith(".dll") || onefile.EndsWith(".exe") || onefile.EndsWith(".so") ||
                      onefile.EndsWith(".sh") || onefile.EndsWith(".py") || onefile.EndsWith(".bat"))
               return true;
           else
               return false;
       
       }
        private void checkInitFiles(Object state, System.Timers.ElapsedEventArgs e)
        {
            if (!isEnable && (DateTime.Now - lastChange).TotalMinutes > 2)
            {
                watcher.EnableRaisingEvents = false;
                this.initFiles(dir,needFillContent,enableWatcher);
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="onefile">文件全路径</param>
        /// <returns></returns>
        private InstallFileInfo procesOneFile(string _basePath,string onefile)
        {
           var relatPath = System.IO.Path.GetDirectoryName(onefile).Replace(this.dir.TrimEnd(System.IO.Path.DirectorySeparatorChar), "");
            relatPath = relatPath.TrimStart(System.IO.Path.DirectorySeparatorChar);
           var filename = System.IO.Path.GetFileName(onefile);
          //    FrmLib.Log.commLoger.runLoger.Info(string.Format("procesOneFile file :{0},{1},{2}",_basePath,relatPath,onefile));
             InstallFileInfo fi = new InstallFileInfo(_basePath,filename, relatPath, this.needFillContent);
            return fi;

        }
        private void iterInitDirs(string basePath, string filePath, bool needfillcontent = true)
        {
            string[] filelist = Directory.GetFiles(filePath);
            string relatPath, filename;
            foreach (var onefile in filelist)
            {
                if (validFileType(onefile))
                {
                 
                    InstallFileInfo fi = procesOneFile(this.dir,onefile);
                  
                    this.softfile.Add(fi);
                }
            }
            var dirlist = Directory.GetDirectories(filePath);
            foreach (var onedir in dirlist)
                this.iterInitDirs(this.dir, onedir, needfillcontent);
        }
        public void initFiles(string basePath,  bool needfillcontent = true, bool enableWatcher = true)
       {
           if (!Directory.Exists(basePath))
           {
               return;
           }
            this.dir = basePath;
            needFillContent = needfillcontent;
            this.enableWatcher = enableWatcher;
           this.softfile.Clear();
            iterInitDirs(this.dir, basePath, needfillcontent);
            isEnable = true;
            if (enableWatcher)
            {
                watchDir();
                watcher.EnableRaisingEvents = true;
               // checkFileChangeTimer.Start();
            }
       }
       public InstallFileInfo getFileInfoByHashKey(string _hashvalue)
       {
           foreach (var x in this.softfile)
           {
               
               if (x.hashcode == _hashvalue)
                   return x;
           }

           return null;
        }
       public InstallFileInfo getFileInfoByNameAndPath(string filename,string path)
       {
           foreach (var x in this.softfile)
           {
               if (x.filename == filename&&path==x.relationDir)
                   return x;
           }
           return null;
       }
       public void removeOneFile(InstallFileInfo ifi)
       {
           if (this.softfile.Contains(ifi))
               this.softfile.Remove(ifi);

       }
       //public InstallFileInfo addNewFile(string onefile,string basePath, bool needfillcontent = true)
       //{
       //    if (validFileType(onefile))
       //    {
       //        string filepath = System.IO.Path.GetDirectoryName(onefile).Replace(this.dir,"");
       //        string filename = System.IO.Path.GetFileName(onefile);
       //        InstallFileInfo fi = new InstallFileInfo(filename,this.dir, filepath, needfillcontent);
       //        this.softfile.Add(fi);
       //        return fi;
       //    }
       //    else
       //        return null;
       //}
       public static SoftUpdateFiles instance {get {return getinstnace();} }
       private SoftUpdateFiles()
       {
           softfile = new List<InstallFileInfo>();
           softConf = new configUpdate();
            isEnable = true;
            //checkFileChangeTimer = new System.Timers.Timer(1000*60);
            //checkFileChangeTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkInitFiles);
            //checkFileChangeTimer.AutoReset = true;
            //checkFileChangeTimer.Stop();
          
        }
       public List<InstallFileInfo> CompareDiffFile(Dictionary<string,InstallFileInfo> allfiles)
       {

           List<InstallFileInfo> resultlist=new List<InstallFileInfo>();
            if (!isEnable)
                return resultlist;
            foreach (var obj in softfile)
           {
               if (allfiles.ContainsKey(obj.hashcode))
               {
                   InstallFileInfo destfile = allfiles[obj.hashcode];
                   if (obj.NeedUpdate(destfile))
                       resultlist.Add(obj);
                   allfiles.Remove(obj.hashcode);
               }
               else
                   resultlist.Add(obj);
           }
           foreach (var obj in allfiles.Values)
           {
               obj.isdelete = true;
               resultlist.Add(obj);
           }
           return resultlist;
       }
    }
}
