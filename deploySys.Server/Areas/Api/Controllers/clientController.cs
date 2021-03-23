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
using System.Threading.Tasks;

namespace deploySys.Server.Areas.Api.Controllers
{
    [Area(AreaNames.Api)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[Area]/[controller]/[action]")]
    [AllowAnonymous]
    public class fileController : apiController
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "File" })]

        public async Task<IActionResult> getTaskFile([FromRoute] string Id)
        {
            byte[] content;
            dynamic props = new System.Dynamic.ExpandoObject();
            long fisize = 0;
            FileInfo fi = null;
            var vfd = objectSpace.SpaceQuery<FileVDir>().Where(a => a.Guid == Id || a.Id.ToString() == Id).FirstOrDefault();
            if (vfd == null || !vfd.isFile)
                return FailedMsg("没有这个文件");
            var pp = vfd.Ass_PhysicalPath;
            if (pp == null)
                return FailedMsg("找不到文件物理路径");
            var fullpath = Path.Combine(pp.getFullPath(), vfd.Guid);
            if (!System.IO.File.Exists(fullpath))
                return FailedMsg("文件不存在");
            fi = new FileInfo(fullpath);
            props.Filename = vfd.Name;
            content = System.IO.File.ReadAllBytes(fullpath);
            fisize = fi.Length;
            var extname = System.IO.Path.GetExtension(props.Filename);
            props.strContentType = FrmLib.Http.FileContentType.GetMimeType(extname);


            return File(content, props.strContentType, props.Filename);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "File" })]

        public async Task<IActionResult> getClientUpdateFile([FromRoute] string Id)
        {
            byte[] content;
            dynamic props = new System.Dynamic.ExpandoObject();
            long fisize = 0;
          
            var finfo = RunConfig.Instance.clientFiles.getFileInfoByHashKey(Id);
            if (finfo == null)
                return null;
            props.Filename = finfo.filename;
            content = finfo.filecontent;
            fisize = finfo.filecontent.Length;
            var extname = System.IO.Path.GetExtension(props.Filename);
            props.strContentType = FrmLib.Http.FileContentType.GetMimeType(extname);


            return File(content, props.strContentType, props.Filename);
        }
    }
}
