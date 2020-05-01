using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Topshelf;

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
       public static void  Main(string[] args)
        {
            AssemblyLoadContext.Default.Resolving += (context, assembly) =>            {

                Console.WriteLine("now resolving");
                var content = getLibBytes(assembly.CodeBase);
                return Assembly.Load(content);
            };


            string logrepositoryname = Assembly.GetEntryAssembly().GetName().Name;
           var repository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
            typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(AppContext.BaseDirectory, "configs", "log4net.config")));
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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
                    configLogging.AddConsole();
                    
                })
                .Build();
                 host.Run();

            }
            else {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Topshelf.HostFactory.Run(x => x.Service<serviceOnWindws>());
                }
            }
            
        }
    }
}
