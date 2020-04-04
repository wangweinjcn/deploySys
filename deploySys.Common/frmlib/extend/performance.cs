using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace FrmLib.Extend
{

    public class cpuMetrics
    {
        public double loadPer { get { return us + sy; } }
        [JsonIgnore]
        public double us; //用户空间占用
        [JsonIgnore]
        public double sy;  //系统空间占用
        public double proces;//当前进程占用
        public string cpuname; //cpu名称
        public int cpuCoreNumber;//核心个数

    }

    public class MemoryMetrics
    {
        public double usePercent  { get { return (Used / Total)*100; } }
        public double Total;
        public double Used;
        public double Free;
    }
    public class osMetrics
    {
     public   MemoryMetrics mem;
     public   cpuMetrics cpu;
        public string osName;
        public DateTime reportdt = DateTime.Now;

     public double loadaverage { get { return (mem.usePercent + cpu.loadPer * 0.01) / 2.0; } }
        public static osMetrics getvalue()
        {
            osMetricsClient osmc = new osMetricsClient();
            return osmc.GetMetrics();
        }
        
    }
    public class osMetricsClient
    {

        public osMetrics GetMetrics()
        {
            osMetrics osm = new osMetrics();
            osm.osName = RuntimeInformation.OSDescription;
            if (IsUnix())
            {
                osm.mem = GetUnixMemMetrics();
                osm.cpu = getUnixCpuMetrics();
            }
            else
            {
              osm.mem=   GetWindowsMemMetrics();
              osm.cpu = getWindowsCpuMetrics();
            }
            return osm;
        }

        private bool IsUnix()
        {
            var isUnix = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                         RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

            return isUnix;
        }
        private string GetWmicOutput(string query, bool redirectStandardOutput = true)
        {
            var info = new ProcessStartInfo("wmic");
            info.Arguments = query;
            info.RedirectStandardOutput = redirectStandardOutput;
            var output = "";
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }
            return output.Trim();
        }
        private cpuMetrics getWindowsCpuMetrics()
        {
            //var memorielines = GetWmicOutput("OS get FreePhysicalMemory,TotalVisibleMemorySize /Value").Split("\n".ToCharArray());

            //var freeMemory = memorielines[0].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
            //var totalMemory = memorielines[1].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];


            var cpuLines = GetWmicOutput(" cpu get  Name,LoadPercentage,NumberOfLogicalProcessors   /Value").Split("\n".ToCharArray());


            var CpuUse = cpuLines[0].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
            var CpuName = cpuLines[1].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
            var cpuCorenum= cpuLines[2].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
            cpuMetrics cm = new cpuMetrics();
            cm.cpuname = CpuName;
            cm.us = double.Parse(CpuUse);
            cm.cpuCoreNumber = int.Parse(cpuCorenum);
            return cm;

        }
        private cpuMetrics getUnixCpuMetrics()
        {
            cpuMetrics cm = new cpuMetrics();
            var output = "";
            var info = new ProcessStartInfo("top");
            info.FileName = "/bin/bash";
            //系统内核占比
            info.Arguments ="-c \"top -b -n 1 | grep Cpu | awk '{print $4}' \" ";

            info.RedirectStandardOutput = true;
           
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
            
            cm.sy = double.Parse(output);

            output = "";
            info.FileName = "/bin/bash";
            //用户空间占比
            info.Arguments ="-c  \"top -b -n 1 | grep Cpu | awk '{print $2}' \"";

            info.RedirectStandardOutput = true;
            
            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
             
            cm.us = double.Parse(output);

            info.FileName = "/bin/bash";
            //cpu名称
            info.Arguments ="-c \"cat /proc/cpuinfo | grep 'model name' |uniq\"";

            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
          
            cm.cpuname = output.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
            info.FileName = "/bin/bash";
            //cpu核心
            info.Arguments ="-c  \"grep 'core id' /proc/cpuinfo |wc -l\" ";

            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }
           
             cm.cpuCoreNumber =int.Parse( output.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]);
            return cm;
        }
        private MemoryMetrics GetWindowsMemMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo();
            info.FileName = "wmic";
            info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
            }

            var lines = output.Trim().Split("\n".ToCharArray());
            var freeMemoryParts = lines[0].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var totalMemoryParts = lines[1].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics();
            metrics.Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
            metrics.Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0);
            metrics.Used = metrics.Total - metrics.Free;

            return metrics;
        }

        private MemoryMetrics GetUnixMemMetrics()
        {
            var output = "";

            var info = new ProcessStartInfo("free ");
            info.FileName = "/bin/bash";
            info.Arguments = "-c \"free -m\"";
            info.RedirectStandardOutput = true;

            using (var process = Process.Start(info))
            {
                output = process.StandardOutput.ReadToEnd();
                Console.WriteLine(output);
            }

            var lines = output.Split("\n".ToCharArray());
            var memory = lines[1].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var metrics = new MemoryMetrics();
            metrics.Total = double.Parse(memory[1]);
            metrics.Used = double.Parse(memory[2]);
            metrics.Free = double.Parse(memory[3]);

            return metrics;
        }
    }
}
