using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Timers;

namespace FrmLib.Extend
{
    /// <summary>
    /// 只处理dll、exe文件、so文件
    /// </summary>
    public class InstallFileInfo
    {
        public string filename { get; set; }
        public string basepath{ get; set; }
        public string Dllversion{ get; set; }
        public DateTime lastwriteDt{ get; set; }
        public string hashcode{ get; set; }
        public byte[] filecontent{ get; set; }
        public bool isdelete { get; set; }
        public string oldHashCode{ get; set; }
        public InstallFileInfo(string _filename, string _basepath, string _hashcode):this()
        {
            this.filename = _filename;
            this.basepath = _basepath;
            this.hashcode = _hashcode;
           
        
        }
        public InstallFileInfo()
        {
            this.isdelete = false;
        }
        public InstallFileInfo(string _filename, string _basepath,bool fillcontent):this()
        {

            this.filename = _filename;
            this.basepath = _basepath;
            updateMe(fillcontent);
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
        public void updateMe(bool needfillcontent)
        {
           string fullfilename=System.IO.Path.Combine(basepath,filename);
            if(!File.Exists(fullfilename))
                return;
            this.lastwriteDt = File.GetLastWriteTime(fullfilename);
                
            
            {
               
                this.hashcode = tools_static.getMD5ByHashAlgorithm(fullfilename);//将字节数组装换为字符串 
                FileStream fstream = File.Open(fullfilename, FileMode.Open, FileAccess.Read, FileShare.Read);
               
                BinaryReader r = new BinaryReader(fstream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                
                byte[] fcontent = r.ReadBytes((int)r.BaseStream.Length);
                if (filename.EndsWith(".dll") || filename.EndsWith(".exe"))
                {
                    try
                    {
                        Assembly asm = Assembly.Load(fcontent);
                        this.Dllversion = asm.GetName().Version.ToString();
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("may be not assembly:{0}", exp.Message);
                    }
                }
                if (needfillcontent)
                {
                    filecontent = GZipCompress(fcontent);
                }
                fstream.Close();
            }
        }
        
          public bool NeedUpdate(InstallFileInfo destfile)
        {
            if (this.hashcode == destfile.hashcode)
                return false;
            else
            {
                if (this.filename.EndsWith(".dll") || this.filename.EndsWith(".exe") )
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
    /// 支持6种文件的自动更新
    /// .dll,.exe,.so,.sh,.py,.bat
    /// </summary>
   public class SoftUpdateFiles
    {
       public List<InstallFileInfo> softfile;
       public configUpdate softConf;
        private string dir;
        private bool needFillContent;
       private static SoftUpdateFiles _instance;
        private static object _lockObj = new object();
        private bool enableWatcher = false;
        FileSystemWatcher watcher;
        //是否有效
        private bool isEnable { get;  set; }
        private Timer checkFileChangeTimer { get;  set; }
        //上次文件变更时间
        private DateTime lastChange;

        private void watchDir()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = dir;
            watcher.NotifyFilter = NotifyFilters.Attributes |
                                   NotifyFilters.CreationTime |
                                   NotifyFilters.DirectoryName |
                                   NotifyFilters.FileName |
                                   NotifyFilters.LastAccess |
                                   NotifyFilters.LastWrite |
                                   NotifyFilters.Security |
                                   NotifyFilters.Size;
            watcher.Filter = "./";
            watcher.Created += new FileSystemEventHandler((o, e) =>
            {
                isEnable = false;
                lastChange = DateTime.Now;

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
        public void initFiles(string filePath, bool needfillcontent = true, bool enableWatcher = true)
       {
           if (!Directory.Exists(filePath))
           {
               return;
           }
            this.dir = filePath;
            needFillContent = needfillcontent;
            this.enableWatcher = enableWatcher;
           this.softfile.Clear();
           string[] filelist = Directory.GetFiles(filePath);
           string filepath, filename;
           foreach (var onefile in filelist)
           {
               if (validFileType(onefile))
               {
                   filepath = System.IO.Path.GetDirectoryName(onefile);
                   filename = System.IO.Path.GetFileName(onefile);
                   InstallFileInfo fi = new InstallFileInfo(filename, filepath, needfillcontent);
                   this.softfile.Add(fi);
               }
           }
            isEnable = true;
            if (enableWatcher)
            {
                watcher.EnableRaisingEvents = true;
                checkFileChangeTimer.Start();
            }
       }
       public InstallFileInfo getFileInfoByHashKey(string hashvalue)
       {
           foreach (var x in this.softfile)
           {
               if (x.hashcode == hashvalue)
                   return x;
           }
           return null;
        }
       public InstallFileInfo getFileInfoByNameAndPath(string filename,string path)
       {
           foreach (var x in this.softfile)
           {
               if (x.filename == filename&&path==x.basepath)
                   return x;
           }
           return null;
       }
       public void removeOneFile(InstallFileInfo ifi)
       {
           if (this.softfile.Contains(ifi))
               this.softfile.Remove(ifi);

       }
       public InstallFileInfo addNewFile(string onefile, bool needfillcontent = true)
       {
           if (validFileType(onefile))
           {
               string filepath = System.IO.Path.GetDirectoryName(onefile);
               string filename = System.IO.Path.GetFileName(onefile);
               InstallFileInfo fi = new InstallFileInfo(filename, filepath, needfillcontent);
               this.softfile.Add(fi);
               return fi;
           }
           else
               return null;
       }
       public static SoftUpdateFiles instance {get {return getinstnace();} }
       private SoftUpdateFiles()
       {
           softfile = new List<InstallFileInfo>();
           softConf = new configUpdate();
            isEnable = false;
            checkFileChangeTimer = new System.Timers.Timer(1000*60);
            checkFileChangeTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkInitFiles);
            checkFileChangeTimer.AutoReset = true;
            checkFileChangeTimer.Stop();
        }
       public List<InstallFileInfo> CompareDiffFile(Dictionary<string,InstallFileInfo> allfiles)
       {
            if (!isEnable)
                return null;
           List<InstallFileInfo> resultlist=new List<InstallFileInfo>();
           foreach (var obj in softfile)
           {
               if (allfiles.ContainsKey(obj.filename))
               {
                   InstallFileInfo destfile = allfiles[obj.filename];
                   if (obj.NeedUpdate(destfile))
                       resultlist.Add(obj);
                   allfiles.Remove(obj.filename);
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
