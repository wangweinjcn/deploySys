using Ace;

using Ace.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Ace.Web;
using Microsoft.AspNetCore.Http;
using Application.Model.Base;
using Chloe.Ext;
using Ace.Web.Mvc.Common;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Ace.Web.Mvc.Common.Tree;

using Application.Model.Base;
using Swashbuckle.AspNetCore.SwaggerGen;
using Application.Model.Workflow;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using FrmLib.Encrypt;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Chloe.ObjectSpace;
using Ace.Web.Mvc;
using deploySys.Model;
using Chloe;

namespace deploySys.Server.Controller.Admin
{
    /// <summary>
    /// 平台维护管理
    /// </summary>

    [Area(AreaNames.Api)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme+","+CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[Area]/[controller]/[action]")]

    public class SysManageController: apiController
    {
        //private IDatabase _redisDB;
        //private IDistributedCache _distributedCache;
        public SysManageController( ) : base()
        {

            
        }

        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        [NonAction]
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }



        #region 任务
        /// <summary>
        /// 所有ReleaseTask
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult ReleaseTasks([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int regionId=-1,[FromQuery]int Id=-1,[FromQuery]int msAppId=-1,[FromQuery]int appTypeId=-1)
        {

            var zoneId = Id;
            var q = objectSpace.SpaceQuery<ReleaseTask>().Join<MicroServiceApp>(JoinType.InnerJoin,( a,b) => !a.IsDeleted && a.Ass_MicroServiceApp==b  ).Join<AppType>(JoinType.InnerJoin,(a,b,c)=>b.Ass_AppType==c).Select((a,b,c)=>new {a.Id,a.useSSL,a.needRegister, b.Ass_AppType_Id,a.HostIds,a.Ass_Zone_Id,a.Ass_Region_Id,a.Ass_MicroServiceApp_Id,a.count,a.CreateDt,a.CreaterID,a.CreaterName,a.dockerNetType,a.FileGuid,a.FileName,a.memo,a.overridePolicy,a.ProcessInfo,a.releaseType,a.selectHostPolicy,a.status,a.unzipInServer,a.UpdateDt,a.UpdaterID,a.UpdaterName,a.useWIP,a.Version,b.appName,b.key,a.sslKey,a.sslPem,a.domainName,a.confFileOverride});
            if (regionId > 0)
                q = q.Where(a => a.Ass_Region_Id == regionId);
            if (zoneId > 0)
                q = q.Where(a => a.Ass_Zone_Id == zoneId);
            if (msAppId > 0)
                q = q.Where(a => a.Ass_MicroServiceApp_Id == msAppId);
            if (appTypeId > 0)
                q = q.Where(a => a.Ass_AppType_Id == appTypeId);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.Version.Contains(keyword) );
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        [HttpGet]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult getRleaseVersionFile([FromRoute]int Id)
        {
            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj == null)
                return FailedMsg("");

            var content = SysFunc.getInstance(objectSpace).GetFile(obj.FileGuid);
            var memory = new MemoryStream(content);
            

            memory.Position = 0;

            return File(memory, "application/octet-stream", obj.FileName);

        }
        /// <summary>
        /// 新增或更改ReleaseTask
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(1000000000)] //最大100m左右
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auReleaseTask(IList<IFormFile> importfile1,[FromForm]ReleaseTask offobj, [FromForm]int regId,[FromForm]int zoneId,[FromForm]int serverAppId)
        {

            Zone zo = objectSpace.ObjectForIntId<Zone>(zoneId);
            if (zo == null)
                return FailedMsg("zo不存在");
            MicroServiceApp msa = objectSpace.ObjectForIntId<MicroServiceApp>(serverAppId);
            if (msa == null)
                return FailedMsg("应用不存在");
            var sf = SysFunc.getInstance(objectSpace);
            var curruser = sf.getCurrentUser();
            if (curruser == null )
                return FailedMsg("当前用户为空");
            if (!curruser.IsAdmin() && msa.OwnerId != curruser.Id.ToString())
            { 
            return FailedMsg("不可以发布不属于你的应用版本");
            }
            var count1 = objectSpace.SpaceQuery<appVersion>().Where(a => a.Ass_MicroServiceApp == msa && a.version == offobj.Version).Count();
            var count2 = objectSpace.SpaceQuery<ReleaseTask>().Where(a => a.Ass_MicroServiceApp == msa && a.Version == offobj.Version).Count();
            if ( offobj.Id<=0 &&( count1 > 0 || count2>0))
                return FailedMsg("该应用有重复的版本号");

            if (offobj.releaseType == 0 && offobj.count>1 && (msa.serviceType==(int)EnumAppServiceType.webSite ||msa.serviceType==(int)EnumAppServiceType.webServiceSite))
            {
                return FailedMsg("对于需要反向代理的服务或站点，不允许发布数量大于1");

            }
            if (msa.serviceType != (int)EnumAppServiceType.webSite)
            {
                if (offobj.dockerNetType == 0 && string.IsNullOrEmpty(msa.Ass_AppType.createContainerParams))
                {
                    return FailedMsg("不支持创建桥接网络的容器");

                }
                if (offobj.dockerNetType == 1 && string.IsNullOrEmpty(msa.Ass_AppType.createContainerParams2))
                {
                    return FailedMsg("不支持创建主机网络的容器");

                }
            }
            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new ReleaseTask(objectSpace);
            }
            else
            {
                if (obj.status != (int)EnumReleaseTaskStatus.created)
                    return FailedMsg("不能修改");
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            if (!string.IsNullOrEmpty(obj.sslKey) && obj.sslKey.ToLower() == "null")
                obj.sslKey = null;
            if (!string.IsNullOrEmpty(obj.sslKey) && obj.sslPem.ToLower() == "null")
                obj.sslPem = null;
            if (importfile1.Count > 0 && importfile1[0].Length > 0)
            {
                var fullfn = Path.GetFileName(importfile1[0].FileName);

                var fn = System.IO.Path.GetFileName(fullfn);
                FileVDir o = null;
                using (MemoryStream fs = new MemoryStream())
                {

                    importfile1[0].CopyTo(fs);

                    byte[] content = FrmLib.Extend.tools_static.StreamToBytes(fs);
                    o = sf.AddNewFile(content, fn);
                    objectSpace.updateOneDirtyObject(o);
                    obj.FileGuid = o.Guid;
                    obj.FileName = fullfn;
                }
            }
            else
            {
                if (obj.isNew())
                    return FailedMsg("文件不能为空");
            }
          
            obj.Ass_Zone = zo;
            obj.Ass_Region = zo.Ass_Region;
            obj.Ass_MicroServiceApp = msa;
            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除ReleaseTask
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteReleaseTask([FromRoute]int Id)
        {
            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<DockerInstance>().Where(a => a.Ass_ReleaseTask_Id == Id && !a.IsDeleted).Count();
                    if (x > 0)
                        return FailedMsg("还有运行实例，不能删除");
                }
                var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
                if (curruser == null)
                    return FailedMsg("");
                if (!curruser.IsAdmin() && obj.Ass_appVersion.Ass_MicroServiceApp.OwnerId == curruser.Id.ToString())
                {
                    return FailedMsg("不可以删除不属于你的应用版本");
                }
                objectSpace.BatchDelete<HostTask>(a => a.Ass_ReleaseTask == obj);
                if (obj.Ass_appVersion != null)
                    obj.Ass_appVersion.choDelete();
                obj.choDelete();

                UpdateDatabase();

            }
            return SuccessMsg();
        }
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult restartHostTask([FromRoute]int Id)
        {

            HostTask obj = objectSpace.ObjectForIntId<HostTask>(Id);
            if (obj.Ass_ReleaseTask != null)
                return FailedMsg("版本发布必须使用版本重启");
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");
            if (!curruser.IsAdmin() &&(obj.Ass_appVersion==null || obj.Ass_appVersion.Ass_MicroServiceApp==null || obj.Ass_appVersion.Ass_MicroServiceApp.OwnerId == curruser.Id.ToString()))
            {
                return FailedMsg("不可以重启不属于你的应用版本");
            }

            if (obj != null && obj.Status != (int)EnumHostTaskStatus.finished)
            {
                obj.Status = (int)EnumHostTaskStatus.waitForProcess;
               
                UpdateDatabase();

            }
            return SuccessMsg();
        }
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult restartReleaseTask([FromRoute]int Id)
        {

            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj != null && obj.status == (int)EnumReleaseTaskStatus.error)
            {
                var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
                if (curruser == null)
                    return FailedMsg("");
                if (!curruser.IsAdmin() && (obj.Ass_appVersion == null || obj.Ass_appVersion.Ass_MicroServiceApp == null || obj.Ass_appVersion.Ass_MicroServiceApp.OwnerId == curruser.Id.ToString()))
                {
                    return FailedMsg("不可以发布不属于你的应用版本");
                }
                obj.status = (int)EnumReleaseTaskStatus.assigned;
                foreach (var ht in obj.Ass_HostTask)
                {
                    if (ht.Status == (int)EnumHostTaskStatus.failed)
                        ht.Status = (int)EnumHostTaskStatus.waitForProcess;
                }
                UpdateDatabase();

            }
            return SuccessMsg();
        }
        /// <summary>
        /// 审核批准版本发布
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auditReleaseTask([FromRoute]int Id)
        {

            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj != null && obj.status ==(int)EnumReleaseTaskStatus.created)
            {
                obj.status = (int)EnumReleaseTaskStatus.audited;
                UpdateDatabase();
                
            }
            return SuccessMsg();
        }
        /// <summary>
        /// 发布任务的文件列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="Id"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult taskFileItems([FromQuery]string keyword,[FromRoute]int Id, [FromQuery]Pagination pagination)
        {
            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj != null)
            {
                var q = objectSpace.SpaceQuery<FileItem>().Join<appVersion>(JoinType.InnerJoin, (a, b) => a.Ass_appVersion == b && b.Ass_ReleaseTask == obj).Select((a,b)=>a);
                if (!string.IsNullOrEmpty(keyword))
                {
                    q = q.Where(a => a.fileName.Contains(keyword) || a.retationPath.Contains(keyword));
                }
                var count = q.Count();
                var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                    .Take(pagination.PageSize).ToCommList();
                JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
                return this.SuccessData(pagedData);

            }
            else
            {
                return FailedMsg("");
            }
        }
        /// <summary>
        /// 发布任务的主机实例列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="Id">任务ID</param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult taskHosts([FromQuery]string keyword, [FromRoute]int Id,[FromQuery]int hostId, [FromQuery]Pagination pagination)
        {
            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj != null)
            {
                var q = objectSpace.SpaceQuery<HostTask>().Join<ReleaseTask>(JoinType.InnerJoin, (a, b) => a.Ass_ReleaseTask == b && b == obj).Join<HostResource>(JoinType.InnerJoin,(a,b,c)=>a.HostId==c.Id).Select((a, b,c) => new {a.Id,a.CreateDt,a.allocPort,a.dockerInanceId,a.Status,b.Version,c.IP,c.hostName,a.taskType });
                if (!string.IsNullOrEmpty(keyword))
                {
                    q = q.Where(a => a.dockerInanceId.ToString()==keyword || a.allocPort.ToString().Contains(keyword) || a.IP.Contains(keyword));
                }
                var count = q.Count();
                var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                    .Take(pagination.PageSize).ToCommList();
                JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
                return this.SuccessData(pagedData);

            }
            else
            {
                var q = objectSpace.SpaceQuery<HostTask>().Join<ReleaseTask>(JoinType.LeftJoin, (a, b) => a.Ass_ReleaseTask == b).Join<HostResource>(JoinType.InnerJoin, (a, b, c) => a.HostId == c.Id && c.Id==hostId).Select((a, b, c) => new { a.Id, a.CreateDt, a.allocPort, a.dockerInanceId, a.Status,Version= b==null?"": b.Version, c.IP, c.hostName, a.taskType });
                if (!string.IsNullOrEmpty(keyword))
                {
                    q = q.Where(a => a.dockerInanceId.ToString() == keyword || a.allocPort.ToString().Contains(keyword) || a.IP.Contains(keyword));
                }
                var count = q.Count();
                var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                    .Take(pagination.PageSize).ToCommList();
                JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
                return this.SuccessData(pagedData);
              
            }
        }
        #endregion
        #region 实例
        /// <summary>
        /// 微服务应用实例
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult MicroServiceAppsInstance([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int appId=-1,[FromQuery]int hostId=-1)
        {

            var q = objectSpace.SpaceQuery<DockerInstance>().Where(a => !a.IsDeleted ).Join<HostResource>(JoinType.InnerJoin, (a, b) => a.Ass_HostResource == b).Join<MicroServiceApp>(JoinType.LeftJoin,(a,b,c)=>a.Ass_MicroServiceApp==c).Select((a, b,c) => new {a.proxyPort,a.status, a.isNginx,a.version,a.IP,a.domain,a.Id,a.Ass_MicroServiceApp_Id,a.baseDir,a.instanceId,b.appBaseDir,b.hostName,b.macId,hostId=b.Id,a.CreateDt,apps=c,appName=c.appName,key=c.key,port=c.port});
            if (appId > 0)
            {
                q = q.Where(a => a.Ass_MicroServiceApp_Id == appId);
            }
            if (hostId > 0)
            {
                q = q.Where(a => a.hostId == hostId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.baseDir.Contains(keyword) || a.instanceId.Contains(keyword) || a.appBaseDir.Contains(keyword) || a.hostName.Contains(keyword) || a.IP.Contains(keyword) || a.macId.Contains(keyword) || a.apps.key.Contains(keyword) );
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();

            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            foreach (JObject jobj in jarr)
            {
                if (FrmLib.Extend.tools_static.jobjectHaveKey(jobj, "apps") && jobj["apps"].HasValues &&
                    FrmLib.Extend.tools_static.jobjectHaveKey(jobj["apps"] as JObject, "appName"))
                    jobj["appName"] = jobj["apps"]["appName"];
                if (jobj["isNginx"].ToObject<bool>())
                    jobj["appName"] = "Nginx";
            }
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult stopInstance([FromRoute]string Id)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");
            if (!curruser.IsAdmin())
            {
                return FailedMsg("只可以管理员操作");
            }
            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            di.status = (int)EnumDockerInstanceStatus.waitForTaskStop;
            HostTask ht = new HostTask(objectSpace);
            ht.dockerInanceId = di.instanceId;
            ht.taskType = (int)EnumHostTaskType.stopDockerInstance;
            ht.HostId = di.Ass_HostResource_Id.Value;
            ht.Ass_appVersion =di.Ass_ReleaseTask!=null? di.Ass_ReleaseTask.Ass_appVersion:null;
            UpdateDatabase();
            return SuccessMsg();
        }
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult restartInstance([FromRoute]string Id)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");
            if (!curruser.IsAdmin())
            {
                return FailedMsg("只可以管理员操作");
            }

            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            di.status = (int)EnumDockerInstanceStatus.waitForTaskRestart;
            HostTask ht = new HostTask(objectSpace);
            ht.dockerInanceId = di.instanceId;
            ht.taskType = (int)EnumHostTaskType.restartDockerInstance;
            ht.HostId = di.Ass_HostResource_Id.Value;
            ht.Ass_appVersion =di.Ass_ReleaseTask!=null? di.Ass_ReleaseTask.Ass_appVersion:null;
            UpdateDatabase();
            return SuccessMsg();

        }
       /// <summary>
       /// 删除实例
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult removeInstance([FromRoute]string Id)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");
            if (!curruser.IsAdmin() )
            {
                return FailedMsg("只可以管理员操作");
            }

            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            if (di.isNginx.Value || di.isNodeMonitor)
                return FailedMsg("系统实例不允许删除");
             di.status = (int)EnumDockerInstanceStatus.waitForTaskRemove;
            
            HostTask ht = new HostTask(objectSpace);
            ht.dockerInanceId = di.instanceId;
            ht.taskType = (int)EnumHostTaskType.removeDockerInstance;
            ht.HostId = di.Ass_HostResource_Id.Value;

            ht.taskParms = string.Format("{0}_{1}", di.Ass_MicroServiceApp.key,di.version);
            if (di.Ass_webSite.Count > 0)
            {
                foreach (var ws in di.Ass_webSite)
                {
                    ws.status = (int)EnumDockerInstanceStatus.waitForTaskRemove;

                    HostTask ht2 = new HostTask(objectSpace);

                    ht2.taskType = (int)EnumHostTaskType.removeNgixSite;
                    ht2.dockerInanceId = ws.Ass_DockerInstance.instanceId;
                    ht2.HostId = ws.Ass_DockerInstance.Ass_HostResource_Id.Value;
                    JObject jobj = JObject.FromObject(new {Id=ws.Id,siteKey= string.Format("{0}_{1}", ws.Ass_MicroServiceApp.key, ws.version)});
                    ht2.taskParms = jobj.ToString();
                }
            }

            UpdateDatabase();
            return SuccessMsg();

        }
        #endregion
        #region 站点
        /// <summary>
        /// 删除站点
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult removeWebSite([FromRoute]string Id)
        {
            //TODO 删除站点
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");
            if (!curruser.IsAdmin())
            {
                return FailedMsg("只可以管理员操作");
            }
            webSite ws = objectSpace.ObjectForId<webSite>(Id);
            if (ws == null)
                return FailedMsg("站点不存在");
            ws.status = (int)EnumDockerInstanceStatus.waitForTaskRemove;

            HostTask ht = new HostTask(objectSpace);          
            ht.taskType = (int)EnumHostTaskType.removeNgixSite;
            if (ws.Ass_DockerInstance == null)
            {
                return FailedMsg("实例不存在");
            }
            ht.dockerInanceId = ws.Ass_DockerInstance.instanceId;
            
            ht.HostId = ws.Ass_DockerInstance.Ass_HostResource_Id.Value;
                
            JObject jobj = JObject.FromObject(new { Id = ws.Id, siteKey = string.Format("{0}_{1}", ws.Ass_MicroServiceApp.key, ws.version) });
            ht.taskParms = jobj.ToString();
            UpdateDatabase();
            return SuccessMsg();

        }

        /// <summary>
        /// 微服务应用站点
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult webSites([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int appId = -1, [FromQuery]int hostId = -1)
        {
            var q = (objectSpace as BaseContextSpace).JoinQuery<webSite, DockerInstance, MicroServiceApp>((a, b, c) => new object[] { JoinType.LeftJoin, a.Ass_DockerInstance == b, JoinType.LeftJoin, a.Ass_MicroServiceApp == c }).Select((a,b,c)=>new {webSite=a,DockerInstance=b,MicroServiceApp=c });
            if (appId > 0)
                q = q.Where(a => a.MicroServiceApp.Id == appId);
            if (hostId > 0)
                q = q.Where(a => a.DockerInstance.Ass_HostResource_Id == hostId);
            var  q2 = q.Select((a) => new { a.webSite.Id, a.webSite.siteDirName,a.webSite.domain,a.webSite.version,a.webSite.status,a.webSite.isWebService,a.MicroServiceApp.appName,a.webSite.CreateDt,a.webSite.CreaterName });
           
            if (!string.IsNullOrEmpty(keyword))
            {
                q2 = q2.Where(a => a.domain.Contains(keyword) || a.version.Contains(keyword) || a.appName.Contains(keyword));
            }
            var count = q2.Count();
            var list = q2.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();

            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
           
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }

        #endregion
        #region 日志
        /// <summary>
        /// 获取实例日志
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult getLogs([FromRoute] string Id,[FromQuery]Pagination pagination)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");
            
            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            if (!curruser.IsAdmin() && (di.Ass_MicroServiceApp.OwnerId != curruser.Id.ToString()))
            {
                return FailedMsg("不可以获取不属于你管理的应用日志");
            }
            var macid = di.Ass_HostResource.macId;
            if (!RunConfig.Instance.nodedeviceStat_dic.ContainsKey(macid))
            {
                return FailedMsg("节点未上线");
            }
            var session = RunConfig.Instance.nodedeviceStat_dic[macid];
            var logdir = Path.Combine(di.Ass_HostResource.appBaseDir,di.baseDir, di.Ass_MicroServiceApp.LogRelationDir);
            var tmpstr = session.session.InvokeApi<string>("getlogs", logdir).GetResult();
            if (string.IsNullOrEmpty(tmpstr))
                return FailedMsg("");
            var jarr = JArray.Parse(tmpstr);
            PagedJObjData pagedData = new PagedJObjData(jarr, jarr.Count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);

        }
        /// <summary>
        /// 删除日志文件
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteLogFile([FromRoute] string Id, [FromForm] string fn)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");

            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            if (!curruser.IsAdmin() && (di.Ass_MicroServiceApp.OwnerId != curruser.Id.ToString()))
            {
                return FailedMsg("不可删除不属于你管理的应用日志");
            }
            var macid = di.Ass_HostResource.macId;
            if (!RunConfig.Instance.nodedeviceStat_dic.ContainsKey(macid))
            {
                return FailedMsg("节点未上线");
            }
            var session = RunConfig.Instance.nodedeviceStat_dic[macid];
            var res = session.session.InvokeApi<int>("deletelog", fn).GetResult();
            if (res == 0)
                return SuccessMsg("");
            else
                return FailedMsg();
        }
            /// <summary>
            /// 获取文件内容
            /// </summary>
            /// <param name="Id"></param>
            /// <param name="fn"></param>
            /// <returns></returns>
            [HttpGet]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult getLogContent([FromRoute] string Id, [FromQuery] string fn)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null)
                return FailedMsg("");

            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            if (!curruser.IsAdmin() && (di.Ass_MicroServiceApp.OwnerId != curruser.Id.ToString()))
            {
                return FailedMsg("不可以获取不属于你管理的应用日志");
            }
            var macid = di.Ass_HostResource.macId;
            if (!RunConfig.Instance.nodedeviceStat_dic.ContainsKey(macid))
            {
                return FailedMsg("节点未上线");
            }
            var session = RunConfig.Instance.nodedeviceStat_dic[macid];
            var tmpstr = session.session.InvokeApi<string>("getlog", fn).GetResult();
            FrmLib.Log.commLoger.runLoger.Debug(tmpstr);
            return Content(tmpstr);
        }
        #endregion
    }

}
