using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using FrmLib.Http;

using Newtonsoft.Json.Linq;

using System.Net.Http;
using System.Net;

using System.IO;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Xml;
using System.Reflection;
using System.Runtime.InteropServices;

namespace deploySys.Node
{
 public static   class  tools
    {
     public  const string cmdok = "It's Ok";
     public static string getVersion()
     {
         Assembly assembly = Assembly.GetExecutingAssembly();
         
         // 获取程序集元数据   
         AssemblyCopyrightAttribute copyright = (AssemblyCopyrightAttribute)
         AssemblyCopyrightAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
         typeof(AssemblyCopyrightAttribute));
         AssemblyDescriptionAttribute description = (AssemblyDescriptionAttribute)
         AssemblyDescriptionAttribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),
         typeof(AssemblyDescriptionAttribute));
       
         return assembly.GetName().Version.ToString();
     }
        public static void restartSelfOnLinux()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string fileName = Path.GetFileName(assembly.Location);
                string path = Path.GetFullPath(assembly.Location);
                var fn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "restart.sh");
               
                File.WriteAllText(fn, getrstartCmdContent());
                var chmodcmd = "chmod +x " + fn;
                excutScriptOnLinux(chmodcmd);
                var restartcmd = fn + " " + fileName + "  " + path;
                excutScriptOnLinux(restartcmd);

            }

        }
     public static string excutScriptOnLinux(string cmdStr)
     {
         Process p = new Process();
         p.StartInfo.FileName = "sh";
         p.StartInfo.UseShellExecute = false;
         p.StartInfo.RedirectStandardInput = true;
         p.StartInfo.RedirectStandardOutput = true;
        
         p.StartInfo.RedirectStandardError = true;
         p.StartInfo.CreateNoWindow = true;
         p.Start();
         p.StandardInput.WriteLine("pwd");
         p.StandardInput.WriteLine(cmdStr);
         
         p.StandardInput.WriteLine("exit");
         p.WaitForExit();
         return  p.StandardOutput.ReadToEnd();
     }
     private static double getCpuUsage()
     {
         return 0;
     }

         
     
    public static List<string> getNetworkInterfacesMac()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        List<string> res = new  List<string>();
        foreach (NetworkInterface adapter in nics)
        {

            
            Console.Write("  Physical address ........................ : ");
            PhysicalAddress address = adapter.GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();
            string macid = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                // Display the physical address in hexadecimal.
                macid = macid + bytes[i].ToString("X2");
                Console.Write("{0}", bytes[i].ToString("X2"));
                // Insert a hyphen after each byte, unless we are at the end of the
                // address.
                if (i != bytes.Length - 1)
                {
                    Console.Write("-");
                }
                macid = macid + "-";
            }
            if (macid.EndsWith("-"))
              macid=  macid.Remove(macid.Length - 1);
            if(!string.IsNullOrEmpty(macid))
            res.Add(macid);
          
        }
        return res;
    }
    /// <summary>
    /// 更新配置文件信息
    /// </summary>
    /// <param name="name">配置文件字段名称</param>
    /// <param name="Xvalue">值</param>
    public  static void UpdateConfig(string name, string Xvalue)
    {
       

       
    }
        public static string getrstartCmdContent()
        { 
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#!/bin/bash");
                sb.AppendLine("echo $0");
                 sb.AppendLine("NAME=$1  ");
                sb.AppendLine("path=$2");
                sb.AppendLine("ID=`ps -ef | grep \"$NAME\" | grep -v \"grep\" |grep -v \"$0\" | awk '{print $2}'` ");
                sb.AppendLine("if [ -z \"$ID\" ];then");
                sb.AppendLine("    echo \"process id is empty, process is not existed...\"");
                sb.AppendLine("    echo \"process will start...\"");
                sb.AppendLine("    nohup  /usr/bin/dotnet $path/$NAME 2>&1  &");
                sb.AppendLine("    echo \"process has start...\"");
                sb.AppendLine("else");
                sb.AppendLine("    for id in $ID;do   kill -9 $id;   echo \"killed $id\"; done");
                sb.AppendLine("    echo \"process will restart...\"");
                sb.AppendLine("    nohup /usr/bin/dotnet $path/$NAME 2>&1 &");
                sb.AppendLine("    echo \"process has restart...\"");
                sb.AppendLine("fi");


                return sb.ToString();
            }
 }

   
}
