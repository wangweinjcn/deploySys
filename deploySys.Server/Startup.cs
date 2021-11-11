using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Ace;
using Chloe.Ext.Intf;
using Chloe.SQLite;
using FrmLib.Extend;
using FrmLib.web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NettyRPC;
using NettyRPC.Core;
using Newtonsoft.Json.Linq;

using StackExchange.Redis;

namespace deploySys.Server
{
    public class nStartup : FrmLib.web.baseStartup
    {
        public ConcurrentUseListDic<byte[]> fileCaches;
        public static Dictionary<string, string> sdkReoteComs { get; set; }
        public nStartup(IWebHostEnvironment env) : base(env)
        {
            swaggerDocTags = new List<OpenApiTag>()
            {
                new OpenApiTag { Name = "deploySys", Description = "部署管理控制台" },


            };
            swaggerXmlList = new List<string>()
            {
                "eurekaServer.xml"
            };
            sdkReoteComs = null;
        }


        public override appConfigure appconfig { get; protected set; }
        protected override IList<string> swaggerXmlList { get; set; }
        protected override string appName { get { return "工行数据报表系统 "; } set => throw new NotImplementedException(); }
        protected override string deverName { get { return "wangwei"; } set {; } }

        protected override string otherSwaggerHeaderDoc { get; set; }

        public override void initOtherServices(IServiceCollection services)
        {

            services.AddScoped<IContextSpace>((serviceProvider) =>
            {

                var str = Configuration["db:ConnString"];
                return new Chloe.ObjectSpace.SqlliteContextSpace(new SQLiteConnectionFactory(str));
            });
            FrmLib.Log.commLoger.runLoger.Info(string.Format("rpc server started at {0}",RunConfig.Instance.rpcPort));
            RpcServer rs = new RpcServer(RunConfig.Instance.rpcBackLength,RunConfig.Instance.rpcPort,RunConfig.Instance.timeOut,new  mpSerializer());
            rs.start();
           

        }
        public override void initOtherConfig(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
          
        }
    }
}
