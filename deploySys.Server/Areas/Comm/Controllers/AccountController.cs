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
using FrmLib.Encrypt;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Swashbuckle.AspNetCore.Annotations;
using FrmLib.Http;
using Ace.Web.Mvc;

namespace deploySys.Server.Controllers.Comm
{
    [Area(AreaNames.Comm)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[Area]/[controller]/[action]")]
   
    public class AccountController : CsHttpController
    {
        const string key = "message";
        const string message = "hello";



        public AccountController(IDistributedCache distributedCache) : base(distributedCache)
        {


        }
        [SwaggerOperation(Tags = new[] { "Common" })]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult test1()
        {
            return SuccessMsg("test1 ok");
        }
        [SwaggerOperation(Tags = new[] { "Common" })]
        [HttpGet]
         public IActionResult test2()
        {
            return SuccessMsg("test2 ok");
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="loginuser">
        /// loginId :手机号码
        /// Password：新密码(md5)
        /// oldPassword：旧密码(md5)
        /// </param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { "Common" })]
        [HttpPost]
        public ActionResult ChangePassword(string loginId,string Password,string oldPassword )
        {
            //Stream stream = HttpContext.Request.Body;
            //byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
            //stream.Read(buffer, 0, buffer.Length);
            //string temp = Encoding.UTF8.GetString(buffer);
            //if (string.IsNullOrEmpty(temp))
            //    return FailedMsg("参数为空");
            //var jsonparams = JObject.Parse(temp);

            string mobilePhone = loginId;

            string newPassword = Password;
          

            SysFunc sf = SysFunc.getInstance(objectSpace);
            SysUser curruser = objectSpace.SpaceQuery<SysUser>().Where(a => a.IsEnabled)
                .Where(a => a.Mobile == mobilePhone || a.Email == mobilePhone
                || a.LoginId == mobilePhone).FirstOrDefault();
            if (curruser == null)
            {
                return FailedMsg("手机号码不存在");
            }
            if (curruser.Password == AesTools.AesEncrypt(oldPassword.ToUpper(), curruser.secretKey))
            {
                curruser.Password = AesTools.AesEncrypt(newPassword.ToUpper(), curruser.secretKey);
                UpdateDatabase(true);
                return this.SuccessMsg("密码修改成功");
            }
            else
                return FailedMsg("原密码错误");
        }
        /// <summary>
        /// 获取未读信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        
        [SwaggerOperation(Tags = new[] { "Common" })]
        public ActionResult getUnReadMessage()
        {
            SysUser user = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (user == null)
                return FailedMsg("用户为空");
            var list = objectSpace.SpaceQuery<MessageInSite>().Where(a =>
              a.Ass_SysUser == user && !a.haveRead)
              .Select(a => new { a.Id, a.Title, a.CreateDt,a.Content, })
              .OrderByDesc(a => a.CreateDt).ToCommList();
            return SuccessData(list);
        }

        /// <summary>
        /// 标记信息已读
        /// </summary>
        /// <param name="id">信息主键</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public ActionResult ReadMessage([FromRoute]string id)
        {
            SysUser user = SysFunc.getInstance(objectSpace).getCurrentUser();
            if (user == null)
                return FailedMsg("用户为空");
            MessageInSite mis = objectSpace.ObjectForId<MessageInSite>(id);
            if (mis.Ass_SysUser != user)
                return FailedMsg("不是你的消息");
            mis.haveRead = true;
            UpdateDatabase(true);
            return UpdateSuccessMsg();
        }
        /// <summary>
        /// 获取openid
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        [HttpGet]
         [AllowAnonymous]
          [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult getOpenId([FromQuery]string code)
        {
            var oi = weixinOpenid(code);
            var o = new { openId = oi };
            return SuccessData( o);
        }

        internal static string weixinOpenid(string code)
        {
            try
            {
                string appid = Globals.Configuration["WeChatPay:AppId"];
                string appsecret = Globals.Configuration["WeChatPay:Secret"];
                if (code != "")
                {
                    try
                    {
                        string url =
                        string.Format(
                             "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                             appid, appsecret, code);
                        HttpHelper client = new HttpHelper();
                        var res = client.doAsycHttpRequest(url, new JObject(), false, true, null);
                        string returnStr = client.ResponseToString(res);
                        if (string.IsNullOrEmpty(returnStr))
                            return null;

                        JObject jobj = JObject.Parse(returnStr);
                        string openid = "";
                        if (FrmLib.Extend.tools_static.jobjectHaveKey(jobj, "openid"))
                            openid = jobj["openid"].ToString();
                        return openid;
                    }
                    catch (Exception e)
                    {
                        FrmLib.Log.commLoger.runLoger.ErrorFormat("获得openid出错！,{0}", e.Message);
                        return null;
                    }
                }
                else
                {
                    FrmLib.Log.commLoger.devLoger.DebugFormat("openid:is null");

                    return null;
                }
            }
            catch (Exception e)
            {
                FrmLib.Log.commLoger.runLoger.ErrorFormat("未知错误！,{0}", e.Message);
                return null;
            }

        }
            /// <summary>
        /// 用户的jwt登录
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="Password">密码，MD5</param>
        /// <param name="siteKey">应用key值，cbeShop代表前端，cbeShopOss代表后端管理系统 </param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Common", "Common" })]
        public IActionResult LoginWithJwt([FromForm]string loginId, [FromForm]string Password)
        {
            //Stream stream = HttpContext.Request.Body;
            //byte[] buffer = new byte[HttpContext.Request.ContentLength.Value];
            //stream.Read(buffer, 0, buffer.Length);
            //string content = Encoding.UTF8.GetString(buffer);
            //FrmLib.Log.commLoger.devLoger.Debug("content:" + content);
            //JObject jobjtmp = JObject.Parse(content);
            string userName = loginId;
            string password = Password;
            //string verifyCode= jobjtmp["verifycode"].ToString();
            if (string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(password))
                return FailedMsg("错误的用户名密码");

            userName = userName.Trim();
            const string moduleName = "系统登录";
            string ip = this.HttpContext.GetClientIP();
            if (ip.Contains("127.0.0.1") && this.HttpContext.Request.Headers.ContainsKey("x-real-ip"))
                ip = this.HttpContext.Request.Headers["x-real-ip"];
            SysUser user;
            SysFunc sf = SysFunc.getInstance(objectSpace);
            string siteKey;

            if ((user = sf.CheckLogin(userName, password,"")) == null)
            {
                return FailedMsg("用户名密码错误");

            }
            else
            {
                if (!user.IsEnabled)
                    return FailedMsg("你已被停用，请联系系统管理员");
                string roleid = "";
                foreach (var onerole in user.Ass_SysRole)
                {
                    roleid = roleid + "," + onerole.RoleID;
                }
                roleid = roleid.TrimStart(',');
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Globals.Configuration["jwt:Secret"]);
                var expTimestr = Globals.Configuration["Cookie:ExpireSeconds"];
                int expSeconds = 600;
                int.TryParse(expTimestr, out expSeconds);
                var authTime = DateTime.Now;
                var expiresAt = authTime.AddSeconds(expSeconds);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = Globals.Configuration["jwt:Audience"],
                    Issuer = Globals.Configuration["jwt:Issuer"],
                    Subject = new ClaimsIdentity(new Claim[]
                    {

                    new Claim("UserName", user.UserName),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("RoleId", roleid),
                    new Claim(ClaimTypes.Expiration,
                    expiresAt.ToString())
                    }),
                    NotBefore = authTime,
                    Expires = expiresAt,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            
                JwtSecurityToken token = tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;
                var tokenString = tokenHandler.WriteToken(token);
                if (this.Request.Headers.ContainsKey("User-Agent"))
                {
                   ip=string.Format("{0}@{1}", ip, this.Request.Headers["User-Agent"]);
                }
                if (ip.Length > 254)
                    ip = ip.Substring(0, 254);
                user.AddNewLoginHistory(ip, token.RawSignature, expiresAt);
                var cachekey = "loginToken_" + user.Id;
                JObject jobj = new JObject();
                jobj.Add("tokenId", token.RawSignature);
                jobj.Add("valid", true);
                jobj.Add("expiresAt", expiresAt);
                var cacheToken = jobj.ToString();

              
                UpdateDatabase();
                return SuccessData(new
                {
                    access_token = tokenString,
                    token_type = "Bearer",
                    profile = new
                    {
                        sid = user.Id,
                        name = user.UserName,                       
                        auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                        expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds(),
                        roleid = roleid
                    }
                });

            }

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginuser">
        ///LoginId:用户名
        ///Password:密码（md5）
        ///</param>
        /// <param name="sitekey">http请求头中放当前请求的应用Key</param>
        /// <returns></returns>
        ///
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult Login([FromForm]string loginId, [FromForm]string Password, [FromHeader]string sitekey)
        {

            string userName = loginId;
            string password = Password;
            //string verifyCode= jobjtmp["verifycode"].ToString();
            if (string.IsNullOrEmpty(userName)
                || string.IsNullOrEmpty(password))
                return FailedMsg("错误的用户名密码");

            const string moduleName = "系统登录";
            string ip = this.HttpContext.GetClientIP();
            ip = ip == null ? "" : (ip.Length > 20 ? ip.Substring(0, 20) : ip);

            SysUser user;
            string msg = "ok";
            SysFunc sf = SysFunc.getInstance(objectSpace);
            if ((user = sf.CheckLogin(userName, password, sitekey)) == null)
            {
                // this.CreateService<ISysLogAppService>().LogAsync(null, null, ip, LogType.Login, moduleName, false, "用户[{0}]登录失败：{1}".ToFormat(LoginId, msg));
                msg = "错误的用户名密码";
                return this.FailedMsg(msg);
            }


            AdminSession session = new AdminSession();
            session.UserId = user.Id.ToString();
            session.UserName = user.UserName;
            session.RealName = user.LoginId;
            //session.DepartmentId = user.DepartmentId;
            //session.DutyId = user.DutyId;
            string roleid = "";
            foreach (var onerole in user.Ass_SysRole)
            {
                roleid = roleid + "," + onerole.RoleID;
            }
            session.RoleId = roleid;
            session.LoginIP = ip;
            session.LoginTime = DateTime.Now;
            session.IsAdmin = user.LoginId.ToLower() == "admin";

            this.CurrentSession = session;

            user.AddNewLoginHistory(ip, session.sessionID, this.CurrentSession.expTime);
            UpdateDatabase();



            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Globals.Configuration["jwt:Secret"]);
            var expTimestr = Globals.Configuration["Cookie:ExpireSeconds"];
            int expSeconds = 600;
            int.TryParse(expTimestr, out expSeconds);
            var authTime = DateTime.Now;
            var expiresAt = authTime.AddSeconds(expSeconds);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = Globals.Configuration["jwt:Audience"],
                Issuer = Globals.Configuration["jwt:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {

                    new Claim("LoginId", user.LoginId),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("RoleId", roleid),
                    new Claim(ClaimTypes.Expiration,
                    expiresAt.ToString())
                }),
                NotBefore = authTime,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityToken token = tokenHandler.CreateToken(tokenDescriptor) as JwtSecurityToken;
            var tokenString = tokenHandler.WriteToken(token);

            var cachekey = "loginToken_" + user.Id;
            JObject jobj = new JObject();
            jobj.Add("tokenId", token.RawSignature);
            jobj.Add("valid", true);
            jobj.Add("expiresAt", expiresAt);
            var cacheToken = jobj.ToString();


            return SuccessData(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = user.Id,
                    name = user.LoginId,
                    roleId = roleid,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });

        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult logout()
        {
            SysFunc sf = SysFunc.getInstance(objectSpace);
            SysUser curruser = sf.getCurrentUser();
            if (curruser == null)
            {
                return SuccessMsg("ok");
            }
            var cachekey = "loginToken_" + curruser.Id;

            return SuccessMsg("ok");

        }

        
       
        
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult Menu()
        {
            SysFunc sf = SysFunc.getInstance(objectSpace);
            SysUser user = sf.getCurrentUser();
            if (user == null)
            {
                return RedirectToAction("logout", "Home", new { });
            }
            return Content( user.GetMyMenu(1).ToString());
        }
      
       
       
        
        
        
    }

}
