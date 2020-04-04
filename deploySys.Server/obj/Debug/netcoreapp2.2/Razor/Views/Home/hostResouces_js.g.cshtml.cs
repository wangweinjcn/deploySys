#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\hostResouces_js.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "de37f16ddea14cbcf214b65172225a6a60ba7837"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_hostResouces_js), @"mvc.1.0.view", @"/Views/Home/hostResouces_js.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/hostResouces_js.cshtml", typeof(AspNetCore.Views_Home_hostResouces_js))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de37f16ddea14cbcf214b65172225a6a60ba7837", @"/Views/Home/hostResouces_js.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_hostResouces_js : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 4950, true);
            WriteLiteral(@"


<el-dialog title=""主机资源"" :visible=""mainDialog.IsShow""
           v-on:close='mainDialog.Close' v-on:open=""""
           v-loading=""mainDialog.loading"" width=""800px"">
    <el-form :model=""mainDialog.EditModel"">


        <el-row :gutter=""10"">
            <el-col :span=""24"">
                <el-form-item label=""集群："" :rules=""[ { required: true, message: '区域不能为空'}  ]"">
                    <el-select v-model=""mainDialog.EditModel.Ass_Zone_Id"" placeholder=""请选择"">
                        <el-option v-for=""item in categorys""
                                   :key=""item.Id""
                                   :label=""item.Name""
                                   :value=""item.Id"">
                        </el-option>
                    </el-select>
                </el-form-item>
            </el-col>
   <el-col :span=""12"">
                <el-form-item label=""Mac地址:"">
                    <el-input v-model=""mainDialog.EditModel.macId"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-");
            WriteLiteral(@"input>
                </el-form-item>
            </el-col>
        </el-row>
 <el-row :gutter=""10"">
            <el-col :span=""12"">
                <el-form-item label=""主机名称:"">
                    <el-input v-model=""mainDialog.EditModel.hostName"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>
            <el-col :span=""12"">
                <el-form-item label=""IP地址:"">
                    <el-input v-model=""mainDialog.EditModel.IP"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>


        </el-row>
        <el-row :gutter=""10"">
            <el-col :span=""12"">
                <el-form-item label=""可分配起始端口值:"">
                    <el-input v-model=""mainDialog.EditModel.allocFromPort"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>
            <el-col :span=""12"">
     ");
            WriteLiteral(@"           <el-form-item label=""可分配端口最大值:"">
                    <el-input v-model=""mainDialog.EditModel.allocEndPort"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>


        </el-row>
       <el-row :gutter=""10"">
            <el-col :span=""14"">
                <el-form-item label=""根目录:"">
                    <el-input v-model=""mainDialog.EditModel.appBaseDir"" :disabled=""false"" placeholder=""请输入"" style=""width:350px""></el-input>
                </el-form-item>
            </el-col>
           <el-col :span=""10"">
                <el-form-item label=""端口:"">
                    <el-input v-model=""mainDialog.EditModel.nodePort"" :disabled=""false"" placeholder=""请输入"" style=""width:100px""></el-input>
                </el-form-item>
            </el-col>


        </el-row>
        <el-row>
            <el-col :span=""8"">
                <el-form-item label=""是否安装nginx："" >
                     <el-checkbox v-model=""mainDialog.Ed");
            WriteLiteral(@"itModel.haveNginx"">已安装</el-checkbox>
                    </el-form-item>

            </el-col>
             <el-col :span=""16"">
                <el-form-item label=""nginx配置目录:"">
                    <el-input v-model=""mainDialog.EditModel.nginxConfPath"" :disabled=""false"" placeholder=""请输入"" style=""width:350px""></el-input>
                </el-form-item>
            </el-col>
        </el-row>
         <el-row :gutter=""10"">
               <el-col :span=""12"">
                <el-form-item label=""cpu:"">
                    <el-input v-model=""mainDialog.EditModel.cpuName"" :disabled=""true"" placeholder="""" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>
            <el-col :span=""12"">
                
                <el-form-item label=""cpu数量:"">
                    <el-input v-model=""mainDialog.EditModel.cpuCoreNumber"" :disabled=""true"" placeholder="""" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>

        </el-row>
  ");
            WriteLiteral(@"       <el-row :gutter=""10"">
            <el-col :span=""12"">
                <el-form-item label=""mac地址:"">
                    <el-input v-model=""mainDialog.EditModel.macId"" :disabled=""true"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>
            <el-col :span=""12"">
                <el-form-item label=""当前会话Id:"">
                    <el-input v-model=""mainDialog.EditModel.clientSessionId"" :disabled=""true"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>


        </el-row>
       
    </el-form>
    <div slot=""footer"" class=""dialog-footer"">
        <el-button v-on:click=""mainDialog.Close"">取 消</el-button>
        <el-button type=""primary"" v-on:click=""mainDialog.Save"">确 定</el-button>
    </div>
</el-dialog>


");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
