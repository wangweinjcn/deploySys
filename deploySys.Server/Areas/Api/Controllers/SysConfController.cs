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
using System.Data;
using deploySys.Server.lib;
using System.Text;
using System.Security.Cryptography;

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
#region role
        /// <summary>
        /// 获取角色已有的菜单树
        /// </summary>
        /// <param name="roleId">当前角色ID</param>
        /// <returns>菜单组和菜单组成的树状结构，节点上的Checkstate表示该角色有权限</returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerOperation(Tags = new[] { "System" })]
        public ActionResult RoleMenuTree([FromQuery]string roleId)
        {
            var MgList = this.objectSpace.SpaceQuery<MenuGroup>().ToList();
            var mList = this.objectSpace.SpaceQuery<Menu>().ToList();
            IList<int> authorizedata = new List<int>();
            if (!string.IsNullOrEmpty(roleId))
            {
                SysRole sr = objectSpace.ObjectForId<SysRole>(roleId);

                authorizedata = objectSpace.SpaceQuery<AssC_SysRoleMenu>().Where(a=>a.AssC_SysRole==sr).Select(a=>a.AssC_Menu_Id).ToCommList();
            }
            /*
            var treeList = new List<TreeViewModel>();
            foreach (var module in MgList)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = true;
                tree.Id = "MenuG_" + module.Id.ToString();
                tree.Text = module.Name;
                tree.Value = "MenuG_" + module.Id.ToString();
                tree.ParentId = module.Ass_Parent == null ? null : "MenuG_" + module.Ass_Parent.Id.ToString();
                tree.Isexpand = true;
                tree.Complete = true;
                tree.Showcheck = false;
                tree.HasChildren = hasChildren;
                tree.Img = module.Icon == "" ? "" : module.Icon;
                treeList.Add(tree);
            }
            foreach (var module in mList)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = false;
                tree.Id = module.Id.ToString();
                tree.Text = module.Name;
                tree.Value = module.Id.ToString();
                tree.ParentId = module.Ass_MenuGroup == null ? "-1" : "MenuG_" + module.Ass_MenuGroup.Id.ToString();
                tree.Isexpand = true;
                tree.Complete = true;
                tree.Showcheck = true;
                int menuid = module.Id;
                tree.Checkstate = authorizedata.Count(t => t.Id == menuid);
                tree.HasChildren = hasChildren;
                tree.Img = "";
                treeList.Add(tree);
            }
            */
            return SuccessData(authorizedata);

        }
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
        public ActionResult Roles([FromQuery]Pagination pagination,[FromQuery]string keyword)
        {
            var q = objectSpace.SpaceQuery<SysRole>().Where(a=>!a.IsDeleted);

            if (!string.IsNullOrEmpty(keyword))

                q = q.Where(a => a.Name.Contains(keyword));



            q = q.Distinct().OrderBy(a => a.CreateDt);
            var count = q.Count();
            var list1 = q.ToList();
            string baseurl = this.Url.Action("Roles");
            PagedJObjData pagedData = new PagedJObjData(BaseContextSpace.CsObjToJson(list1)
                as JArray, count, pagination.Page, pagination.PageSize, baseurl);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// [FromBody]
        /// 增加或更新角色
        /// </summary>
        /// <param name="input">
        /// id为空或-1，表示是新增对象；Id大于0，表示更新指定对象
        /// 关联对象Ass_Menu需复制Id字段，表示关联的菜单ID
        /// </param>
        ///<remarks>
        ///input.Ass_Menu 存放
        ///</remarks>
        /// <returns> 返回信息是Id值无效，表示传回的Id找不到对象
        /// </returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerOperation(Tags = new[] { "System" })]

        public ActionResult AuRole([FromForm]SysRole input, [FromForm]string menuIds)


        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            /*
            string json = "";
            using (var sr = new StreamReader(Request.Body))
            {
                json = sr.ReadToEnd();
            }

          //  Model.View_SysRole input = JsonConvert.DeserializeObject<Model.View_SysRole>(json);
            SysRole input = JsonConvert.DeserializeObject<SysRole>(json   );
            */
            IList<int> tmplist = menuIds.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList(); ;

            SysRole onlineobj = objectSpace.ObjectForIntId<SysRole>(input.Id);

            if (onlineobj == null)
            {
                if (input.Id <= 0)
                    onlineobj = new SysRole(objectSpace);
                else
                    return FailedMsg("Id值无效");
            }

            objectSpace.applyValueFromOffObject(onlineobj, input);

            var rolemnlist = objectSpace.SpaceQuery<AssC_SysRoleMenu>().Where(a => a.AssC_SysRole_Id == onlineobj.Id).ToList();


            if (tmplist.Count() > 0)
            {

                foreach (var mid in tmplist)
                {
                   
                    var oldrmenu = (from x in rolemnlist where x.AssC_Menu_Id == mid select x).FirstOrDefault();
                    if (oldrmenu != null)
                    {
                        rolemnlist.Remove(oldrmenu);
                        continue;
                    }
                    var obj = objectSpace.ObjectForIntId<Menu>(mid);
                    if (obj == null)
                        continue;
                    onlineobj.AddMenu(obj);
                }
                foreach (var obj in rolemnlist)
                    obj.choDelete();
            }
            objectSpace.UpdateAllDirtyObjects(true);
            return this.UpdateSuccessMsg();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns></returns>
        [Route("{id?}")]
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "System" })]

        public ActionResult DeleteRole([FromRoute]string id)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            SysRole onlineobj = objectSpace.ObjectForId<SysRole>(id);
            if (onlineobj == null)
            {
                string msg = "input id is null";
                return this.FailedMsg(msg);
            }
            onlineobj.choDelete();
            UpdateDatabase();
            return this.DeleteSuccessMsg();
        }
        #endregion
        #region user

        [HttpGet]

        [SwaggerOperation(Tags = new[] { "System" })]
        public IActionResult getOwnerUer()
        {
            var list = objectSpace.SpaceQuery<SysUser>().Select(a => new { a.Id, a.UserName }).ToCommList();
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            if (string.IsNullOrEmpty(offobj.Key))
                return FailedMsg("key不可以为空");
            if (objectSpace.SpaceQuery<Region>().Where(a => a.Key.ToLower() == offobj.Key.ToLower()).Count() > 0)
                return FailedMsg("重复的Key");
            Region obj = objectSpace.ObjectForIntId<Region>(offobj.Id);
            if (!MetarnetRegex.IsNumAndEnCh(offobj.Key))
            {
                return FailedMsg("应用Key只能使用英文字符和数字");
            }
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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


            var q = objectSpace.SpaceQuery<Zone>().Where(a => !a.IsDeleted);
            if (reg != null)
                q = q.Where(a => a.Ass_Region == reg);
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            Region reg = objectSpace.ObjectForIntId<Region>(regId);
            if (reg == null)
                return FailedMsg("区域不存在");
            if (string.IsNullOrEmpty(offobj.Key))
                return FailedMsg("key不可以为空");
            if (objectSpace.SpaceQuery<Zone>().Where(a => a.Key.ToLower() == offobj.Key.ToLower()).Count() > 0)
                return FailedMsg("重复的Key");

            if (!MetarnetRegex.IsNumAndEnCh(offobj.Key))
            {
                return FailedMsg("应用Key只能使用英文字符和数字");
            }
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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
        public IActionResult HostResources([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int Id,[FromQuery]int regionId=-1)
        {
            Zone zo = objectSpace.ObjectForIntId<Zone>(Id);
            Region reg = objectSpace.ObjectForIntId<Region>(regionId);

            var q = objectSpace.SpaceQuery<HostResource>().Where(a => !a.IsDeleted);
            if(zo!=null)
                q=q.Where(a=> a.Ass_Zone == zo);
            if (reg != null)
            {
                q = q.Join<Zone>(JoinType.InnerJoin, (a, b) => a.Ass_Zone == b && b.Ass_Region == reg).Select((a, b) => a);
            }
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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
            if(!string.IsNullOrEmpty(obj.macId))
                obj.macId=obj.macId.Replace(":","").Replace("-","").ToLower();
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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
                    var list = (from x in list1 select new { cpuLoad = x.cpu.loadPer, memoryUsage = x.mem.usePercent, reportDt = x.reportdt } ).OrderBy(a=>a.reportDt).ToList();
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
        public IActionResult auAppType([FromForm]AppType offobj)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            if (string.IsNullOrEmpty(offobj.key))
                return FailedMsg("key不可以为空");
            if (objectSpace.SpaceQuery<AppType>().Where(a => a.key.ToLower() == offobj.key.ToLower() && offobj.Id<0).Count() > 0)
                return FailedMsg("重复的Key");
            if (!MetarnetRegex.IsNumAndEnCh(offobj.key))
            {
                return FailedMsg("应用Key只能使用英文字符和数字");
            }
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
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
        public IActionResult MyMicroServiceApps([FromQuery]string keyword, [FromQuery]Pagination pagination, [FromQuery]int Id)
        {
            AppType at = objectSpace.ObjectForIntId<AppType>(Id);

             var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            var q = objectSpace.SpaceQuery<MicroServiceApp>().Where(a => !a.IsDeleted);
            if (at != null)
                q = q.Where(a => a.Ass_AppType == at);
            if (!curruser.IsAdmin())
                q = q.Where(a => a.OwnerId == curruser.Id.ToString());
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.appName.Contains(keyword) || a.hostname.Contains(keyword) || a.memo.Contains(keyword) || a.key.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            var allids = (from x in list select x.Id).ToList();
            var allregionapps = objectSpace.SpaceQuery<RegionApps>().Where(a => allids.Contains(a.AssC_MicroServiceApp_Id)).Select(a => a.AssC_Region_Id).ToCommList();
            var listUserId = (from x in list select x.OwnerId.ToInt32()).ToList();
            var listUserName = objectSpace.SpaceQuery<SysUser>().Where(a => listUserId.Contains(a.Id)).Select(a => new { a.Id, a.UserName }).ToCommList();
            foreach (var apps in list)
            {
                apps.regionIds = string.Join(",", (from x in allregionapps select x.ToString()).ToArray());
                if (!string.IsNullOrEmpty(apps.OwnerId))
                    apps.UserName = (from x in listUserName where x.Id.ToString() == apps.OwnerId select x.UserName).FirstOrDefault();
            }
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }

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
            var q = objectSpace.SpaceQuery<MicroServiceApp>().Where(a => !a.IsDeleted);
            AppType at = objectSpace.ObjectForIntId<AppType>(Id);
            if (at != null)
                q = q.Where(a => a.Ass_AppType == at);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.appName.Contains(keyword)|| a.hostname.Contains(keyword) || a.memo.Contains(keyword) || a.key.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            var allids = (from x in list select x.Id).ToList();
            var allregionapps = objectSpace.SpaceQuery<RegionApps>().Where(a => allids.Contains(a.AssC_MicroServiceApp_Id)).Select(a => a.AssC_Region_Id).ToCommList();
            var listUserId = (from x in list select x.OwnerId.ToInt32()).ToList();
            var listUserName = objectSpace.SpaceQuery<SysUser>().Where(a => listUserId.Contains(a.Id)).Select(a => new { a.Id, a.UserName }).ToCommList();
            foreach (var apps in list)
            {
                apps.regionIds = string.Join(",", (from x in allregionapps select x.ToString()).ToArray());
                if(!string.IsNullOrEmpty(apps.OwnerId))
                 apps.UserName = (from x in listUserName where x.Id.ToString() == apps.OwnerId select x.UserName).FirstOrDefault();
            }
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
            if ((offobj.serviceType == (int)EnumAppServiceType.webSite
                || offobj.serviceType == (int)EnumAppServiceType.webServiceSite) && string.IsNullOrEmpty(offobj.hostname))
                return FailedMsg("web站点，域名不能为空");
            if (string.IsNullOrEmpty(offobj.key))
                return FailedMsg("key不可以为空");
            if (objectSpace.SpaceQuery<MicroServiceApp>().Where(a => a.key.ToLower() == offobj.key.ToLower() && a.Id!=offobj.Id).Count() > 0)
                return FailedMsg("重复的Key");

            if (!MetarnetRegex.IsNumAndEnCh(offobj.key))
            {
                return FailedMsg("应用Key只能使用英文字符和数字");
            }
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new MicroServiceApp(objectSpace);
              
            }
            else
            {
                if (!curruser.IsAdmin() && (obj.OwnerId != curruser.Id.ToString()))
                    return FailedMsg("你无权修改此应用");
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            if(string.IsNullOrEmpty(obj.OwnerId))
                  obj.OwnerId = curruser.Id.ToString();
            IList<int> newIdlist = null;
            if (!string.IsNullOrEmpty(offobj.regionIds))
                newIdlist = offobj.regionIds.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
            else
                newIdlist = new List<int>();
            var oldregions = objectSpace.SpaceQuery<Region>().Join<RegionApps>(JoinType.InnerJoin, (a, b) => b.AssC_Region == a && b.AssC_MicroServiceApp_Id == obj.Id).Select((a, b) => a.Id).ToCommList();

            if (obj.Id > 0)
            {
                List<int> deleteIdlist = new List<int>();
                foreach (var oid in oldregions)
                {
                    if (!newIdlist.Contains(oid))
                        deleteIdlist.Add(oid);

                }
                objectSpace.BatchDelete<RegionApps>(a => a.AssC_MicroServiceApp == obj && deleteIdlist.Contains(a.AssC_Region_Id));
            }
            foreach (var nid in newIdlist)
            {
                if (!oldregions.Contains(nid))
                {
                    RegionApps ra = new RegionApps(objectSpace);
                    ra.AssC_MicroServiceApp = obj;
                    ra.AssC_Region_Id = nid;
                }
            }
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
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许删除");
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
        #region db

        /// <summary>
        /// 所有数据库服务器
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult DbServers([FromQuery] string keyword, [FromQuery] Pagination pagination)
        {


            var q = objectSpace.SpaceQuery<DbServer>().Where(a => !a.IsDeleted);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.IP.Contains(keyword) || a.dbType.Contains(keyword));
            }

            var count = q.Count();
            var list = q.Select(a=>new {a.IP,a.Name,a.Port,a.dbType,a.rootUser,a.CreateDt,a.UpdateDt,a.CreaterID,a.Id,a.UpdaterID,rootPassword="" }).OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 新增或更改数据库服务器
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auDbServer([FromForm] DbServer offobj)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            if (string.IsNullOrEmpty(offobj.IP) || string.IsNullOrEmpty(offobj.dbType))
                return FailedMsg("IP is null ");
            if (offobj.Id<0 && objectSpace.SpaceQuery<DbServer>().Where(a => a.IP.ToLower() == offobj.IP.ToLower() && a.Port==offobj.Port && a.dbType.ToLower()==offobj.dbType.ToLower() ).Count() > 0)
                return FailedMsg("重复的数据库服务器");
            //if (!MetarnetRegex.IsIPv4(offobj.IP))
            //{
            //    return FailedMsg("应用IP只能使用英文点和数字");
            //}
            DbServer obj = objectSpace.ObjectForIntId<DbServer>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new DbServer(objectSpace);

            }

            obj.IP = offobj.IP;
            obj.Name = offobj.Name;
            obj.Port = offobj.Port;
            obj.dbType = offobj.dbType;
            obj.rootUser = offobj.rootUser;
            if (!string.IsNullOrEmpty(offobj.rootPassword))
            {
                var origPass=this.DecryptByAES(offobj.rootPassword, getAesKey(), getAesIV());
                obj.rootPassword = offobj.rootPassword;
            }
           


            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除数据库服务器
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteDbServer([FromRoute] int Id)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            DbServer obj = objectSpace.ObjectForIntId<DbServer>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<Db>().Where(a => a.Ass_DbServer_Id == Id && !a.IsDeleted).Count();
                    if (x > 0)
                        return FailedMsg("还有Db，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }

        /// <summary>
        /// 所有数据库
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult Dbs([FromQuery] string keyword, [FromQuery] Pagination pagination)
        {


            var q = objectSpace.SpaceQuery<Db>().Where(a => !a.IsDeleted);
            if (!string.IsNullOrEmpty(keyword))
            {
                q = q.Where(a => a.Name.Contains(keyword) || a.User.Contains(keyword));
            }
            var count = q.Count();
            var list = q.OrderByDesc(a => a.CreateDt).Skip(pagination.PageSize * (pagination.Page - 1))
                .Take(pagination.PageSize).ToCommList();
            JArray jarr = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            PagedJObjData pagedData = new PagedJObjData(jarr, count, pagination.Page, pagination.PageSize);
            return this.SuccessData(pagedData);
        }
        /// <summary>
        /// 将指定的16进制字符串转换为byte数组
        /// </summary>
        /// <param name="s">16进制字符串(如：“7F 2C 4A”或“7F2C4A”都可以)</param>
        /// <returns>16进制字符串对应的byte数组</returns>
        private  byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        /// AES解密  
        /// </summary>  
        /// <param name="input">密文字节数组</param>  
        /// <param name="key">密钥（32位）</param>  
        /// <returns>返回解密后的字符串</returns>  
        private  string DecryptByAES(string input, string key,string IV)
        {
            byte[] inputBytes = HexStringToByteArray(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 32));
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = Encoding.UTF8.GetBytes(IV.Substring(0, 16));

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        private string getAesIV()
        {
        var sf = SysFunc.getInstance(objectSpace);
            return sf.getParamValue("AESIV");
        }
        private string getAesKey()
        {
            var sf = SysFunc.getInstance(objectSpace);
            var key = sf.getParamValue("AESKey");
            if (key.Length < 32)
            {
                // 不足32补全
                key = key.PadRight(32, '0');
            }
            else if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return key;
        }
        /// <summary>
        /// 新增或更改数据库
        /// </summary>
        /// <param name="offobj"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult auDb([FromForm] Db offobj)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            var ds = objectSpace.ObjectForIntId<DbServer>(offobj.Ass_DbServer_Id.Value);
            if (ds == null)
                return FailedMsg("数据库服务器不存在");
            if (string.IsNullOrEmpty(offobj.Name))
                return FailedMsg("Name不可以为空");
            if (offobj.Id<0 && objectSpace.SpaceQuery<Db>().Where(a => a.Name.ToLower() == offobj.Name.ToLower()  && a.Ass_DbServer==ds).Count() > 0)
                return FailedMsg("重复的数据库");
            if (!MetarnetRegex.IsNumAndEnCh(offobj.Name))
            {
                return FailedMsg("应用数据库只能使用英文字符和数字");
            }
            Db obj = objectSpace.ObjectForIntId<Db>(offobj.Id);
            if (obj == null)
            {
                if (offobj.Id > 0)
                    return FailedMsg("找不到对象");
                obj = new Db(objectSpace);
            }
            objectSpace.applyValueFromOffObject(obj, offobj);
            var origp = this.DecryptByAES(offobj.Password, getAesKey(), getAesIV());
            var origServerp = this.DecryptByAES(ds.rootPassword, getAesKey(), getAesIV());
            if (offobj.needCreateDb && offobj.Status==0)
            {
                var bdb = baseDbTools.getInstance(ds.dbType, ds.IP, ds.Port.ToString(), ds.rootUser, origServerp);
           
                bdb.createDbWithUser(obj.Name, obj.Ecoder, obj.User, origp);
                obj.Status = 1;
            }
            UpdateDatabase();
            return SuccessData(obj);
        }
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{Id?}")]
        [SwaggerOperation(Tags = new[] { "ProductParams" })]
        public IActionResult deleteDb([FromRoute] int Id)
        {
            var curruser = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (curruser == null || !curruser.IsAdmin())
                return FailedMsg("不是管理员不允许修改");
            Db obj = objectSpace.ObjectForIntId<Db>(Id);
            if (obj != null)
            {
                if (!obj.IsDeleted)
                {
                    var x = objectSpace.SpaceQuery<ReleaseTask>().Where(a => a.DbId == Id && !a.IsDeleted && (a.status==0 || a.status==10)).Count();
                    if (x > 0)
                        return FailedMsg("还有待处理的发布任务，不能删除");
                }
                obj.IsDeleted = true;
                UpdateDatabase();

            }
            return SuccessMsg();
        }

        #endregion
    }

}
