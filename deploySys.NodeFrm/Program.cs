using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Microsoft.Extensions.Logging.Console;
namespace deploySys.Node
{
    class Program
    {
        static IList<string> allassembles = new List<string>();
        public static byte[] getLibBytes(string fullname)
        {
            byte[] content = null;
            Console.WriteLine(fullname);
            if (File.Exists(fullname))
                content = File.ReadAllBytes(fullname);
            if (!allassembles.Contains(fullname))
            {
                allassembles.Add(fullname);
                StringBuilder sb = new StringBuilder();
                foreach (var obj in allassembles)
                    sb.AppendLine(obj);
                File.WriteAllText("asms.txt", sb.ToString());
            }
            return content;
        }
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (context, assembly) =>
            {

                Console.WriteLine("now resolving");
                var content = getLibBytes(assembly.Name);
                return Assembly.Load(content);
            };
            

            string logrepositoryname = Assembly.GetEntryAssembly().GetName().Name;
            var repository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
             typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(AppContext.BaseDirectory, "configs", "log4net.config")));
             
            var os = Environment.OSVersion.Platform;
            if (os==System.PlatformID.Unix)
            {
                var host = new HostBuilder()
                 .ConfigureHostConfiguration(configHost =>
                 {
                     configHost.SetBasePath(Directory.GetCurrentDirectory());

                     configHost.AddCommandLine(args);
                 })
                 .ConfigureAppConfiguration((hostContext, configApp) =>
                 {
                     configApp.SetBasePath(Directory.GetCurrentDirectory());
                     configApp.AddCommandLine(args);
                 })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddHostedService<serviceOnLinux>();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                   // configLogging.AddConsole();
                   
                })
                .Build();
                host.Run();

            }
            else
            {
               if (os==System.PlatformID.Win32NT)
                {
                    Topshelf.HostFactory.Run(x => x.Service<serviceOnWindws>());
                }
            }

        }
    }
}
