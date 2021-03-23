using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace deploySys.Node
{



        public class serviceOnLinux : IHostedService
        {
        private string cmdForInstanceId = "cat /proc/self/cgroup |grep cpuset |cut -d/ -f3|cut -c1-12";
            IApplicationLifetime appLifetime;
            ILogger<serviceOnLinux> logger;
            IHostingEnvironment environment;
            IConfiguration configuration;
        nodeService ns;
            public serviceOnLinux(
                IConfiguration configuration,
                IHostingEnvironment environment,
                ILogger<serviceOnLinux> logger,
                IApplicationLifetime appLifetime)
            {
                this.configuration = configuration;
                this.logger = logger;
                this.appLifetime = appLifetime;
                this.environment = environment;
            Console.WriteLine("excutScriptOnLinux: "+cmdForInstanceId);
            var tmp = tools.excutScriptOnLinux(cmdForInstanceId).Replace(AppDomain.CurrentDomain.BaseDirectory.TrimEnd(System.IO.Path.DirectorySeparatorChar) ,"").Trim(System.Environment.NewLine.ToCharArray()).Trim(' ');
            Console.WriteLine("now get instance id: " + tmp);

            ns = new nodeService(tmp);
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                this.logger.LogInformation("StartAsync method called.");

                this.appLifetime.ApplicationStarted.Register(OnStarted);
                this.appLifetime.ApplicationStopping.Register(OnStopping);
                this.appLifetime.ApplicationStopped.Register(OnStopped);

                return Task.CompletedTask;

            }

            private void OnStarted()
            {
            ns.startService();
                this.logger.LogInformation("OnStarted method called.");

                // Post-startup code goes here  
            }

            private void OnStopping()
            {
            ns.stopService();
                this.logger.LogInformation("OnStopping method called.");

                // On-stopping code goes here  
            }

            private void OnStopped()
            {
                this.logger.LogInformation("OnStopped method called.");

                // Post-stopped code goes here  
            }


            public Task StopAsync(CancellationToken cancellationToken)
            {
                this.logger.LogInformation("StopAsync method called.");

                return Task.CompletedTask;
            }
        }
    

}
