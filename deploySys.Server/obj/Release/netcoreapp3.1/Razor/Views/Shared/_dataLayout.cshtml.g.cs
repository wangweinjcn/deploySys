#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "093040b5b8c3da368d47a5cd9bab450e7d1d9ad7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__dataLayout), @"mvc.1.0.view", @"/Views/Shared/_dataLayout.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"093040b5b8c3da368d47a5cd9bab450e7d1d9ad7", @"/Views/Shared/_dataLayout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__dataLayout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/jquery.md5.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "093040b5b8c3da368d47a5cd9bab450e7d1d9ad73379", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<style>
    .el-row {
        margin-bottom: 20px !important;
    }

    .v-page-ul {
        margin-top: 20px;
        float: right;
    }

    .pages .v-page-ul {
        margin-top: 20px;
        float: left;
    }
</style>
<div id=""app"">
");
            DefineSection("_Scripts", async() => {
                WriteLiteral("\r\n\r\n        ");
#nullable restore
#line 24 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml"
   Write(Html.Partial("_searchcomponent"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n    ");
            }
            );
            WriteLiteral("    <el-row class=\"container\" style=\"height: 100%\">\r\n        <el-col :span=\"9\">\r\n            ");
#nullable restore
#line 28 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml"
       Write(RenderSection("_search", required: false));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </el-col>\r\n        <el-col :lg=\"9\" :offset=\"3\">\r\n            ");
#nullable restore
#line 31 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml"
       Write(RenderSection("_command", required: false));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </el-col>\r\n    </el-row>\r\n\r\n    <el-row class=\"container\" style=\"height: 100%\">\r\n        ");
#nullable restore
#line 36 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml"
   Write(RenderBody());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </el-row>\r\n</div>\r\n");
#nullable restore
#line 39 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\_dataLayout.cshtml"
Write(RenderSection("vueScript",required:true));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
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