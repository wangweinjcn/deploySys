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
using System.Runtime.CompilerServices;
using NettyRPC;

namespace deploySys.Node
{


    public partial class Crontask:ICronTask
    {
        private static int runingTaskCount =0;
        private static object lockobj = new object();
        private static bool needRestart = false;
        public static void checkSelfUpdate()
        {

            if (needRestart)
                return;
            try
            {
               
                var changelist = nodeClient.Instance.CompareClientFiles();

                if (changelist == null)
                    return;
                if (changelist.Count > 0)
                {
                    string timestr = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    string bakpath = Path.Combine(commSetting.Configuration["tmpDataDir"], timestr);
                    if (!Directory.Exists(bakpath))
                        Directory.CreateDirectory(bakpath);
                    try
                    {
                        foreach (var obj in changelist)
                        {
                            if (obj == null)
                            {
                                FrmLib.Log.commLoger.runLoger.Error("changelist one obj is null");
                                continue;
                            }
                            var tmpstr = JObject.FromObject(obj).ToString();

                            FrmLib.Log.commLoger.devLoger.Debug("obj :" + tmpstr);

                            if (obj.isdelete)
                            {
                                var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, obj.relationDir, obj.filename);
                                File.Delete(fullpath);
                                
                                FrmLib.Log.commLoger.runLoger.Info(string.Format("now delete file {0}", fullpath));

                            }
                            else
                            {
                                  
                                byte[] gzipcontent = nodeClient.Instance.GetClientUpdateFile(obj.hashcode);

                                if (gzipcontent == null  )
                                {
                                    FrmLib.Log.commLoger.runLoger.Error("gzipcontentj is null ," +obj.filename);
                                    continue;
                                }
                                if (gzipcontent.Length == 0)
                                { 
                                    FrmLib.Log.commLoger.runLoger.Error("gzipcontentj is null; filename is "+obj.filename);
                                    continue;
                                }
                                byte[] content = obj.GZipDecompress(gzipcontent);
                                // FrmLib.Log.commLoger.runLoger.Debug(string.Format("now write bak dir, {0}", obj.filename));
                                if (!Directory.Exists(Path.Combine(bakpath, obj.relationDir)))
                                    Directory.CreateDirectory(Path.Combine(bakpath, obj.relationDir));
                                var fullfilename = Path.Combine(bakpath, obj.relationDir, obj.filename);
                                 FrmLib.Log.commLoger.runLoger.Info(string.Format("now update obj {0}", fullfilename));
                                File.WriteAllBytes(fullfilename, content);
                            }
                        }
                        FrmLib.Log.commLoger.runLoger.Debug("now move to basedir ");
                        string cmd = "mv " + Path.Combine(bakpath, "*") + "  " + AppDomain.CurrentDomain.BaseDirectory;
                        FrmLib.Log.commLoger.runLoger.Debug("cmd string: " + cmd);
                        string res = tools.excutScriptOnLinux(cmd);
                         cmd = "mv " + Path.Combine(bakpath,"configs") + "  " + AppDomain.CurrentDomain.BaseDirectory;
                        FrmLib.Log.commLoger.runLoger.Debug("cmd string: " + cmd);
                         res = tools.excutScriptOnLinux(cmd);
                    }
                    finally
                    {

                        string cmd = "rm -rf " + bakpath;
                        string res = tools.excutScriptOnLinux(cmd);
                    }
                  
                  
                    var iid = nodeService.getInstanceId();
                      FrmLib.Log.commLoger.runLoger.Debug("now get dockering instance Id: "+iid);
                    if (iid != null)
                    {
                      needRestart = true;
                        
                        while (runingTaskCount != 0)
                        {
                             FrmLib.Log.commLoger.runLoger.Debug("checkruning task count ="+runingTaskCount.ToString());
                            System.Threading.Thread.Sleep(5000); 
                        }
                      FrmLib.Log.commLoger.runLoger.Debug("now set restart: ");
                      restart2DockerInstance(iid);
                    }

                }

            }
            catch (Exception exp)
            { 
             FrmLib.Log.commLoger.runLoger.Error(string.Format("checkSelfUpdate error{0}",FrmLib.Extend.tools_static.getExceptionMessage(exp)));
            }



        }
       
        public static void processTask()
        {
            if (needRestart)
                return;
            try
            {

                lock (lockobj)
                    runingTaskCount++;
                var alltask = nodeClient.Instance.getNodeTask();
                if (alltask == null)
                    return;
                string siteDirName = "";//nginx站点目录名
                foreach (JObject onetask in alltask)
                {
                    string url;
                    HostTask ht = JsonConvert.DeserializeObject<HostTask>(onetask["HostTask"].ToString());
                    DockerInstance di = null;
                    if (FrmLib.Extend.tools_static.jobjectHaveKey(onetask, "DockerInstance") && onetask["DockerInstance"] != null && !string.IsNullOrEmpty(onetask["DockerInstance"].ToString()))
                        di = JsonConvert.DeserializeObject<DockerInstance>(onetask["DockerInstance"].ToString());
                    string appbasepath = null;
                    try
                    {
                        nodeClient.Instance.reportTaskStart(ht.Id);
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
                                Console.WriteLine("releaseType:{0}", rt.releaseType);
                                if (rt.releaseType == 0) //新增
                                {

                                    Console.WriteLine("serviceType:{0}", msapp.serviceType);
                                    switch (msapp.serviceType)
                                    {
                                        case ((int)EnumAppServiceType.socketService):
                                        case ((int)EnumAppServiceType.webServiceSite):
                                        case ((int)EnumAppServiceType.webService):

                                            var newInstancePath = Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir, msapp.key, version.version + "_" + ht.Id.ToString());
                                            Console.WriteLine("{0}", newInstancePath);
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
                                            var siteDirN = string.Format("{0}_{1}", msapp.key, version.version);
                                            var thissiteDir = Path.Combine(sitenginxDir, siteDirN);
                                            if (!Directory.Exists(thissiteDir))
                                                Directory.CreateDirectory(thissiteDir);
                                            overrideTaskFiles(rt, version, msapp, thissiteDir);
                                            appbasepath = thissiteDir;
                                            break;

                                    }
                                    Console.WriteLine("now createInstanceContainer ");
                                    createInstanceContainer(rt, ht, version, msapp, appbasepath, out siteDirName);

                                }
                                else
                                {


                                    if (rt.releaseType == 1) //更新
                                    {
                                        Console.WriteLine("msapp.serviceType:{0}", msapp.serviceType);
                                        if (msapp.serviceType != (int)EnumAppServiceType.webSite)
                                        {

                                            var listinstance = FrmLib.Extend.AsyncHelpers.RunSync<List<DockerInstance>>(() => nodeClient.Instance.InvokeApi<List<DockerInstance>>("getAppDockerInstnce", ht.Id));
                                            Console.WriteLine("count:{0}", listinstance.Count);
                                            var appdirList = (from x in listinstance select x.baseDir).Distinct().ToList();
                                            foreach (var onedir in appdirList)
                                            {
                                                var appPath = Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir, onedir);
                                                if (!Directory.Exists(appPath))
                                                {
                                                    throw new Exception("docker working dir is not exist," + appPath);
                                                }

                                                Console.WriteLine("override file @" + appPath);
                                                overrideTaskFiles(rt, version, msapp, appPath);

                                            }
                                            foreach (var instance in listinstance)
                                            {
                                                Console.WriteLine("now restart {0}", instance.instanceId);
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
                                var siteName = JObject.Parse(ht.taskParms)["siteKey"].ToString();
                                var siteDir = Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "site", siteName);
                                if (Directory.Exists(siteDir))
                                    Directory.Delete(siteDir, true);
                                var siteConfFile = Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "conf.d", string.Format("{0}.conf", siteName));
                                if (File.Exists(siteConfFile))
                                    File.Delete(siteConfFile);
                                var sslKeyFile = Path.Combine(RunConfig.Instance.serverHostResource.nginxConfPath, "ssl", string.Format("{0}.key", siteName));
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
                                removeDockerInstance(di.instanceId, Path.Combine(RunConfig.Instance.serverHostResource.appBaseDir, di.baseDir));

                                break;
                        }
                        nodeClient.Instance.reportTaskFinish(ht.Id, siteDirName);
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(FrmLib.Extend.tools_static.getExceptionMessage(exp));
                        Console.WriteLine("now report TaskFail");
                        if (!string.IsNullOrEmpty(ht.dockerInanceId))
                        {
                            stopDockerInstance(ht.dockerInanceId);
                            removeDockerInstance(ht.dockerInanceId, appbasepath);
                        }
                        nodeClient.Instance.reportTaskFail(ht.Id, exp.Message);
                    }
                }
            }
            finally
            {
                lock (lockobj)
                    runingTaskCount--;

            }
        }
        public static void reportPerf()
        {
            if (needRestart)
                return;
            try
            {
                lock (lockobj)
                    runingTaskCount++;
            nodeClient.Instance.ReportPerf();
            }
            finally
            {
                lock (lockobj)
                    runingTaskCount--;

            }
        }
        public static void sayAlive()
        {
            if (needRestart)
                return;
            try
            {
                lock (lockobj)
                    runingTaskCount++;
               nodeClient.Instance.SayAlive();
            }
            finally
            {
                lock (lockobj)
                    runingTaskCount--;

            }
            

        }
    }
}
