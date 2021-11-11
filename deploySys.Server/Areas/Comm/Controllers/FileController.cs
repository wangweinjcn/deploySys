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
using Application.Model.Base;
using System.DrawingCore.Imaging;
using System.DrawingCore;
using Chloe.Ext;
using Swashbuckle.AspNetCore.Annotations;


using Microsoft.Net.Http.Headers;
using Ace.Web.Mvc;
using SkiaSharp;

namespace deploySys.Server.Controllers.Comm
{
    [Area(AreaNames.Comm)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[Area]/[controller]/[action]")]

  public class FileController : CsApiHttpController
    {
        const string key = "message";
        const string message = "hello";
        const int maxCacheFileLength = 5242880;
        
        
        public FileController(IDistributedCache distributedCache) : base(distributedCache)
        {
          


        }
   
        /// <summary>
        /// 删除文件或目录
        /// </summary>
        /// <param name="Id"></param>

        /// <returns></returns>
        [HttpPost]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult deleteOne([FromRoute]string Id)
        {
            var fvd = objectSpace.ObjectForId<FileVDir>(Id);
            if (fvd.isFile)
            {
                var pf = fvd.Ass_PhysicalPath;

                var fullpath =Path.Combine(Globals.Configuration["rootDir"], pf.getFullPath(),fvd.Guid);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }

              
                fvd.choDelete();
                
            }
            else
            {
                var ccount = objectSpace.SpaceQuery<FileVDir>().Where(a => a.Ass_FileVDir_parent == fvd).Count();
                if (ccount > 0)
                    return FailedMsg("目录下有文件或子目录不能删除");
                fvd.choDelete();
            }
            UpdateDatabase();
            return SuccessMsg();

        }
       
        /// <summary>
        /// 添加或更新文件信息,新文件必须要有部门、归属目录；根目录不允许有文件
        ///最大100M
        /// </summary>

        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(1000000000)]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult auOne([FromForm]IList<IFormFile> file)
        {
            if ((file == null || file.Count == 0))
                return FailedMsg(401, "文件没有内容");
            SysFunc sf = SysFunc.getInstance(objectSpace);
            var fullfn = Path.GetFileName(file[0].FileName);

            var fn = System.IO.Path.GetFileName(fullfn);
            FileVDir o = null;
            using (MemoryStream fs = new MemoryStream())
            {
                file[0].CopyTo(fs);

                byte[] content = FrmLib.Extend.tools_static.StreamToBytes(fs);
                o = sf.AddNewFile(content, fn);
                UpdateDatabase();
            }
           
            
            return SuccessData(o);
        }
        /// <summary>
        /// 根据请求信息赋予封装的文件信息类
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entityLength"></param>
        /// <returns></returns>
        private DownFileInfo GetFileInfoFromRequest(HttpRequest request, long entityLength)
        {
            var dfileinfo = new DownFileInfo
            {
                From = 0,
                To = entityLength - 1,
                IsPartial = false,
                Length = entityLength
            };

            var requestHeaders = request.GetTypedHeaders();
           
            if (requestHeaders.Range != null && requestHeaders.Range.Ranges.Count > 0)
            {
                FrmLib.Log.commLoger.devLoger.Debug("requestHeaders.Range");
  
                  var range = requestHeaders.Range.Ranges.FirstOrDefault();
                if (range.From.HasValue && range.From < 0 || range.To.HasValue && range.To > entityLength - 1)
                {
                    return null;
                }

                var start = range.From;
                var end = range.To;
                FrmLib.Log.commLoger.devLoger.Debug("rang,"+JObject.FromObject(range));
                if (start.HasValue)
                {
                    FrmLib.Log.commLoger.devLoger.Debug("requestHeaders.Range start,");
                    if (start.Value >= entityLength)
                    {
                        return null;
                    }
                    if (!end.HasValue || end.Value >= entityLength)
                    {
                        end = entityLength - 1;
                    }
                }
                else
                {
                    FrmLib.Log.commLoger.devLoger.Debug("requestHeaders.Range start has no value,");
                    if (end.Value == 0)
                    {
                        return null;
                    }

                    var bytes = Math.Min(end.Value, entityLength);
                    start = entityLength - bytes;
                    end = start + bytes - 1;
                }
                dfileinfo.From = start.Value;
                dfileinfo.To = end.Value;
                dfileinfo.IsPartial = true;
                dfileinfo.Length = end.Value - start.Value + 1;
            }
            
            FrmLib.Log.commLoger.devLoger.Debug("debug file downd:" +JObject.FromObject(dfileinfo));
            return dfileinfo;
        }

        /// <summary>
        /// 设置响应头信息
        /// </summary>
        /// <param name="response"></param>
        /// <param name="fileInfo"></param>
        /// <param name="fileLength"></param>
        /// <param name="fileName"></param>
        private void    SetResponseHeaders(HttpResponse response, DownFileInfo fileInfo,
                                      long fileLength, string fileName,string MimeType)
        {
            response.Headers[HeaderNames.AcceptRanges] = "bytes";
            response.StatusCode = fileInfo.IsPartial ? StatusCodes.Status206PartialContent
                                      : StatusCodes.Status200OK;

            var contentDisposition = new Microsoft.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            contentDisposition.SetHttpFileName(fileName);
            response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            response.Headers[HeaderNames.ContentType] = MimeType;
            response.Headers[HeaderNames.ContentLength] = fileInfo.Length.ToString();
            if (fileInfo.IsPartial)
            {
                response.Headers[HeaderNames.ContentRange] = new Microsoft.Net.Http.Headers.ContentRangeHeaderValue(fileInfo.From, fileInfo.To, fileLength).ToString();
            }
        }
        /// <summary>
        /// 断点下载
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        private  async Task<long> RangeDownload( BaseController controller, string fullpath,string filename="")
        {
            long size, start, end, length, fp = 0;
             string strContentType = "application/octet-stream";
           // var mstream = new MemoryStream(content);
            using (StreamReader reader = new StreamReader(System.IO.File.OpenRead(fullpath) ))
            {

                size = reader.BaseStream.Length;
                start = 0;
                end = size - 1;
                length = size;
                // Now that we've gotten so far without errors we send the accept range header
                /* At the moment we only support single ranges.
                 * Multiple ranges requires some more work to ensure it works correctly
                 * and comply with the spesifications: http://www.w3.org/Protocols/rfc2616/rfc2616-sec19.html#sec19.2
                 *
                 * Multirange support annouces itself with:
                 * header('Accept-Ranges: bytes');
                 *
                 * Multirange content must be sent with multipart/byteranges mediatype,
                 * (mediatype = mimetype)
                 * as well as a boundry header to indicate the various chunks of data.
                 */
                controller.Response.Headers.Add("Accept-Ranges", "0-" + size);
               
                if (!string.IsNullOrEmpty(filename))
                {
                    var extname = System.IO.Path.GetExtension(filename).ToLower();
                    //if (extname.Contains("mp3"))
                    //    strContentType = "audio/m4a";
                    //else
                    strContentType = FrmLib.Http.FileContentType.GetMimeType(extname);
                }
                // header('Accept-Ranges: bytes');
                // multipart/byteranges
                // http://www.w3.org/Protocols/rfc2616/rfc2616-sec19.html#sec19.2

                if (!String.IsNullOrEmpty(controller.Request.Headers["HTTP_RANGE"]))
                {
                    long anotherStart = start;
                    long anotherEnd = end;
                    string[] arr_split = controller.Request.Headers["HTTP_RANGE"].FirstOrDefault().Split(new char[] { Convert.ToChar("=") });
                    string range = arr_split[1];

                    // Make sure the client hasn't sent us a multibyte range
                    if (range.IndexOf(",") > -1)
                    {
                        // (?) Shoud this be issued here, or should the first
                        // range be used? Or should the header be ignored and
                        // we output the whole content?
                        controller.Response.Headers.Add("Content-Range", "bytes " + start + "-" + end + "/" + size);
                        controller.Response.StatusCode = 416;
                        controller.Response.StatusCode = 416;
                        await controller.Response.WriteAsync("Requested Range Not Satisfiable");
                    }

                    // If the range starts with an '-' we start from the beginning
                    // If not, we forward the file pointer
                    // And make sure to get the end byte if spesified
                    if (range.StartsWith("-"))
                    {
                        // The n-number of the last bytes is requested
                        anotherStart = size - Convert.ToInt64(range.Substring(1));
                    }
                    else
                    {
                        arr_split = range.Split(new char[] { Convert.ToChar("-") });
                        anotherStart = Convert.ToInt64(arr_split[0]);
                        long temp = 0;
                        anotherEnd = (arr_split.Length > 1 && Int64.TryParse(arr_split[1].ToString(), out temp)) ? Convert.ToInt64(arr_split[1]) : size;
                    }
                    /* Check the range and make sure it's treated according to the specs.
                     * http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html
                     */
                    // End bytes can not be larger than $end.
                    anotherEnd = (anotherEnd > end) ? end : anotherEnd;
                    // Validate the requested range and return an error if it's not correct.
                    if (anotherStart > anotherEnd || anotherStart > size - 1 || anotherEnd >= size)
                    {

                        controller.Response.Headers.Add("Content-Range", "bytes " + start + "-" + end + "/" + size);
                        controller.Response.StatusCode = 416;
                        await controller.Response.WriteAsync("Requested Range Not Satisfiable");
                    }
                    start = anotherStart;
                    end = anotherEnd;

                    length = end - start + 1; // Calculate new content length
                    fp = reader.BaseStream.Seek(start, SeekOrigin.Begin);
                    controller.Response.StatusCode = 206;
                }
            }
            // Notify the client the byte range we'll be outputting
            controller.Response.Headers.Add("Content-Range", "bytes " + start + "-" + end + "/" + size);
            controller.Response.Headers.Add("Content-Length", length.ToString());
            controller.Response.Headers.Add("Content-Type", strContentType);
            // Start buffered download
            await controller.Response.SendFileAsync(fullpath, fp, length);
            return fp;
        }
        /// <summary>
        /// 获取指定文件
        /// </summary>
        /// <param name="Id">文件的Guid</param>
        /// <returns></returns>
        ///
        [HttpGet]
        [AllowAnonymous]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public async Task< IActionResult> getFile([FromRoute]string Id)
        {


            byte[] content;
             dynamic props = new System.Dynamic.ExpandoObject();
            string cachekey = string.Format("{0}-{1}","File", Id);
            bool regetObject = true;
            var ns = nStartup.instance as nStartup;
            if (ns.fileCaches.ContainsKey(cachekey))
            {
                content = ns.fileCaches.getValue(cachekey);
                props = ns.fileCaches.getProps(cachekey);
                if (content!=null && content.Length > 0 && props != null)
                    return File(content, props.strContentType, props.Filename);
                else
                {
                    ns.fileCaches.removeOne(cachekey);
                    regetObject = true;
                }
            }
           if(regetObject)
            {
                var vfd = objectSpace.SpaceQuery<FileVDir>().Where(a => a.Guid == Id || a.Id.ToString() == Id).FirstOrDefault();
                if (vfd == null || !vfd.isFile)
                    return FailedMsg("没有这个文件");
                var pp = vfd.Ass_PhysicalPath;
                if (pp == null)
                    return FailedMsg("找不到文件物理路径");
                var fullpath = Path.Combine(Globals.Configuration["rootDir"], pp.getFullPath(), vfd.Guid);
                if (!System.IO.File.Exists(fullpath))
                    return FailedMsg("文件不存在");
                FileInfo fi = new FileInfo(fullpath);
                 props.Filename = vfd.Name;
                var extname = System.IO.Path.GetExtension(vfd.Name);
                props.strContentType = FrmLib.Http.FileContentType.GetMimeType(extname);
                if (fi.Length <= maxCacheFileLength)
                {
                    content = System.IO.File.ReadAllBytes(fullpath);
                    ns.fileCaches.addOne(cachekey, content,props);
                    return File(content, props.strContentType,props.Filename);
                }
               else
                await this.RangeDownload(this,  fullpath, props.Filename);
                // 
            }
            
            return SuccessMsg();
        }
        /// <summary>
        /// 获取指定文件
        /// </summary>
        /// <param name="Id">文件的Guid</param>
        /// <returns></returns>
        ///
        [HttpGet]
        [AllowAnonymous]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult getFileOld([FromRoute]string Id)
        {
            var vfd = objectSpace.SpaceQuery<FileVDir>().Where(a => a.Guid == Id || a.Id.ToString() == Id).FirstOrDefault();
            if (vfd == null || !vfd.isFile)
                return FailedMsg("没有这个文件");
            var pp = vfd.Ass_PhysicalPath;
            var fullpath = Path.Combine(Globals.Configuration["rootDir"], pp.getFullPath(), vfd.Guid);
            if (!System.IO.File.Exists(fullpath))
                return FailedMsg("文件不存在");

            var extname = System.IO.Path.GetExtension(vfd.Name);
            FrmLib.Log.commLoger.devLoger.Debug("GetMimeType");
            var strContentType = FrmLib.Http.FileContentType.GetMimeType(extname);
            //获取下载文件长度
            var fileLength = new FileInfo(fullpath).Length;
            //初始化下载文件信息
            var fileInfo = GetFileInfoFromRequest(this.Request, fileLength);
            FrmLib.Log.commLoger.devLoger.Debug("PartialContentFileStream");
            //获取剩余部分文件流
            var stream = new PartialContentFileStream(new FileStream(fullpath, FileMode.Open),
                                                 fileInfo.From, fileInfo.To);
            FrmLib.Log.commLoger.devLoger.Debug("SetResponseHeaders");
            //设置响应 请求头
            SetResponseHeaders(this.Response, fileInfo, fileLength, fullpath, strContentType);
            FrmLib.Log.commLoger.devLoger.Debug("FileStreamResult");
            return new FileStreamResult(stream, strContentType);

            //byte[] content = System.IO.File.ReadAllBytes(fullpath);

            //return File(content, strContentType,vfd.Name);
        }
        /// <summary>
        /// 按指定尺寸获取图片
        /// </summary>
        /// <param name="Id">图片Id</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{id?}")]
        [SwaggerOperation(Tags = new[] { "Common" })]
        public IActionResult getImage([FromRoute]string Id ,[FromQuery]int width=0,[FromQuery]int height=0)
        {
            byte[] content;
            dynamic props = new System.Dynamic.ExpandoObject();
            props.strContentType = "image/jpeg";
            props.filename = string.Format("{0}.jpg", Id);


            string cachekey = string.Format("{0}-{1}-{2}", Id, width, height);
            var ns = nStartup.instance as nStartup;
            if (ns.fileCaches.ContainsKey(cachekey))
            {
                content = ns.fileCaches.getValue(cachekey);
                props = ns.fileCaches.getProps(cachekey);
            }
            else
            {
                var vfd = objectSpace.SpaceQuery<FileVDir>().Where(a => a.Guid == Id || a.Id.ToString() == Id).FirstOrDefault();
                if (vfd == null || !vfd.isFile)
                    return FailedMsg("没有这个文件");
                var pp = vfd.Ass_PhysicalPath;
                if (pp == null)
                    return FailedMsg("文件路径错误");
                var fullpath = Path.Combine(Globals.Configuration["rootDir"], pp.getFullPath(), vfd.Guid);
                if (!System.IO.File.Exists(fullpath))
                    return FailedMsg("没有这个文件");

                var extname = System.IO.Path.GetExtension(vfd.Name);
                props.strContentType = FrmLib.Http.FileContentType.GetMimeType(extname);
                if (!props.strContentType.ToLower().Contains("image"))
                    return FailedMsg("不是图片文件");
                byte[] content1 = System.IO.File.ReadAllBytes(fullpath);
                if (content1 == null || content1.Length == 0)
                    return FailedMsg("空文件");
                content = imgResize(content1, width, height);
                if (content == null)
                {
                    content = content1;
                 //   FrmLib.Log.commLoger.runLoger.Error("错误的图片格式,"+Id);
                 //   return FailedMsg("不是有效的图片");
                }
                props.filename = vfd.Name;
                ns.fileCaches.addOne(cachekey, content,props);
            }
            return File(content, props.strContentType, props.filename);
        }


        private byte[] imgResize(byte[] input,  int width, int height)
        {
            const int quality = 75; //质量为75%
            var ms = new MemoryStream(input);
            
            using (var inputStream = new SKManagedStream(ms))
            using (var original = SKBitmap.Decode(inputStream))
            {
                var output = new MemoryStream();
                //  SKCodec codec = SKCodec.Create(inputStream);
                if (original == null)
                {
                    FrmLib.Log.commLoger.runLoger.Error("SKBitmap.Decode is error;");
                    return null;
                }
                SKImageInfo info = original.Info;
                
                // get the scale that is nearest to what we want (eg: jpg returned 512)
                //SKSizeI supportedScale = codec.GetScaledDimensions((float)width / info.Width);
                float scale = 1;
                if (width >  0 && height > 0)
                {
                    var scale1 = (float)width / info.Width;
                    var scale2 = (float)height / info.Height;
                    scale = Math.Min(scale1, scale2);
                }
                // decode the bitmap at the nearest size
                //SKImageInfo nearest = new SKImageInfo(supportedScale.Width, supportedScale.Height);
                //SKBitmap bmp = SKBitmap.Decode(codec, nearest);

                // now scale that to the size that we want
                //float realScale = (float)info.Height / info.Width;
                //SKImageInfo desired = new SKImageInfo(desiredWidth, (int)(realScale * desiredWidth);
                //bmp = bmp.Resize(desired), SKBitmapResizeMethod.Lanczos3);

                using (var resized = original.Resize(new SKImageInfo((int)(info.Width*scale), (int)(info.Height*scale)), SKBitmapResizeMethod.Lanczos3))
                {
                    if (resized == null)
                    {
                        string str = string.Format("scale:{0},width:{1},height:{2}",scale,info.Width,info.Height);
                        FrmLib.Log.commLoger.runLoger.Error(" original.Resize is error; "+str);
                        return null;
                    }
                    using (var image = SKImage.FromBitmap(resized))
                    {
                        if (image == null)
                        {
                             FrmLib.Log.commLoger.runLoger.Error("SKImage.FromBitmap is error;");
                            return null;
                        }
                            image.Encode(SKEncodedImageFormat.Jpeg, quality)
                                .SaveTo(output);
                        
                    }
                }
                return FrmLib.Extend.tools_static.StreamToBytes(output);
            }
        }
    }
    public class DownFileInfo
    {
        public long From = 0;
        public long To = 0;
        public bool IsPartial = false;
        public long Length = 0;
    }
    public class PartialContentFileStream : Stream
    {
        private readonly long _start;
        private readonly long _end;
        private long _position;
        private FileStream _fileStream;

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PartialContentFileStream(FileStream fileStream, long start, long end)
        {
            _start = start;
            _position = start;
            _end = end;
            _fileStream = fileStream;

            if (start > 0)
            {
                _fileStream.Seek(start, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// 将缓冲区数据写到文件
        /// </summary>
        public override void Flush()
        {
            _fileStream.Flush();
        }

        /// <summary>
        /// 设置当前下载位置
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
            {
                _position = _start + offset;
                return _fileStream.Seek(_start + offset, origin);
            }
            else if (origin == SeekOrigin.Current)
            {
                _position += offset;
                return _fileStream.Seek(_position + offset, origin);
            }
            else
            {
                throw new NotImplementedException("SeekOrigin.End未实现");
            }
        }

        /// <summary>
        /// 依据偏离位置读取
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int byteCountToRead = count;
            if (_position + count > _end)
            {
                byteCountToRead = (int)(_end - _position) + 1;
            }
            var result = _fileStream.Read(buffer, offset, byteCountToRead);
            _position += byteCountToRead;
            return result;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            int byteCountToRead = count;
            if (_position + count > _end)
            {
                byteCountToRead = (int)(_end - _position);
            }
            var result = _fileStream.BeginRead(buffer, offset,
                                               count, (s) =>
                                               {
                                                   _position += byteCountToRead;
                                                   callback(s);
                                               }, state);
            return result;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
