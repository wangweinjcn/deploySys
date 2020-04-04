using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace deploySys.Model
{



    public enum EnumUserType
    {
        [Description("系统账号")]
        SysUser = 10,

        [Description("后台管理账号")]
        OssUser = 1,
        [Description("客户账号")]
        CustomerUser = 2

    }
  
   

    public enum EnumSex
    {
        //10:女，1：男；-1未知
        [Description("女")]
        female = 0,
        [Description("男")]
        male = 1,
        [Description("未知")]
        other = -1
    }
    



    public enum EnumAccountRole
    {
        custUsers= 0,      
        admins=100,
        managers=200,
            ossAdmin=300,
            ossRole1=400,

    }
    public enum EnumDockerInstanceStatus
    {
          [Description("已停止")]
        stop=-1, //停止
            [Description("预分配")]
        prepare=0, //预分配
              [Description("就绪")]
        ready=10, //就绪
                [Description("运行中")]
        running=20, //运行中
        [Description("等待停止")]
        waitForTaskStop = 100,
        [Description("等待重启")]
        waitForTaskRestart = 101,
    }
    public enum EnumReleaseTaskStatus
    {
        [Description("发布处理失败")]
        error=-1,
        [Description("新增待处理")]
        created = 0,
        [Description("已分配")]
        assigned = 10,
        [Description("已审核")]
        audited = 11,

        [Description("已发布")]
        released = 20,
        [Description("已注册")]
        registed = 30,

    }
    public enum EnumAppServiceType
    {
        [Description("webService")]
        webService = 0,
        [Description("webSite")]
        webSite = 1,
        [Description("socketService")]
        socketService = 2,
        [Description("混合的webservice")]
        webServiceSite = 3,

    }
    public enum EnumHostTaskType
    {
        [Description("发布任务")]
        releaseTask = 0,
        [Description("停止docker")]
        stopDockerInstance = 10,
        [Description("重启docker实例")]
        restartDockerInstance = 15,
        [Description("其他")]
        other = 20,
    }
    public enum EnumHostTaskStatus
    {
        [Description("待处理")]
        waitForProcess = 0,
        [Description("失败")]
        failed = -1,
        [Description("已开始")]
        started = 10,
        [Description("已完成")]
        finished = 20,
    }
   }
