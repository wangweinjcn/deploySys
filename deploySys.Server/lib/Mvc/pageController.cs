using Ace.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace Ace.Web.Mvc
{
#if (NETCORE || NETSTANDARD2_0)
    public abstract class pageController : BaseController
    {
         private const string privateCookieName = "depSCookie";
          private int cookieExpireSeconds = 1800;
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }
        /// <summary>
         /// 设置本地cookie
         /// </summary>
         /// <param name="key">键</param>
         /// <param name="value">值</param>  
         /// <param name="secs">过期时长，单位：秒</param>      
        protected void SetCookies(string key, string value, int secs = 30)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddSeconds(secs)
            });
        }
        AdminSession _session;
        public AdminSession CurrentSession
        {
            get
            {

                if (this._session != null)
                    return this._session;

                if (this.HttpContext.User.Identity.IsAuthenticated == false)

                {

                    return null;
                }
                AdminSession session = AdminSession.Parse(this.HttpContext.User);
                this._session = session;
                return session;
            }
            set
            {
                AdminSession session = value;

                if (session == null)
                {
                    //注销登录
                    this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    this.DeleteCookies(privateCookieName);
                    System.Threading.Thread.CurrentPrincipal = null;
                    return;
                }

                List<Claim> claims = session.ToClaims();
                var expirestr = Globals.Configuration["Cookie:ExpireSeconds"];
                // var expirestr = "3600";
                if (!string.IsNullOrEmpty(expirestr))
                    cookieExpireSeconds = int.Parse(expirestr);
                session.expTime = DateTime.Now.AddSeconds(cookieExpireSeconds);
                //init the identity instances 
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme));
                this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddSeconds(cookieExpireSeconds),
                        IsPersistent = true,
                        AllowRefresh = false
                    });
                var clv = (from x in claims where x.Type == "UserId" select x).FirstOrDefault();
                var userid = (clv == null ? "-1" : clv.Value);
                var jobj = Newtonsoft.Json.Linq.JObject.FromObject(new { uid = userid, sid = session.sessionID });
                SetCookies(privateCookieName, jobj.ToString(), cookieExpireSeconds);
                System.Threading.Thread.CurrentPrincipal = userPrincipal;


                this._session = session;
            }
        }
    }
#endif

}
