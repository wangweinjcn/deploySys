using System;
using System.Collections.Generic;
using System.Text;

namespace deploySys.Common
{
   public class DockerParam
    {
        public string Hostname { get; set; }

        public string Domainname { get; set; }
        public string User { get; set; }
        public bool AttachStdin { get; set; }
          public bool AttachStdout { get; set; }
          public bool AttachStderr { get; set; }
          public bool Tty { get; set; }
          public bool OpenStdin { get; set; }
        public bool StdinOnce { get; set; }

        public string Entrypoint { get; set; }
        public string Image { get; set; }

        public string WorkingDir { get; set; }
        public bool NetworkDisabled { get; set; }
        public string MacAddress { get; set; }
        public string StopSignal { get; set; }
        public int StopTimeout { get; set; }
        public IList<string> Env;
        public IList<string>  Cmd;
        public Dictionary<string, string> Labels;
        public Dictionary<string, object> Volumes;
        public Dictionary<string, object> ExposedPorts;
        public Dictionary<string, object> HostConfig;
        public Dictionary<string, object> NetworkingConfig;


        public DockerParam()
        {
            StopSignal = "SIGTERM";
            StopTimeout = 10;
            Env = new List<string>();
             Cmd = new List<string>();
            Labels = new Dictionary<string, string>();
              Volumes = new Dictionary<string, object>();
              ExposedPorts = new Dictionary<string, object>();
              HostConfig = new Dictionary<string, object>();
              NetworkingConfig = new Dictionary<string, object>();
        }

    }
    
}
