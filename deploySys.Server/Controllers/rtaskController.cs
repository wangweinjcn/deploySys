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

    public class rtaskController : BaseController
    {
        private IDatabase _redisDB;
        private IDistributedCache _distributedCache;
        public rtaskController(IDistributedCache distributedCache) : base()
        {


        }
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult releaseVersions()
        {

            return View();
        }
        [HttpGet]
        [Route("{Id?}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult taskFiles([FromRoute]string Id )
        {
            ViewBag.taskId = Id;
            return View();
        }
        [HttpGet]
        [Route("{Id?}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult taskHosts([FromRoute]string Id,[FromQuery]int hostId=-1)
        {
            ViewBag.taskId = Id;
            ViewBag.hostId = hostId;
            return View();
        }

    }
}
