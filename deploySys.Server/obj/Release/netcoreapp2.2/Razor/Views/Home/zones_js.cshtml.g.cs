#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\zones_js.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b45e5cc1c5188e81981ed43ab0f5f886848de6d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_zones_js), @"mvc.1.0.view", @"/Views/Home/zones_js.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/zones_js.cshtml", typeof(AspNetCore.Views_Home_zones_js))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b45e5cc1c5188e81981ed43ab0f5f886848de6d7", @"/Views/Home/zones_js.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_zones_js : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 1710, true);
            WriteLiteral(@"


<el-dialog :close-on-click-modal=false title=""集群信息"" :visible=""mainDialog.IsShow""
           v-on:close='mainDialog.Close' v-on:open=""""
           v-loading=""mainDialog.loading"" width=""800px"">
    <el-form :model=""mainDialog.EditModel"">


        <el-row :gutter=""10"">
            <el-col :span=""24"">
                <el-form-item label=""区域："" :rules=""[ { required: true, message: '区域不能为空'}  ]"">
                    <el-select v-model=""mainDialog.EditModel.Ass_Region_Id"" placeholder=""请选择"">
                        <el-option v-for=""item in categorys""
                                   :key=""item.Id""
                                   :label=""item.Name""
                                   :value=""item.Id"">
                        </el-option>
                    </el-select>
                </el-form-item>
            </el-col>
  
        </el-row>
 <el-row :gutter=""10"">
            <el-col :span=""12"">
                <el-form-item label=""Key:"">
                    <el-input v-model=""mainD");
            WriteLiteral(@"ialog.EditModel.Key"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-input>
                </el-form-item>
            </el-col>
            <el-col :span=""12"">
                <el-form-item label=""集群名称:"">
                    <el-input v-model=""mainDialog.EditModel.Name"" :disabled=""false"" placeholder=""请输入"" style=""width:250px""></el-input>
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
