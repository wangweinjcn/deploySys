#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e80b4bf89378078bf49cf026a2f8cba6266602fb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_subMenuPartial), @"mvc.1.0.view", @"/Views/Shared/subMenuPartial.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/subMenuPartial.cshtml", typeof(AspNetCore.Views_Shared_subMenuPartial))]
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
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e80b4bf89378078bf49cf026a2f8cba6266602fb", @"/Views/Shared/subMenuPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_subMenuPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<JArray>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 3 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
  
    if (Model.Count > 0)
    {
        foreach (var obj in Model)
        {

            if (obj["IsMenu"].ToObject<bool>())
            {

#line default
#line hidden
            BeginContext(194, 42, true);
            WriteLiteral("                <dd><a href=\"#\" data-url=\"");
            EndContext();
            BeginContext(237, 28, false);
#line 11 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
                                     Write(obj["UrlAddress"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(265, 14, true);
            WriteLiteral("\" data-title=\"");
            EndContext();
            BeginContext(280, 11, false);
#line 11 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
                                                                                Write(obj["Name"]);

#line default
#line hidden
            EndContext();
            BeginContext(291, 11, true);
            WriteLiteral("\" data-id=\"");
            EndContext();
            BeginContext(303, 9, false);
#line 11 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
                                                                                                       Write(obj["Id"]);

#line default
#line hidden
            EndContext();
            BeginContext(312, 46, true);
            WriteLiteral("\" class=\"site-demo-active\" data-type=\"tabAdd\">");
            EndContext();
            BeginContext(359, 22, false);
#line 11 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
                                                                                                                                                               Write(obj["Name"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(381, 11, true);
            WriteLiteral("</a></dd>\r\n");
            EndContext();
#line 12 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
            }
            else
            {
                

#line default
#line hidden
            BeginContext(458, 91, true);
            WriteLiteral("                <dd>\r\n                    <a href=\"javascript:;\">\r\n                        ");
            EndContext();
            BeginContext(550, 22, false);
#line 18 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
                   Write(obj["Name"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(572, 101, true);
            WriteLiteral("\r\n                    </a>\r\n                    <dl class=\"layui-nav-child\">\r\n                       ");
            EndContext();
            BeginContext(674, 58, false);
#line 21 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"
                  Write(Html.Partial("subMenuPartial",obj["ChildNodes"] as JArray));

#line default
#line hidden
            EndContext();
            BeginContext(732, 52, true);
            WriteLiteral("\r\n                    </dl>\r\n                </dd>\r\n");
            EndContext();
#line 24 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Shared\subMenuPartial.cshtml"

            }
        }
    }

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<JArray> Html { get; private set; }
    }
}
#pragma warning restore 1591
