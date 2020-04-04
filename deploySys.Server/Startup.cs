using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using deploySys.Server.lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Chloe.Ext.Intf;
using NettyRPC;
using NettyRPC.Core;

namespace deploySys.Server
{
    public class nStartup : FrmLib.web.baseStartup
    {
        public ConcurrentUseListDic<byte[]> fileCaches;
        public static Dictionary<string, string> sdkReoteComs { get; set; }
        public nStartup(IHostingEnvironment env) : base(env)
        {
            swaggerDocTags = new List<Tag>()
            {
                new Tag { Name = "deploySys", Description = "部署管理控制台" },


            };
            swaggerXmlList = new List<string>()
            {
                "eurekaServer.xml"
            };
            sdkReoteComs = null;
        }


        protected override IList<string> swaggerXmlList { get; set; }
        protected override string appName { get { return "msgp"; } set => throw new NotImplementedException(); }
        protected override string deverName { get { return "wangwei"; } set {; } }
        public override void initOtherServices(IServiceCollection services)
        {

            services.AddScoped<IContextSpace>((serviceProvider) =>
            {

                var str = Configuration["db:ConnString"];
                return new Chloe.ObjectSpace.SqlliteContextSpace(new SQLiteConnectionFactory(str));
            });
            FrmLib.Log.commLoger.runLoger.Info(string.Format("rpc server started at {0}",RunConfig.Instance.rpcPort));
            RpcServer rs = new RpcServer(RunConfig.Instance.rpcBackLength,RunConfig.Instance.rpcPort,new mpSerializer());
            rs.start();
            //services.AddMvc().AddXmlSerializerFormatters();
            //  services.AddTimedJob();
        }
        public override void initOtherConfig(IApplicationBuilder app, IHostingEnvironment env)
        {
            //var jarrstr = Globals.Configuration["sdkApi:remoteTokens"];
            //JArray jarr = JArray.Parse(jarrstr);
            //foreach (JObject jobj in jarr)
            //{
            //    var tmpstr = jobj["comKey"].ToString();
            //    if (sdkReoteComs.ContainsKey(tmpstr))
            //        continue;
            //    sdkReoteComs.Add(tmpstr, jobj["secretKey"].ToString());
            //}
            //var str = Globals.Configuration["Task:Enable"];
            //bool enabletask = true;
            //bool.TryParse(str, out enabletask);
            //if (enabletask)
            //    app.UseTimedJob();
          
        }
    }
}
