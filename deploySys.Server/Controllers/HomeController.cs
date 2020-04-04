using Ace;
using Ace.Application;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Ace.Web;
using Microsoft.AspNetCore.Http;
using deploySys.Model;
using Ace.Web.Mvc.Common;
using System.IO;
using System.Threading;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.SwaggerGen;

using System.Net.Http.Headers;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Swashbuckle.AspNetCore.Annotations;
using Ace.Web.Mvc;
using Application.Model.Base;

namespace deploySys.Server.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]

    public class HomeController : BaseController
    {
        private IDatabase _redisDB;
        private IDistributedCache _distributedCache;
        public HomeController(IDistributedCache distributedCache) : base()
        {


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/")]
        [Route("/Home")]
        [Route("/Home/Index")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            SysFunc sf = SysFunc.getInstance(objectSpace);
            SysUser user = sf.getCurrentUser();
            if (user == null)
            {
                return RedirectToAction("login", "Home", new { });
            }
            var cfsimgurl = Url.Action("photo");
            var menus = user.GetMyMenu(0);
            JObject jobj = new JObject();
            jobj.Add("menus", menus);
            var ouser = JObject.FromObject(user);
            if (!string.IsNullOrEmpty(user.Photo))
            {
                ouser["PhotoUrl"] = cfsimgurl + user.Photo;
            }
            jobj.Add("loginuser", ouser);
            return View(jobj);
        }

        /// <summary>
        /// 返回登录视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "View" })]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult login()
        {
            return View();
        }
        /// <summary>
        /// 登录退出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult logout()
        {

            return RedirectToAction("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult test()
        {
            var baspath = AppDomain.CurrentDomain.BaseDirectory;
            var fn = Path.Combine(baspath, "Data", "12.html");
            var content = System.IO.File.ReadAllText(fn);
            HttpContext.Response.WriteAsync(content);
            return null;
        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Zones()
        {

            return View();
        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult appTypes()
        {

            return View();
        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult msApps()
        {

            return View();
        }
        [HttpGet]
        [Route("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult hostPerf([FromRoute]string id)
        {
            ViewBag.hostId = id;
            return View();

        }
        [HttpGet]
        [Route("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult appInstances([FromRoute]string id,[FromQuery]string hostId="-1")
        {
            ViewBag.appId = id;
            ViewBag.hostId = hostId;
            return View();
        }
       
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult hostResouces()
        {

            return View();
        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult regions()
        {
            return View();
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Users()
        {

            return View();
        }
    }
}
