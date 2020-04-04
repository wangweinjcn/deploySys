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

    public class SysConfController : apiController
    {
        //private IDatabase _redisDB;
        //private IDistributedCache _distributedCache;
        public SysConfController( ) : base()
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
        #region user
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="keyword">角色名字的包含的词搜索，可选
        /// </param>
        /// <param name="pagination">
        /// 分页参数
        /// </param>
        /// <returns>
        /// json对象，data为返回数组
        /// </returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerOperation(Tags = new[] { "System" })]
        public ActionResult Roles([FromQuery]string keyword)
        {
            var q = objectSpace.SpaceQuery<SysRole>().Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(keyword))

                q = q.Where(a => a.Name.Contains(keyword));



            var list = q.ToCommList();

            return SuccessData(list);
        }
        /// <summary>
        /// [FromForm]
        /// 添加或修改用户
        /// </summary>
        /// <param name="input">
        /// id为空或-1，表示是新增对象；Id大于0，表示更新指定对象
        /// </param>
        /// <param name="Password">
        /// 密码的md5格式
        /// </param>
        /// <param name="roleid">
        /// 角色Id,使用逗号分开的字符串列表,EnumAccountRole；
        /// </param>
        /// <param name="deptId">归属部门Id</param>
        /// <param name="sitekey"> http头传输当前应用的Key值
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "System" })]
        public ActionResult AuUser([FromForm]SysUser input, [FromForm]string roleid, [FromForm]string Password,[FromHeader]string sitekey)
        {
            IList<int> tmplist = roleid.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList(); ;

            var setrolelist = objectSpace.SpaceQuery<SysRole>().Where(a => tmplist.Contains(a.Id)).ToList();
            if (setrolelist == null || setrolelist.Count() < 1)
                return FailedMsg("错误的角色ID");
            if (string.IsNullOrEmpty(sitekey))
                sitekey = Globals.Configuration["siteKey"];

            var sf = SysFunc.getInstance(objectSpace);

            SysUser onlineobj = objectSpace.ObjectForIntId<SysUser>(input.Id);
            if (onlineobj == null)
            {
                if (input.Id <= 0)
                {

                  
                    onlineobj = sf.AddUser(input.LoginId, input.Mobile, input.UserName, Password, input.Email, input.sex, 0, input.OtherLoginId,sitekey);
                   

                }
                else
                    return FailedMsg("Id值无效");
            }
            else
            {
                onlineobj.sex = input.sex;
                onlineobj.UserName = input.UserName;
                onlineobj.Email = input.Email;
                onlineobj.Mobile = input.Mobile;
                onlineobj.OtherLoginId = input.OtherLoginId;
                if (!string.IsNullOrEmpty(Password))
                    onlineobj.Password = AesTools.AesEncrypt(Password.ToUpper(), onlineobj.secretKey);

            }
            var oldlist = onlineobj.Ass_SysRole;
            IList<SysRole> needdelete = new List<SysRole>();
            foreach (var oldrole in oldlist)
            {
               
                var tmp = (from x in setrolelist where x.Id == oldrole.Id select x).FirstOrDefault();
                if (tmp == null)
                    oldrole.choDelete();
                else
                    setrolelist.Remove(tmp);
            }
            foreach (var tmp in setrolelist)
            {
              
                onlineobj.SetRole(tmp);
            }                       
            UpdateDatabase();
            return this.AddSuccessMsg();
        }
        /// <summary>
        /// 用户详细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "System" })]
        public ActionResult UserDetail([FromRoute]string id)
        {
            var obj = objectSpace.ObjectForId<SysUser>(id);
            if (obj == null)
                return FailedMsg("");
            var deptId = objectSpace.SpaceQuery<AssC_SysUserDepartment>().Where(a => a.AssC_SysUser == obj).Select(a => a.AssC_Department_Id).FirstOrDefault();
            var allroleId = objectSpace.SpaceQuery<AssC_SysRoleSysUser>().Where(a => a.AssC_SysUser == obj).Select(a => a.AssC_SysRole_Id).ToCommList();
            var jobj = JObject.FromObject(obj);
            jobj.Add("deptId", deptId);
            jobj.Add("allroles", JArray.FromObject(allroleId));
            return SuccessData(jobj);


        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "System" })]
        public ActionResult DeleteUser([FromRoute]string id)
        {
            SysUser onlineobj = objectSpace.ObjectForId<SysUser>(id);
            if (onlineobj == null)
            {
                string msg = "input id is null";
                return this.FailedMsg(msg);
            }
            onlineobj.choDelete();
            UpdateDatabase();
            return this.DeleteSuccessMsg();
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "System" })]
        public ActionResult Users([FromQuery]Pagination pagination, [FromQuery]string keyword)
        {
            var q = objectSpace.SpaceQuery<AssC_SysRoleSysUser>().Join <SysUser>(Chloe.JoinType.InnerJoin,(a,b)=>a.AssC_SysUser==b).Join<SysRole>(Chloe.JoinType.InnerJoin,(a,b,c)=>a.AssC_SysRole==c ).Select((a,b,c)=>b);

            if (!string.IsNullOrEmpty(keyword))
                q = q.Where(a => a.LoginId.Contains(keyword)
                || a.UserName.Contains(keyword)
                || a.Email.Contains(keyword)
                || a.Mobile.Contains(keyword)
                || a.Id.ToString() == keyword);
            q = q.Distinct().OrderBy(a => a.CreateDt);
            var count = q.Count();
            var list1 = q.ToList();
            string baseurl = this.Url.Action("Users");
            PagedJObjData pagedData = new PagedJObjData(BaseContextSpace.CsObjToJson(list1)
                as JArray, count, pagination.Page, pagination.PageSize, baseurl);
            return this.SuccessData(pagedData);
        }
        #endregion
        #region 区域和zone
        /// <summary>
        /// 所有区域
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult Regions([FromQuery]string keyword, [FromQuery]Pagination pagination)
        {


            var q = objectSpace.SpaceQuery<Region>().Where(a => !a.IsDeleted );
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.Name.Contains(keyword) || a.Key.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 新增或更改区域
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auRegion([FromForm]Region offobj)
        {
            Region obj = objectSpace.ObjectForIntId<Region>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new Region(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);

            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteRegion([FromRoute]int Id)
        {
            Region obj = objectSpace.ObjectForIntId<Region>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<Zone>().Where(a => a.Ass_Region_Id == Id && !a.IsDeleted).Count();
                    if (x > 0)
                        return FailedMsg("还有zone，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }

        /// <summary>
        /// 所有Zone
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult Zones([FromQuery]string keyword, [FromQuery]Pagination pagination,[FromQuery]int Id)
        {
            Region reg = objectSpace.ObjectForIntId<Region>(Id);
            if (reg == null)
                return FailedMsg("区域不存在");

            var q = objectSpace.SpaceQuery<Zone>().Where(a => !a.IsDeleted && a.Ass_Region==reg );
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.Name.Contains(keyword) || a.Key.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 新增或更改Zone
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{regId}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auZone( [FromForm]Zone offobj,[FromRoute]int regId)
        {
            Region reg = objectSpace.ObjectForIntId<Region>(regId);
            if (reg == null)
                return FailedMsg("区域不存在");
            Zone obj = objectSpace.ObjectForIntId<Zone>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new Zone(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            obj.Ass_Region = reg;
            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除Zone
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteZone([FromRoute]int Id)
        {
            Zone obj = objectSpace.ObjectForIntId<Zone>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<HostResource>().Where(a => a.Ass_Zone_Id == Id && !a.IsDeleted).Count();
                    if (x > 0)
                        return FailedMsg("还有HostResource，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }
        #endregion

        #region 主机资源
        /// <summary>
        /// 所有HostResource
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]     
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult HostResources([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int Id)
        {
            Zone zo = objectSpace.ObjectForIntId<Zone>(Id);
            if (zo == null)
                return FailedMsg("区域不存在");

            var q = objectSpace.SpaceQuery<HostResource>().Where(a => !a.IsDeleted && a.Ass_Zone == zo);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.macId.Contains(keyword) || a.hostName.Contains(keyword) || a.IP.Contains(keyword) || a.clientSessionId.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            foreach (var one in list)
            {
                var und = RunConfig.Instance.getUNDByMac(one.macId);
                if (und != null)
                {
                    one.lastRegisteDt = und.LastRegisterDt;
                    one.aliveDt = und.AliveDt;
                    if ((DateTime.Now - one.aliveDt.Value).TotalMinutes <= RunConfig.Instance.maxOfflineMinutes)
                        one.inLine = true;
                    else
                        one.inLine = false;

                }
                else
                    one.inLine = false;
            }
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 新增或更改HostResource
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{zoneId}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auHostResource([FromForm]HostResource offobj, [FromRoute]int zoneId)
        {
            Zone zo = objectSpace.ObjectForIntId<Zone>(zoneId);
            if (zo == null)
                return FailedMsg("区域不存在");
            HostResource obj = objectSpace.ObjectForIntId<HostResource>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new HostResource(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            obj.Ass_Zone = zo;
            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除HostResource
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteHostResource([FromRoute]int Id)
        {
            HostResource obj = objectSpace.ObjectForIntId<HostResource>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<DockerInstance>().Where(a => a.Ass_HostResource_Id == Id && !a.IsDeleted).Count();
                    if (x > 0)
                        return FailedMsg("还有HostResource，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }
        [HttpGet]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult HostResourcePerfs([FromRoute]int Id)
        {
            HostResource obj = objectSpace.ObjectForIntId<HostResource>(Id);
            if (obj != null)
            {
                if (RunConfig.Instance.nodedeviceStat_dic.ContainsKey(obj.macId))
                {
                    var list1 = RunConfig.Instance.nodedeviceStat_dic[obj.macId].und.metrics.getlist();
                    var list = (from x in list1 select new { cpuLoad= x.cpu.loadPer, memoryUsage= x.mem.usePercent,reportDt= x.reportdt.ToString("yyyyMMdd hhmmss") } ).OrderBy(a=>a.reportDt).ToList();
                    return SuccessData(list);
                }

            }
            return FailedMsg("找不到主机");
        }

        #endregion

        #region 应用类型
        /// <summary>
        /// 所有应用类型
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult AppTypes([FromQuery]string keyword, [FromQuery]Pagination pagination)
        {


            var q = objectSpace.SpaceQuery<AppType>().Where(a => !a.IsDeleted);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.name.Contains(keyword) || a.key.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 新增或更改应用类型
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auAppType([FromBody]AppType offobj)
        {
            AppType obj = objectSpace.ObjectForIntId<AppType>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new AppType(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);

            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除应用类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteAppType([FromRoute]int Id)
        {
            AppType obj = objectSpace.ObjectForIntId<AppType>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<MicroServiceApp>().Where(a => a.Ass_AppType_Id == Id && !a.IsDeleted).Count();
                    if (x > 0)
                        return FailedMsg("还有zone，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }
        #endregion
        #region 微服务应用
        /// <summary>
        /// 所有微服务应用
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
     
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult MicroServiceApps([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int Id)
        {
            AppType at = objectSpace.ObjectForIntId<AppType>(Id);
            if (at == null)
                return FailedMsg("应用类型不存在");

            var q = objectSpace.SpaceQuery<MicroServiceApp>().Where(a => !a.IsDeleted && a.Ass_AppType == at);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.appName.Contains(keyword) || a.memo.Contains(keyword) || a.key.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        
        /// <summary>
        ///获取该app对应的版本列表，最多100条；时间倒序
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{appId}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult lastAppVersions([FromRoute]int appId)
        {
            MicroServiceApp obj = objectSpace.ObjectForIntId<MicroServiceApp>(appId);
            if (obj == null)
                return FailedMsg("");
            var list = objectSpace.SpaceQuery<appVersion>().Where(a => a.Ass_MicroServiceApp == obj && !a.IsDeleted).OrderByDesc(a => a.CreateDt).Take(100).ToCommList();
            return this.SuccessData(list);
        }
        /// <summary>
        /// 新增或更改微服务应用
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{regId}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auMicroServiceApp([FromForm]MicroServiceApp offobj, [FromRoute]int regId)
        {

            AppType reg = objectSpace.ObjectForIntId<AppType>(regId);
            if (reg == null)
                return FailedMsg("区域不存在");
            MicroServiceApp obj = objectSpace.ObjectForIntId<MicroServiceApp>(offobj.Id);

            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new MicroServiceApp(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            if(!string.IsNullOrEmpty(obj.confFileNames))
                obj.confFileNames = obj.confFileNames.Replace("，", ";").Replace(",", ";").Replace("；", ";");
            if (!string.IsNullOrEmpty(obj.hostname))
                obj.hostname = obj.hostname.Replace("，", ";").Replace(",", ";").Replace("；", ";");

            obj.Ass_AppType = reg;
            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除微服务应用
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteMicroServiceApp([FromRoute]int Id)
        {
            MicroServiceApp obj = objectSpace.ObjectForIntId<MicroServiceApp>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<DockerInstance>().Where(a => a.Ass_MicroServiceApp_Id == Id && !a.IsDeleted ).Count();
                    if (x > 0)
                        return FailedMsg("还有HostResource，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }
        #endregion
       
    }

}
