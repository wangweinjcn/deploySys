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

namespace deploySys.Node
{


    public class Crontask:ICronTask
    {
        private static string DockerApiUri()
        {
            

            if (FrmLib.Extend.tools_static.isWindows())
            {
                return "npipe://./pipe/docker_engine";
            }

           

            if (FrmLib.Extend.tools_static.isUnix())
            {
                return "unix:/var/run/docker.sock";
            }

            throw new Exception("Was unable to determine what OS this is running on, does not appear to be Windows or Linux!?");
        }

        static   DockerClient dockerC =new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
       //static private HttpHelper hclient = new HttpHelper();

        public static void checkSelfUpdate()
        {
            try
            {
                var changelist = nodeClient.Instance.CompareClientFiles();

                if (changelist == null)
                    return;
                if (changelist.Count > 0)
                {
                    string timestr = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    string bakpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, timestr);
                    if (!Directory.Exists(bakpath))
                        Directory.CreateDirectory(bakpath);

                    foreach (var obj in changelist)
                    {


                        if (obj.isdelete)
                        {
                            File.Delete(Path.Combine(obj.basepath, obj.filename));
                            RunConfig.Instance.runlog.Info(string.Format("now delete file {0}", obj.filename));

                        }
                        else
                        {
                            byte[] gzipcontent = nodeClient.Instance.GetClientUpdateFile(obj.hashcode);
                            byte[] content = obj.GZipDecompress(gzipcontent);
                            RunConfig.Instance.runlog.Debug(string.Format("now write bak dir, {0}", obj.filename));
                            File.WriteAllBytes(Path.Combine(bakpath, obj.filename), content);
                        }
                    }
                    RunConfig.Instance.runlog.Debug("now move to basedir ");
                    string cmd = "mv " + Path.Combine(bakpath, "*.*") + "  " + AppDomain.CurrentDomain.BaseDirectory;
                    RunConfig.Instance.runlog.Debug("cmd string: " + cmd);
                    string res = tools.excutScript(cmd);
                    //cmd = "cp " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "*.o") + "  /lib";
                    //RunConfig.Instance.runlog.Debug("now cp *.o  string: " + cmd);
                    //res = tools.excutScript(cmd);
                    //RunConfig.Instance.runlog.Debug("mv ended:" + res);
                    //cmd = "cp " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "*.so") + "  /lib";
                    //RunConfig.Instance.runlog.Debug("now cp *.so  string: " + cmd);
                    //res = tools.excutScript(cmd);
                    //cmd = "chmod +x " + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "*.sh");
                    //RunConfig.Instance.runlog.Debug("cmd string: " + cmd);
                    //res = tools.excutScript(cmd);
                    //RunConfig.Instance.runlog.Debug("chmod  ended:" + res);
                    //RunConfig.Instance.runlog.Debug("now delete dir: ");
                    //Directory.Delete(bakpath);
                    //cmd = "ldconfig";
                    //RunConfig.Instance.runlog.Debug("now do ldconfig : " + cmd);
                    //res = tools.excutScript(cmd);
                    RunConfig.Instance.runlog.Debug("now set restart: ");
                    tools.restartSelf();

                }

            }
            catch (Exception ex)
            { }



        }
        public static bool haveImages(string ImageName)
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
        public static ContainerInspectResponse getContainer(string instanceId)
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
        public static void  PullImage(string imagename,string tagname)
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
        public static void startDockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }
            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStartParameters()));
        }
        public static void stopDockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }

            FrmLib.Extend.AsyncHelpers.RunSync(()=>dockerC.Containers.StopContainerAsync(instanceId,new Docker.DotNet.Models.ContainerStopParameters()));       
        }
        public static void nginxDockerReload(string instanceId)
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
        public static void restartDockerInstance(string instanceId)
        {
            if (getContainer(instanceId) == null)
            {
                Console.WriteLine(" instance is not exist");
                return;
            }

            FrmLib.Extend.AsyncHelpers.RunSync(() => dockerC.Containers.StartContainerAsync(instanceId, new Docker.DotNet.Models.ContainerStartParameters()));
        }
        public static void removeDockerInstance(string instanceId,string path)
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
        /// 下载整包文件在本地解压更新
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="version"></param>
        /// <param name="msapps"></param>
        /// <param name="destPath"></param>
        private static void overrideTaskFilesInNode(ReleaseTask rt, appVersion version, MicroServiceApp msapps, string destPath)
        {
            
            var content = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.InvokeApi<byte[]>("GetFileContentByGuid", rt.FileGuid));
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
                byte[] filecontent = FrmLib.Extend.AsyncHelpers.RunSync<byte[]>(() => nodeClient.Instance.InvokeApi<byte[]>("GetFileContentByGuid", onefile.Guid));
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
            Console.WriteLine("unzipInServer:{0}",rt.unzipInServer);
            if (rt.unzipInServer)
                overrideTaskFilesInServer(rt, version, msapps, destPath);
            else
            {

                overrideTaskFilesInNode(rt, version, msapps, destPath);
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
                string sslstr = "80";
                string sslRemark = "#";
                Console.WriteLine("rt.usessl:{0};apps.usessl:{1}", rt.useSSL, msapps.useSsl);
                if (rt.useSSL == 1 || (rt.useSSL == 2 && msapps.useSsl.HasValue && msapps.useSsl.Value))
                {
                    Console.WriteLine("now configure ssl");
                    var fn1 = Path.Combine(sslnginxDir, appversionName + ".pem");
                    File.WriteAllText(fn1, sslPem);
                    fn1 = Path.Combine(sslnginxDir, appversionName + ".key");
                    File.WriteAllText(fn1, sslKey);
                    sslstr = "443 ssl";
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
            var loadstr = System.Text.Encoding.UTF8.GetString(content);
            loadstr = loadstr.Replace("{basenginxDir}", basenginxDir);
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
        public static void processTask()
        {
           
           // var xxx = client.Containers.CreateContainerAsync();
            var alltask = nodeClient.Instance.getNodeTask();
            if (alltask == null)
                return;
            string siteDirName = "";//nginx站点目录名
            foreach (JObject onetask in alltask)
            {
                string url;
                HostTask ht =JsonConvert.DeserializeObject<HostTask>( onetask["HostTask"].ToString());
                DockerInstance di=null;
                if(FrmLib.Extend.tools_static.jobjectHaveKey(onetask,"DockerInstance")&&onetask["DockerInstance"]!=null  && !string.IsNullOrEmpty(onetask["DockerInstance"].ToString()))
                 di= JsonConvert.DeserializeObject<DockerInstance>(onetask["DockerInstance"].ToString());
                 string appbasepath = null;
                try
                {  nodeClient.Instance.reportTaskStart(ht.Id);
                    switch (ht.taskType)
                    {
                        case ((int)EnumHostTaskType.releaseTask):

                            ReleaseTask rt = JsonConvert.DeserializeObject<ReleaseTask>(onetask["ReleaseTask"].ToString());
                            if (rt == null)
                            {
                                throw new Exception("rt is null");
                            }
                            MicroServiceApp msapp = JsonConvert.DeserializeObject<MicroServiceApp>(onetask["versionApp"].ToString());
                            if (msapp == null)
                                throw new Exception("versionApp is null");
                            appVersion version = JsonConvert.DeserializeObject<appVersion>(onetask["appVersion"].ToString());

                            if (version == null)
                                throw new Exception("version is null");
                            Console.WriteLine("releaseType:{0}",rt.releaseType);
                            if (rt.releaseType == 0) //新增
                            {
                               
                                Console.WriteLine("serviceType:{0}",msapp.serviceType);
                                switch (msapp.serviceType)
                                {
                                    case ((int)EnumAppServiceType.socketService):
                                    case ((int)EnumAppServiceType.webServiceSite):
                                   case ((int)EnumAppServiceType.webService):
                                       
                                        var newInstancePath = Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir, msapp.key, version.version + "_" + ht.Id.ToString());
                                         Console.WriteLine("{0}",newInstancePath);
                                        if (!Directory.Exists(newInstancePath))
                                             Directory.CreateDirectory(newInstancePath);
                                        Console.WriteLine("have created dir");
                                        rt.overridePolicy = 2;
                                        rt.confFileOverride = true;
                                        Console.WriteLine("now entry override file");
                                        overrideTaskFiles(rt, version, msapp, newInstancePath);
                                        appbasepath = newInstancePath;
                                        break;
                                    case ((int)EnumAppServiceType.webSite):
                                        var basenginxDir = RunConfig.Instance.serverHostResource.nginxConfPath;
                                        basenginxDir = string.IsNullOrEmpty(basenginxDir) ? "/opt/nginx" : basenginxDir;
                                        var sitenginxDir = Path.Combine(basenginxDir, "site");
                                        if (!Directory.Exists(sitenginxDir))
                                        {
                                            Directory.CreateDirectory(sitenginxDir);
                                        }
                                        var siteDirN = string.Format("{0}_{1}", msapp.key,version.version);
                                        var thissiteDir = Path.Combine(sitenginxDir, siteDirN);
                                        if (!Directory.Exists(thissiteDir))
                                             Directory.CreateDirectory(thissiteDir);
                                        overrideTaskFiles(rt, version, msapp, thissiteDir);
                                        appbasepath = thissiteDir;
                                        break;

                                }
                                Console.WriteLine("now createInstanceContainer ");
                                createInstanceContainer(rt, ht, version, msapp, appbasepath,out siteDirName);

                            }  
                            else
                            {
                                

                                if (rt.releaseType == 1) //更新
                                {
                                    Console.WriteLine("msapp.serviceType:{0}",msapp.serviceType);
                                    if (msapp.serviceType != (int)EnumAppServiceType.webSite)
                                    {

                                        var listinstance = FrmLib.Extend.AsyncHelpers.RunSync<List<DockerInstance>>(() => nodeClient.Instance.InvokeApi<List<DockerInstance>>("getAppDockerInstnce", ht.Id));
                                        Console.WriteLine("count:{0}",listinstance.Count);
                                        var appdirList = (from x in listinstance select x.baseDir).Distinct().ToList();
                                        foreach (var onedir in appdirList)
                                        {
                                            var appPath = Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir, onedir);
                                            if (!Directory.Exists(appPath))
                                            {
                                                throw new Exception("docker working dir is not exist," + appPath);
                                            }

                                            Console.WriteLine("override file @"+appPath);
                                            overrideTaskFiles(rt, version, msapp, appPath);

                                        }
                                        foreach (var instance in listinstance)
                                        {
                                            Console.WriteLine("now restart {0}" , instance.instanceId);
                                            restartDockerInstance(instance.instanceId);
                                        }
                                    }
                                    else
                                    {
                                         var listinstance = FrmLib.Extend.AsyncHelpers.RunSync<List<webSite>>(() => nodeClient.Instance.InvokeApi<List<webSite>>("getAppWebSite", ht.Id));

                                        var basenginxDir = RunConfig.Instance.serverHostResource.nginxConfPath;
                                         basenginxDir = string.IsNullOrEmpty(basenginxDir) ? "/opt/nginx" : basenginxDir;
                                        var sitenginxDir = Path.Combine(basenginxDir, "site");
                                        if (!Directory.Exists(sitenginxDir))
                                        {
                                            throw new Exception("site root dir is not exist");
                                        }
                                        foreach (var onesite in listinstance)
                                        {
                                           
                                            var thissiteDir = Path.Combine(sitenginxDir, onesite.siteDirName);
                                            if (!Directory.Exists(thissiteDir))
                                                throw new Exception("site dir is not exist");

                                            Console.WriteLine("override file @{0}", thissiteDir);
                                            overrideTaskFiles(rt, version, msapp, thissiteDir);
                                        }
                                        Console.WriteLine("now restart nginx instance");
                                        restartDockerInstance(RunConfig.Instance.serverHostResource.nginxInstanceId);
                                    }
                                }

                            }

                            break;
                        case ((int)EnumHostTaskType.stopDockerInstance):
                                                
                            if (di == null || string.IsNullOrEmpty(di.instanceId))
                                throw new Exception("docker instance is null");

                            stopDockerInstance(di.instanceId);
                            break;
                        case ((int)EnumHostTaskType.restartDockerInstance):
                           
                            if (di == null || string.IsNullOrEmpty(di.instanceId))
                                throw new Exception("docker instance is null");
                          
                          restartDockerInstance(di.instanceId);
                          
                            break;
                            case ((int)EnumHostTaskType.removeNgixSite): //移除nginx站点 ???
                            var siteName = JObject.Parse( ht.taskParms)["siteKey"].ToString();
                            var siteDir = Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "site", siteName);
                            if (Directory.Exists(siteDir))
                                Directory.Delete(siteDir, true);
                            var siteConfFile=Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "conf.d",string.Format("{0}.conf", siteName));
                            if (File.Exists(siteConfFile))
                                File.Delete(siteConfFile);
                            var sslKeyFile=Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "ssl",string.Format("{0}.key", siteName));
                            if (File.Exists(sslKeyFile))
                                File.Delete(sslKeyFile);
                            var sslPemFile = Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "ssl", string.Format("{0}.pem", siteName));
                            if (File.Exists(sslPemFile))
                                File.Delete(sslPemFile);
                            nginxDockerReload(RunConfig.Instance.serverHostResource.nginxInstanceId);
                            break;

                            case ((int)EnumHostTaskType.removeDockerInstance):
                            if (di == null || string.IsNullOrEmpty(di.instanceId))
                                throw new Exception("docker instance is null");
                            if (di.isNginx.Value)
                            {
                                RunConfig.Instance.serverHostResource.nginxInstanceId = null;
                            }
                            stopDockerInstance(di.instanceId);
                            removeDockerInstance(di.instanceId,Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir,di.baseDir));

                            break;
                    }
                      nodeClient.Instance.reportTaskFinish(ht.Id,siteDirName);
                }
                catch (Exception exp)
                {
                    Console.WriteLine(FrmLib.Extend.tools_static.getExceptionMessage(exp));
                    Console.WriteLine("now report TaskFail");
                    if (!string.IsNullOrEmpty(ht.dockerInanceId))
                    {
                        stopDockerInstance(ht.dockerInanceId);
                        removeDockerInstance(ht.dockerInanceId,appbasepath);
                    }
                    nodeClient.Instance.reportTaskFail(ht.Id, exp.Message);
                }
            }
        }
        public static void reportPerf()
        {
            try
            { 
            nodeClient.Instance.ReportPerf();
            }
            catch (Exception ex)
            { }
        }
        public static void sayAlive()
        {
            try
            {
                nodeClient.Instance.SayAlive();
            }
            catch (Exception ex)
            { }

        }
    }
}
