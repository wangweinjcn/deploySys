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
        public IActionResult ReleaseTasks([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int regId=-1,[FromQuery]int zoneId=-1,[FromQuery]int serverAppId=-1)
        {


            var q = objectSpace.SpaceQuery<ReleaseTask>().Where(a => !a.IsDeleted );
            if (regId > 0)
                q = q.Where(a => a.Ass_Region_Id == regId);
            if (zoneId > 0)
                q = q.Where(a => a.Ass_Zone_Id == zoneId);
            if (serverAppId > 0)
                q = q.Where(a => a.Ass_MicroServiceApp_Id == serverAppId);
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
        /// <summary>
        /// 新增或更改ReleaseTask
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auReleaseTask(IList<IFormFile> importfile1,[FromForm]ReleaseTask offobj, [FromForm]int regId,[FromForm]int zoneId,[FromForm]int serverAppId)
        {
            Region reg = objectSpace.ObjectForIntId<Region>(regId);
            if (reg == null)
                return FailedMsg("区域不存在");
            Zone zo = objectSpace.ObjectForIntId<Zone>(zoneId);
            if (zo == null)
                return FailedMsg("zo不存在");
            MicroServiceApp msa = objectSpace.ObjectForIntId<MicroServiceApp>(serverAppId);
            if (msa == null)
                return FailedMsg("应用不存在");
            var count1 = objectSpace.SpaceQuery<appVersion>().Where(a => a.Ass_MicroServiceApp == msa && a.version == offobj.Version).Count();
            var count2 = objectSpace.SpaceQuery<ReleaseTask>().Where(a => a.Ass_MicroServiceApp == msa && a.Version == offobj.Version).Count();
            if (count1 > 0 || count2>0)
                return FailedMsg("该应用有重复的版本号");

            SysFunc sf = SysFunc.getInstance(objectSpace);
            var fullfn = Path.GetFileName(importfile1[0].FileName);

            var fn = System.IO.Path.GetFileName(fullfn);
            FileVDir o = null;
            using (MemoryStream fs = new MemoryStream())
            {
                importfile1[0].CopyTo(fs);

                byte[] content = FrmLib.Extend.tools_static.StreamToBytes(fs);
                o = sf.AddNewFile(content, fn);
                UpdateDatabase();
            }


            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new ReleaseTask(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            obj.FileGuid = o.Guid;
            obj.FileName = fullfn;
            obj.Ass_Zone = zo;
            obj.Ass_Region = reg;
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
                obj.IsDeleted = true;
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
            if (obj != null)
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
        /// <param name="Id"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult taskHosts([FromQuery]string keyword, [FromRoute]int Id, [FromQuery]Pagination pagination)
        {
            ReleaseTask obj = objectSpace.ObjectForIntId<ReleaseTask>(Id);
            if (obj != null)
            {
                var q = objectSpace.SpaceQuery<HostTask>().Join<ReleaseTask>(JoinType.InnerJoin, (a, b) => a.Ass_ReleaseTask == b && b == obj).Join<HostResource>(JoinType.InnerJoin,(a,b,c)=>a.HostId==c.Id).Select((a, b,c) => new {a.Id,a.CreateDt,a.allocPort,a.dockerInanceId,a.Status,b.Version,c.IP,c.hostName });
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
                return FailedMsg("");
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

            var q = objectSpace.SpaceQuery<DockerInstance>().Where(a => !a.IsDeleted ).Join<HostResource>(JoinType.InnerJoin, (a, b) => a.Ass_HostResource == b).Select((a, b) => new { DockerInstance = a, HostResource = b });
            if (appId > 0)
            {
                q = q.Where(a => a.DockerInstance.Ass_MicroServiceApp_Id == appId);
            }
            if (hostId > 0)
            {
                q = q.Where(a => a.HostResource.Id == hostId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.DockerInstance.baseDir.Contains(keyword) || a.DockerInstance.instanceId.Contains(keyword) || a.HostResource.appBaseDir.Contains(keyword) || a.HostResource.hostName.Contains(keyword) || a.HostResource.IP.Contains(keyword) || a.HostResource.clientSessionId.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.DockerInstance.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult stopInstance([FromRoute]string Id)
        {
            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            di.status = (int)EnumDockerInstanceStatus.waitForTaskStop;
            HostTask ht = new HostTask(objectSpace);
            ht.dockerInanceId = di.instanceId;
            ht.taskType = (int)EnumHostTaskType.stopDockerInstance;
            ht.HostId = di.Ass_HostResource_Id.Value;
            UpdateDatabase();
            return SuccessMsg();
        }
        [HttpPost]
        [Route("{Id}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult restartInstance([FromRoute]string Id)
        {
            DockerInstance di = objectSpace.ObjectForId<DockerInstance>(Id);
            if (di == null)
                return FailedMsg("实例不存在");
            di.status = (int)EnumDockerInstanceStatus.waitForTaskRestart;
            HostTask ht = new HostTask(objectSpace);
            ht.dockerInanceId = di.instanceId;
            ht.taskType = (int)EnumHostTaskType.restartDockerInstance;
            ht.HostId = di.Ass_HostResource_Id.Value;
            UpdateDatabase();
            return SuccessMsg();

        }
        #endregion
    }

}
