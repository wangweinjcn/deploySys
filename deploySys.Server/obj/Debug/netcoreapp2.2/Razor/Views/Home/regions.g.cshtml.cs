#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\regions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "84cfbb468c181b4fa114e2a0e608c6ef5b0533e4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_regions), @"mvc.1.0.view", @"/Views/Home/regions.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/regions.cshtml", typeof(AspNetCore.Views_Home_regions))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"84cfbb468c181b4fa114e2a0e608c6ef5b0533e4", @"/Views/Home/regions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_regions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\regions.cshtml"
  
    Layout = "_dataLayout";

#line default
#line hidden
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("_search", async() => {
                BeginContext(56, 107, true);
                WriteLiteral("\r\n    <searchEditor v-bind:SearchModel=\"mainSearchModel\" v-on:dosearch=\"getMainTableData\"></searchEditor>\r\n");
                EndContext();
            }
            );
            DefineSection("_command", async() => {
                BeginContext(184, 394, true);
                WriteLiteral(@"
    <el-button-group>
        <el-button type=""primary"" icon=""el-icon-edit"" v-on:click=""mainEdit"" :disabled=""mainCommandDisnable""></el-button>
        <el-button type=""primary"" icon=""el-icon-circle-plus"" v-on:click=""mainAdd"" ></el-button>
        <el-button type=""primary"" icon=""el-icon-delete"" v-on:click=""mainDelete"" :disabled=""mainCommandDisnable""></el-button>
    </el-button-group>
");
                EndContext();
            }
            );
            BeginContext(581, 1021, true);
            WriteLiteral(@"
<template>
    <div>
        <v-table is-vertical-resize
                 :vertical-resize-offset='60'
                 is-horizontal-resize
                 style=""width:100%""
                 :multiple-sort=""false""
                 :min-height=""550""
                 even-bg-color=""#f2f2f2""
                 :columns=""mainTableConfig.columns""
                 :table-data=""mainTableConfig.tableData""
                 row-hover-color=""#eee""
                 row-click-color=""#edf7ff""
                 :row-click=""mainRowClick""
                 v-on:sort-change=""sortChange""
                 :paging-index=""(mainPageIndex-1)*pageSize""></v-table>

       <div class=""mt20 mb20 bold""></div>

        <div class=""pages"">
            <v-pagination v-on:page-change=""mainPageChange"" v-on:page-size-change=""mainPageSizeChange"" :total=""mainTotalCount"" :page-size=""pageSize"" :layout=""['total', 'prev', 'pager', 'next', 'sizer', 'jumper']"">

            </v-pagination>
        </div>
    <div>
        ");
            EndContext();
            BeginContext(1603, 26, false);
#line 41 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\regions.cshtml"
   Write(Html.Partial("regions_js"));

#line default
#line hidden
            EndContext();
            BeginContext(1629, 31, true);
            WriteLiteral("\r\n    </div>\r\n</template>\r\n\r\n\r\n");
            EndContext();
            DefineSection("vueScript", async() => {
                BeginContext(1679, 2208, true);
                WriteLiteral(@"
    <script>
        var vmd = new ViewModel();
        vmd.init(""#app"");
        function ViewModel() {
            var me = this;
            ViewModelBase.call(me);
            vmExtend.call(me);
            me.data.deptOptions = [];
            me.data.rolesOptions = [];
            me.data.mainTableConfig.columns = [
                {
                    field: 'Id', width: 50, titleAlign: 'center', columnAlign: 'center', title: 'ID', titleCellClassName: 'title-cell-class-name-title', isFrozen:true
                },
                { field: 'Key', width: 330, titleAlign: 'center', title: 'Key', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { field: 'Name', width: 330, titleAlign: 'center', title: '区域名称', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },



            ];

        }
        function vmExtend() {
            var me = this;

            me.methods.sortChange = f");
                WriteLiteral(@"unction (params) {
                if (params.height.length > 0) {

                    this.mainTableConfig.tableData.sort(function (a, b) {

                        if (params.height === 'asc') {

                            return a.height - b.height;
                        } else if (params.height === 'desc') {

                            return b.height - a.height;
                        } else {

                            return 0;
                        }
                    });
                }

            };
            
            me.mainLoadTablePagedDataUrl = ""/Api/sysconf/regions"";
            me.mainAddUrl = ""/Api/sysconf/auRegion"";
            me.mainDeleteUrl = ""/Api/sysconf/deleteRegion"";
            me.mainUpdateUrl = ""/Api/sysconf/auRegion"";
            me.data.mainDialog = new categoryDialog(this, true);
            me.mainDetailUrl = """";
            me.methods.OnCreate = function () {
              
            }
        }
        function categoryDi");
                WriteLiteral("alog(vm, _ismain) {\r\n            var me = this;\r\n            DialogBase.call(me, vm, _ismain);\r\n           \r\n        \r\n           \r\n        }\r\n    </script>\r\n\r\n");
                EndContext();
            }
            );
            BeginContext(3890, 2, true);
            WriteLiteral("\r\n");
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