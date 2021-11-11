using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using FrmLib.Extend;
using deploySys.Model;
using System.IO;
using SharpCompress.Readers;
using SharpCompress.Common;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace deploySys.Node
{
     public partial class Crontask:ICronTask
    {
        public static bool isUnix()
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                       RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            return isUnix;
        }
        public static bool isWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
        private static string DockerApiUri()
        {


            if (isWindows())
            {
                return "npipe://./pipe/docker_engine";
            }



            if (isUnix())
            {
                return "unix:/var/run/docker.sock";
            }

            throw new Exception("Was unable to determine what OS this is running on, does not appear to be Windows or Linux!?");
        }

        static DockerClient dockerC = new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
         private static bool haveImages(string ImageName)
        {
            try
            {
                var images = FrmLib.Extend.AsyncHelpers.RunSync<IList<ImagesListResponse>>(() => dockerC.Images.ListImagesAsync(new ImagesListParameters() { MatchName = ImageName }));
                if (images.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private static ContainerInspectResponse getContainer(string instanceId)
        {
            try
            {
            //    ContainersListParameters cparams = new ContainersListParameters();
            //    cparams.All = true;
            //    cparams.Filters = new Dictionary<string, IDictionary<string, bool>>
            //{
            //    {
            //        "Id",
            //        new Dictionary<string, bool>
            //        {
            //            { instanceId, true}
            //        }
            //    }

            //};
                var x = FrmLib.Extend.AsyncHelpers.RunSync<ContainerInspectResponse>(() => dockerC.Containers.InspectContainerAsync(instanceId,default(CancellationToken)));
                
                return x;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static void  PullImage(string imagename,string tagname)
        {
           

            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Images
                .CreateImageAsync(new ImagesCreateParameters
                {
                    FromImage = imagename,
                    Tag = tagname
                },
                    new AuthConfig(),
                    new Progress<JSONMessage>()));
        }
        private static void startDockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }
            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStartParameters()));
        }
        private static void stopDockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }

            FrmLib.Extend.AsyncHelpers.RunSync(()=>dockerC.Containers.StopContainerAsync(instanceId,new Docker.DotNet.Models.ContainerStopParameters()));       
        }
        private static void nginxDockerReload(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }
             Console.WriteLine("nginxDockerReload");
            var created =  FrmLib.Extend.AsyncHelpers.RunSync<ContainerExecCreateResponse>(() =>  dockerC.Containers.ExecCreateContainerAsync(instanceId, new ContainerExecCreateParameters()
            {
                AttachStdin = true,
                AttachStdout = true,
                Cmd = new List<string>()
                {
                    "nginx",
                    "-s reload"
                },
                Tty = true,
                AttachStderr = true,

            }));

            FrmLib.Extend.AsyncHelpers.RunSync(() =>  dockerC.Containers.StartContainerExecAsync(created.ID));
             Console.WriteLine("nginxDockerReload ok");
        }
        private static void restart2DockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }
            System.Threading.Thread.Sleep(1000);
            FrmLib.Log.commLoger.devLoger.Debug("now stop docker instance :" + instanceId);
            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.RestartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerRestartParameters()));
        }
        private static void restartDockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }
            System.Threading.Thread.Sleep(1000);
            FrmLib.Log.commLoger.devLoger.Debug("now stop docker instance :" + instanceId);
            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StopContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStopParameters()));
            FrmLib.Log.commLoger.devLoger.Debug("now start docker instance :" + instanceId);
            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStartParameters()));
            
        }
        private static void removeDockerInstance(string instanceId,string path)
        {
            
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }
            
            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.RemoveContainerAsync(instanceId, new Docker.DotNet.Models.ContainerRemoveParameters()));
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))

            {
                Directory.Delete(path, true);
            }
        }
        /// <summary>
        /// 下载jar文件到发布目录
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="version"></param>
        /// <param name="msapps"></param>
        /// <param name="destPath"></param>
        private static void overrideTaskJarFilesInNode(ReleaseTask rt, appVersion version, MicroServiceApp msapps, string destPath)
        { 
         var content = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.GetTaskFileContentByGuid(rt.FileGuid));
            if (content == null || content.Length == 0)
                throw new Exception("overrideTaskJarFilesInNode get file content is empty");
             var fullfilename = Path.Combine(destPath, rt.FileName);        
            File.WriteAllBytes(fullfilename, content);
        }
        /// <summary>
        /// 下载war文件到发布目录
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="version"></param>
        /// <param name="msapps"></param>
        /// <param name="destPath"></param>
        private static void overrideTaskWarFilesInNode(ReleaseTask rt, appVersion version, MicroServiceApp msapps, string destPath)
        {

            var content = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.GetTaskFileContentByGuid( rt.FileGuid));
            if (content == null || content.Length == 0)
                throw new Exception("overrideTaskWarFilesInNode get file content is empty");
            var localTmpBaseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmpdata", rt.Id.ToString());
             if (!Directory.Exists(localTmpBaseDir))
                Directory.CreateDirectory(localTmpBaseDir);
           
            try
            {
                Console.WriteLine("unzip files");
                var  extractPath = Path.GetFullPath(localTmpBaseDir);
                // 确保提取路径上的最后一个字符
                //是目录分隔符char。
                //如果没有这个，恶意的zip文件可能会试图在预期的范围之外遍历
                //提取路径。
                if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                    extractPath += Path.DirectorySeparatorChar;
                using (ZipArchive archive = new ZipArchive(new MemoryStream(content)))
                {
                 //   FrmLib.Log.commLoger.runLoger.Debug("ZipArchive count:"+archive.Entries.Count);
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                      //  FrmLib.Log.commLoger.runLoger.Debug(entry.FullName);

                        if (entry.FullName.EndsWith(System.IO.Path.AltDirectorySeparatorChar))
                        {
                            if (!Directory.Exists(entry.FullName))
                                Directory.CreateDirectory(System.IO.Path.Combine(localTmpBaseDir, entry.FullName));
                            continue;
                        }
                        // 获取完整路径，以确保删除了相关段。
                        string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
                        //序号匹配是最安全的，区分大小写的卷可以装入
                        //是不区分大小写的。
                        if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                            entry.ExtractToFile(destinationPath);
                        
                    }
                }
                //解压文件
                //using (var stream = new MemoryStream(content))
                //{
                //    stream.Seek(0, SeekOrigin.Begin);
                //    using (var reader = ReaderFactory.Open(stream))
                //    {
                //        while (reader.MoveToNextEntry())
                //        {
                //            if (!reader.Entry.IsDirectory)
                //            {

                //                reader.WriteEntryToDirectory(localTmpBaseDir, new ExtractionOptions()
                //                {
                //                    ExtractFullPath = true,
                //                    Overwrite = true

                //                });
                //            }
                //        }
                //    }
                //}
                var fpath = localTmpBaseDir;
                IList<FileItem> needOverRideFiles = null;
                IList<FileItem> allfis = null;
              
                IList<string> conffile = null;
                if (!string.IsNullOrEmpty(msapps.confFileNames))
                    conffile = msapps.confFileNames.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                else
                    conffile = new List<string>();
                allfis = new List<FileItem>();
                Console.WriteLine(fpath);

                dsFunc.initPathToFileItem(version, fpath, fpath, false, conffile, allfis);
                FrmLib.Log.commLoger.runLoger.Debug( string.Format("initPathToFileItem count:{0}",allfis.Count));
                
                Console.WriteLine("overridePolicy:{0}", rt.overridePolicy);

                switch (rt.overridePolicy)
                {
                    case (0): //部分包全覆盖       
                    case (2): //全量包全覆盖
                        needOverRideFiles = allfis;
                        break;
                    case (1):  //全量包有修改更新
                        
                        if (!string.IsNullOrEmpty(msapps.confFileNames))
                            conffile = msapps.confFileNames.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                        else
                            conffile = new List<string>();
                        IList<FileItem> existfilelist = new List<FileItem>();
                        dsFunc.initPathToFileItem(version, destPath, destPath, false, conffile, existfilelist);
                        needOverRideFiles = version.CompareDiffFile(allfis, existfilelist);
                        break;
                }
               FrmLib.Log.commLoger.runLoger.Debug("copy file");
                foreach (var onefile in needOverRideFiles)
                {
                    if (onefile.isConfFile && !rt.confFileOverride)
                        continue;

                    var fullfilename = Path.Combine(destPath, onefile.retationPath, onefile.fileName);
                 //   FrmLib.Log.commLoger.runLoger.Debug(fullfilename);
                    var tmppath = Path.GetDirectoryName(fullfilename);
                    if (!Directory.Exists(tmppath))
                        Directory.CreateDirectory(tmppath);
                    if (onefile.remoteShouldDeleted && File.Exists(fullfilename))
                    {
                        System.IO.File.Delete(fullfilename);
                        continue;
                    }
                    string localFileName = Path.Combine(fpath, onefile.retationPath, onefile.fileName);
                    byte[] filecontent = File.ReadAllBytes(localFileName);
                    File.WriteAllBytes(fullfilename, filecontent);
                }
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error("overrideTaskFilesInNode:" + FrmLib.Extend.tools_static.getExceptionMessage(ex));
            }
            finally
            {
                Directory.Delete(localTmpBaseDir, true);
            }

        }
        /// <summary>
        /// 下载整包文件在本地解压更新
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="version"></param>
        /// <param name="msapps"></param>
        /// <param name="destPath"></param>
        private static void overrideTaskFilesInNode(ReleaseTask rt, appVersion version, MicroServiceApp msapps, string destPath)
        {
            
            var content = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.GetTaskFileContentByGuid( rt.FileGuid));
            if (content == null || content.Length == 0)
                throw new Exception("overrideTaskFilesInNode get file content is empty");
            var localTmpBaseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"tmpdata", rt.Id.ToString());
            if (!Directory.Exists(localTmpBaseDir))
                Directory.CreateDirectory(localTmpBaseDir);
            try
            {
                Console.WriteLine("unzip files");
                //解压文件
                using (var stream = new MemoryStream(content))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = ReaderFactory.Open(stream))
                    {
                        while (reader.MoveToNextEntry())
                        {
                            if (!reader.Entry.IsDirectory)
                            {
                               
                                reader.WriteEntryToDirectory(localTmpBaseDir, new ExtractionOptions()
                                {
                                    ExtractFullPath = true,
                                    Overwrite = true

                                });
                            }
                        }
                    }
                }
                var fpath = dsFunc.findMainFileDir(localTmpBaseDir, msapps.rootDirMainFile);
              
                IList<FileItem> needOverRideFiles = null;
                IList<FileItem> allfis = null;
                if (fpath == null)
                {
                    throw new Exception("找不到根目录");
                }
                else
                {
                    IList<string> conffile = null;
                    if (!string.IsNullOrEmpty(msapps.confFileNames))
                        conffile = msapps.confFileNames.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                    else
                        conffile = new List<string>();
                    allfis = new List<FileItem>();
                    Console.WriteLine(fpath);
                    
                    dsFunc.initPathToFileItem(version, fpath, fpath, false, conffile, allfis);
                    Console.WriteLine("initPathToFileItem ok");
                }
                Console.WriteLine("overridePolicy:{0}",rt.overridePolicy);

                switch (rt.overridePolicy)
                {
                    case (0): //部分包全覆盖       
                    case (2): //全量包全覆盖
                        needOverRideFiles = allfis;
                        break;
                    case (1):  //全量包有修改更新
                        List<string> conffile;
                        if (!string.IsNullOrEmpty(msapps.confFileNames))
                            conffile = msapps.confFileNames.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                        else
                            conffile = new List<string>();
                        IList<FileItem> existfilelist = new List<FileItem>();
                        dsFunc.initPathToFileItem(version, destPath, destPath, false, conffile, existfilelist);
                        needOverRideFiles = version.CompareDiffFile(allfis, existfilelist);
                        break;
                }
                Console.WriteLine("copy file");
                foreach (var onefile in needOverRideFiles)
                {
                    if (onefile.isConfFile && !rt.confFileOverride)
                        continue;

                    var fullfilename = Path.Combine(destPath, onefile.retationPath, onefile.fileName);
                    var tmppath = Path.GetDirectoryName(fullfilename);
                    if (!Directory.Exists(tmppath))
                        Directory.CreateDirectory(tmppath);
                    if (onefile.remoteShouldDeleted && File.Exists(fullfilename))
                    {
                        System.IO.File.Delete(fullfilename);
                        continue;
                    }
                    string localFileName = Path.Combine(fpath, onefile.retationPath, onefile.fileName);
                    byte[] filecontent = File.ReadAllBytes(localFileName);
                    File.WriteAllBytes(fullfilename, filecontent);
                }
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error("overrideTaskFilesInNode:"+FrmLib.Extend.tools_static.getExceptionMessage(ex));
            }
            finally { 
            Directory.Delete(localTmpBaseDir, true);
            }

        }
        private static void overrideTaskFilesInServer(ReleaseTask rt, appVersion version, MicroServiceApp msapps, string destPath)
        {
            IList<FileItem> needOverRideFiles = null;
            switch (rt.overridePolicy)
            {
                case (0): //部分包全覆盖       
                case (2): //全量包全覆盖
                    needOverRideFiles = FrmLib.Extend.AsyncHelpers.RunSync<List<FileItem>>(() => nodeClient.Instance.InvokeApi<List<FileItem>>("getReleaseTaskFiles", rt.Id));
                    break;
                case (1):  //全量包有修改更新
                    List<string> conffile;
                    if (!string.IsNullOrEmpty(msapps.confFileNames))
                        conffile = msapps.confFileNames.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                    else
                        conffile = new List<string>();
                    IList<FileItem> existfilelist = new List<FileItem>();
                    dsFunc.initPathToFileItem(version, destPath, destPath, false, conffile, existfilelist);
                    needOverRideFiles = FrmLib.Extend.AsyncHelpers.RunSync<List<FileItem>>(() => nodeClient.Instance.InvokeApi<List<FileItem>>("CompareRemoteFiles", rt.Id, existfilelist));
                    break;
            }
            foreach (var onefile in needOverRideFiles)
            {
                if (onefile.isConfFile && !rt.confFileOverride)
                    continue;

                var fullfilename = Path.Combine(destPath, onefile.retationPath, onefile.fileName);
                if (onefile.remoteShouldDeleted && File.Exists(fullfilename))
                {
                    System.IO.File.Delete(fullfilename);
                    continue;
                }
                byte[] filecontent = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.GetTaskFileContentByGuid( onefile.Guid));
                if (filecontent == null || filecontent.Length == 0)
                    throw new Exception("overrideTaskFilesInServer get file content is empty");
                File.WriteAllBytes(fullfilename, filecontent);
            }
        }
       /// <summary>
       /// 在服务端解压并更新
       /// </summary>
       /// <param name="rt"></param>
       /// <param name="version"></param>
       /// <param name="msapps"></param>
       /// <param name="destPath"></param>
            private static void overrideTaskFiles(ReleaseTask rt,appVersion version,MicroServiceApp msapps,string destPath)
        {
            Console.WriteLine("overrideTaskFiles:{0},{1}",rt.unzipInServer,rt.FileName);
            if (rt.unzipInServer &&!rt.FileName.ToLower().EndsWith(".jar"))
                overrideTaskFilesInServer(rt, version, msapps, destPath);
            else
            {

                if (rt.FileName.ToLower().EndsWith(".jar"))
                {
                    overrideTaskJarFilesInNode(rt, version, msapps, destPath);
                }
                else
                {
                    if (rt.FileName.ToLower().EndsWith(".war"))
                        overrideTaskWarFilesInNode(rt, version, msapps, destPath);
                    else
                         overrideTaskFilesInNode(rt, version, msapps, destPath);
                }
            }
            var versionstr = string.Format("{0}_{1}_{2}@{3}",msapps.key,version.version,rt.Id,DateTime.Now.ToString("yyyyMMddHHmmss"));
            var fn = Path.Combine(destPath, "release.ver");
            File.WriteAllText(fn, versionstr);
        }
        private static string prepareNginxDir()
        {
            var basenginxDir = RunConfig.Instance.serverHostResource.nginxConfPath;
            basenginxDir = string.IsNullOrEmpty(basenginxDir) ? "/opt/nginx" : basenginxDir;
            if (!Directory.Exists(basenginxDir))
            {
                Directory.CreateDirectory(basenginxDir);
            }
            var sitenginxDir = Path.Combine(basenginxDir, "site");
            if (!Directory.Exists(sitenginxDir))
            {
                Directory.CreateDirectory(sitenginxDir);
            }
            var lognginxDir = Path.Combine(basenginxDir, "log");
            if (!Directory.Exists(lognginxDir))
            {
                Directory.CreateDirectory(lognginxDir);
            }
            var confnginxDir = Path.Combine(basenginxDir, "conf.d");
            if (!Directory.Exists(confnginxDir))
            {
                Directory.CreateDirectory(confnginxDir);
            }
            var sslnginxDir = Path.Combine(basenginxDir, "ssl");
            if (!Directory.Exists(sslnginxDir))
            {
                Directory.CreateDirectory(sslnginxDir);
            }
            return basenginxDir;
        }
        private static void createOneWebSite(ReleaseTask rt, HostTask ht, appVersion version, MicroServiceApp msapps,out string siteDirName)
        {
             Console.WriteLine("createOneWebSite");
            var domainName = string.IsNullOrEmpty(rt.domainName) ? msapps.hostname : rt.domainName;
            Console.WriteLine("domainName:{0}", domainName);
            var sslPem= string.IsNullOrEmpty(rt.sslPem) ? msapps.sslPem : rt.sslPem;
            var sslKey= string.IsNullOrEmpty(rt.sslKey) ? msapps.sslKey : rt.sslKey;
            try
            {

                var basenginxDir=  prepareNginxDir();
                 var sitenginxDir = Path.Combine(basenginxDir, "site");
                 var sslnginxDir = Path.Combine(basenginxDir, "ssl");
                var confnginxDir = Path.Combine(basenginxDir, "conf.d");
                var appversionName = string.Format("{0}_{1}", msapps.key,rt.Version);
                var thissiteDir = Path.Combine(sitenginxDir, appversionName);
               
                if (!Directory.Exists(thissiteDir))
                {
                    Directory.CreateDirectory(thissiteDir);
                }
                string sslstr = RunConfig.Instance.serverHostResource.nginxHttpPort;
                string sslRemark = "#";
                Console.WriteLine("rt.usessl:{0};apps.usessl:{1}", rt.useSSL, msapps.useSsl);
                if (rt.useSSL == 1 || (rt.useSSL == 2 && msapps.useSsl.HasValue && msapps.useSsl.Value))
                {
                    Console.WriteLine("now configure ssl");
                    var fn1 = Path.Combine(sslnginxDir, appversionName + ".pem");
                    File.WriteAllText(fn1, sslPem);
                    fn1 = Path.Combine(sslnginxDir, appversionName + ".key");
                    File.WriteAllText(fn1, sslKey);
                    sslstr = string.Format("{0} ssl",RunConfig.Instance.serverHostResource.nginxHttpsPort);
                    sslRemark = "";
                }
                string loadstr = null;
                if (msapps.serviceType == (int)EnumAppServiceType.webSite)
                {
                    var content = nodeClient.Instance.getNginxConfFileContent("webSite.conf");
                    if (content == null || content.Length == 0)
                        throw new Exception("site  configure file is null or empty");
                    loadstr = System.Text.Encoding.UTF8.GetString(content);
                    loadstr = loadstr.Replace("{appname}", appversionName).Replace("{msapps.hostname}", domainName).Replace("{sslstr}", sslstr).Replace("{sslRemark}", sslRemark);
                }
                else
                {
                    if (msapps.serviceType == (int)EnumAppServiceType.webServiceSite)
                    {
                        var content = nodeClient.Instance.getNginxConfFileContent("websiteWithServer.conf");
                        if (content == null || content.Length == 0)
                            throw new Exception("site  configure file is null or empty");
                        string ip = rt.useWIP.HasValue && rt.useWIP.Value ? RunConfig.Instance.serverHostResource.WIP : "127.0.0.1"; //RunConfig.Instance.serverHostResource.IP;
                        loadstr = System.Text.Encoding.UTF8.GetString(content);
                        loadstr = loadstr.Replace("{appname}", appversionName).Replace("{ip}", ip).Replace("{ht.allocPort}", ht.allocPort.ToString()).Replace("{msapps.hostname}", domainName).Replace("{sslstr}", sslstr).Replace("{sslRemark}", sslRemark);
                    }
                }
                if (string.IsNullOrEmpty(loadstr))
                    throw new Exception("site  configure file is null or empty");
                siteDirName =appversionName;
                var fn = Path.Combine(confnginxDir, siteDirName + ".conf");
               
                File.WriteAllText(fn, loadstr);
                // nginxDockerReload(RunConfig.Instance.serverHostResource.nginxInstanceId);
                Console.WriteLine("now restart nginx");

                restartDockerInstance(RunConfig.Instance.serverHostResource.nginxInstanceId);
                Console.WriteLine("createOneWebSite ok");
            }
            catch (Exception ex)
            {
                FrmLib.Log.commLoger.runLoger.Error(FrmLib.Extend.tools_static.getExceptionMessage(ex));
                throw ex;
            }
        }
        private static void createNginxInstance(ReleaseTask rt, HostTask ht, appVersion version, MicroServiceApp msapps)
        {
            Console.WriteLine("createNginxInstance");
            var basenginxDir = RunConfig.Instance.serverHostResource.nginxConfPath;
            basenginxDir = string.IsNullOrEmpty(basenginxDir) ? "/opt/nginx" : basenginxDir;
            if (!Directory.Exists(basenginxDir))
            {
                Directory.CreateDirectory(basenginxDir);
            }

            var soucrMineTypeFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs", "mime.types");
            var destMtFile= Path.Combine(basenginxDir, "mime.types");
            File.Copy(soucrMineTypeFile, destMtFile,true);
            var content = nodeClient.Instance.getNginxConfFileContent("webServerMainconf.conf");         
            var fn = Path.Combine(basenginxDir, "nginx.conf");
            File.WriteAllBytes(fn, content);
             content = nodeClient.Instance.getNginxConfFileContent("diWebServer.json");
               //处理nginx端口
            
            var loadstr = System.Text.Encoding.UTF8.GetString(content);
            loadstr = loadstr.Replace("{HT.WebPort}", RunConfig.Instance.serverHostResource.nginxHttpPort);
            FrmLib.Log.commLoger.devLoger.Debug(JsonConvert.SerializeObject( RunConfig.Instance.serverHostResource));
            loadstr = loadstr.Replace("{basenginxDir}", basenginxDir);
               FrmLib.Log.commLoger.devLoger.Debug("parms:"+loadstr);
            CreateContainerParameters dparam = JsonConvert.DeserializeObject<CreateContainerParameters>(loadstr);

            if (string.IsNullOrEmpty(dparam.Image))
                throw new Exception("容器参数中映像名称为空");
            if (!haveImages(dparam.Image))
            {
                string[] cparams = new string[2];
                var tmplist = dparam.Image.Split(':');
                cparams[0] = tmplist[0];
                if (tmplist.Count() > 1)
                {
                    cparams[1] = tmplist[1];
                }
                else
                    cparams[1] = "latest";
                PullImage(cparams[0], cparams[1]);
            }
            var response =FrmLib.Extend.AsyncHelpers.RunSync<CreateContainerResponse>(() =>  dockerC.Containers.CreateContainerAsync(dparam));
            var iid = response.ID;
             AsyncHelpers.RunSync(() => dockerC.Containers.RenameContainerAsync(iid,new ContainerRenameParameters() {NewName="nginx" },new CancellationToken()));
            DockerInstance di = new DockerInstance();
            di.instanceId = response.ID;
            di.proxyPort = 80;
            di.baseDir = "";
            Console.WriteLine(response.ID);
            RunConfig.Instance.serverHostResource.haveNginx = true;            
            RunConfig.Instance.serverHostResource.nginxInstanceId = response.ID;
           
            var res = nodeClient.Instance.addDockInstance(di, RunConfig.Instance.serverHostResource.Id, rt.Id, msapps.Id, ht.Id, true, basenginxDir);
            Console.WriteLine(" instance ID:{0}",res);
            if ( res> 0)
            {
                startDockerInstance(di.instanceId);
            }
            else
            {

                removeDockerInstance(di.instanceId,Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir,di.baseDir));
                throw new Exception("添加docker实例失败,errorCode:"+res.ToString());
            }
             Console.WriteLine("createNginxInstance ok");
        }
        private static void createServiceInstance(ReleaseTask rt,HostTask ht,appVersion version,MicroServiceApp msapps,string appPath)
        {
             Console.WriteLine("createServiceInstance");
            //字符串中{0}，主机对外端口，{1}:app服务端口，{2}:工作目录（应用根目录),{3}:启动命令的主文件,{4}:docker实例名称
            var instancename = string.Format("{0}-{1}-{2}",msapps.key,version.version,ht.Id);
            string paramstr = null;
            CreateContainerParameters dparam = null;
             string iid = null;
            var dibasedir=appPath.Replace(RunConfig.Instance.serverHostResource.appBaseDir, "").TrimStart(System.IO.Path.DirectorySeparatorChar);
            try
            {
                string origParamstr = null;
                if (rt.dockerNetType == 0)
                    origParamstr = msapps.createContainerParams;
                if (rt.dockerNetType == 1)
                    origParamstr = msapps.createContainerParams2;
                if (string.IsNullOrEmpty(origParamstr))
                    throw new Exception("容器创建参数为空");

                paramstr = origParamstr.Replace(@"{ht.allocPort}", ht.allocPort.ToString()).Replace(@"{msapps.port}", msapps.port).Replace(@"{appPath}", appPath).Replace(@"{msapps.rootDirMainFile}", msapps.rootDirMainFile).Replace(@"{instancename}", instancename);
                Console.WriteLine(paramstr);
                dparam = JsonConvert.DeserializeObject<CreateContainerParameters>(paramstr);
                if (!haveImages(dparam.Image))
                {
                    string[] cparams = new string[2];
                    var tmplist = dparam.Image.Split(':');
                    cparams[0] = tmplist[0];
                    if (tmplist.Count() > 1)
                    {
                        cparams[1] = tmplist[1];
                    }
                    else
                        cparams[1] = "latest";
                    PullImage(cparams[0], cparams[1]);
                }
                var response = AsyncHelpers.RunSync<Docker.DotNet.Models.CreateContainerResponse>(() => dockerC.Containers.CreateContainerAsync(dparam));
                iid = response.ID;
                Console.WriteLine("{0}", response.ID);
                AsyncHelpers.RunSync(() => dockerC.Containers.RenameContainerAsync(iid,new ContainerRenameParameters() {NewName=instancename },new CancellationToken()));
                DockerInstance di = new DockerInstance();
                di.instanceId = iid;
                di.proxyPort = ht.allocPort;
                di.baseDir = dibasedir;
             
                var res = nodeClient.Instance.addDockInstance(di, RunConfig.Instance.serverHostResource.Id, rt.Id, msapps.Id, ht.Id, false);
                Console.WriteLine("{0}", res);

                if ( res>0)
                {
                    //如果服务端数据保存成功，则启动实例，否则删除
                    startDockerInstance(di.instanceId);
                    ht.dockerInanceId = di.instanceId;
                }
                else
                {
                    removeDockerInstance(iid,Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir,di.baseDir));
                    throw new Exception("添加docker实例失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("createServiceInstance "+ FrmLib.Extend.tools_static.getExceptionMessage(ex));
                if(!string.IsNullOrEmpty(iid))
                   removeDockerInstance(iid,Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir,dibasedir)); 
                throw ex;
            }
             Console.WriteLine("createServiceInstance end");
        }
        private static void createInstanceContainer(ReleaseTask rt,HostTask ht,appVersion version,MicroServiceApp msapps,string appPath,out string siteDirName)
        {
            bool needCreatenginx = true;
            siteDirName = "";
            switch (msapps.serviceType)
            {
                case ((int)EnumAppServiceType.webSite):
                    
                    if (RunConfig.Instance.serverHostResource.haveNginx.HasValue && RunConfig.Instance.serverHostResource.haveNginx.Value)
                    {
                        var instanceinfo = getContainer(RunConfig.Instance.serverHostResource.nginxInstanceId);
                        if (instanceinfo != null)
                            needCreatenginx = false;
                      

                    }
                    if(needCreatenginx)
                        //创建nginx实例
                        createNginxInstance(rt, ht, version, msapps);
                    createOneWebSite(rt, ht, version, msapps,out siteDirName);
                    break;
                case ((int)EnumAppServiceType.socketService):

                    
                case ((int)EnumAppServiceType.webService):

                    createServiceInstance(rt,ht,version,msapps,appPath);
                    break;
                case ((int)EnumAppServiceType.webServiceSite):
                     createServiceInstance(rt,ht,version,msapps,appPath);
                    if (RunConfig.Instance.serverHostResource.haveNginx.HasValue && RunConfig.Instance.serverHostResource.haveNginx.Value)
                    {
                        var instanceinfo = getContainer(RunConfig.Instance.serverHostResource.nginxInstanceId);
                        if (instanceinfo != null)
                            needCreatenginx = false;
                    }
                    if (needCreatenginx)
                        //创建nginx实例
                        createNginxInstance(rt, ht, version, msapps);
                    createOneWebSite(rt, ht, version, msapps,out siteDirName);
                    break;
            }

           

        }
    }

}
