using Docker.DotNet;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace testdockerapi
{
    class Program
    {
        public static void nginxDockerReload(string instanceId)
        {
            Console.WriteLine("nginxDockerReload");
            var created = dockerC.Containers.ExecCreateContainerAsync(instanceId, new ContainerExecCreateParameters()
            {
                AttachStdin = true,
                AttachStdout = true,
                Cmd = new List<string>()
                {
                    "nginx",
                    "-s reload"
                },
                Tty = true,
                AttachStderr = true,

            }).GetAwaiter().GetResult();
            Console.WriteLine("{0}", created.ID);
           dockerC.Containers.StartContainerExecAsync(created.ID).GetAwaiter().GetResult();
            Console.WriteLine("nginxDockerReload ok");
        }

        private static string DockerApiUri()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                return "npipe://./pipe/docker_engine";
            }

            var isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            if (isLinux)
            {
                return "unix:/var/run/docker.sock";
            }

            throw new Exception("Was unable to determine what OS this is running on, does not appear to be Windows or Linux!?");
        }
        static DockerClient dockerC = new DockerClientConfiguration(new Uri(DockerApiUri())).CreateClient();
        public static bool haveImages(string ImageName)
        {
            var images =  dockerC.Images.ListImagesAsync(new ImagesListParameters() { MatchName = ImageName }).GetAwaiter().GetResult();
            foreach (var one in images)
                Console.WriteLine(string.Format("{0},{1}", one.ID,string.Join(";", one.RepoTags)));
            if (images.Count == 0)
                return false;
            else
                return true;
        }
        public static ContainerProcessesResponse getContainerInfo(string instanceId)
        {
            return  dockerC.Containers.ListProcessesAsync(instanceId, new ContainerListProcessesParameters()).GetAwaiter().GetResult();
        }

        static void Main(string[] args)
        {

            //var fullname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "diconfig.json");
            //var str = File.ReadAllText(fullname);

            // CreateContainerParameters dparam = JsonConvert.DeserializeObject<CreateContainerParameters>(str);
            //  var response =  dockerC.Containers.CreateContainerAsync(dparam).GetAwaiter().GetResult();
            // Console.WriteLine(response.ID);
          // Console.WriteLine("have images {0},{1}",args[0], haveImages(args[0]));
            //var x = getContainerInfo(args[1]);
            //Console.WriteLine("id:{0},name:{1}", args[1], string.Join(';', x.Processes));
            nginxDockerReload(args[0]);
        }
    }
}
