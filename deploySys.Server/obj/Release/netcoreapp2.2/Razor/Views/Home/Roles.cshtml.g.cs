#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Roles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ede8ec74b58578f4a07b6286b1cf6635cebf0fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Roles), @"mvc.1.0.view", @"/Views/Home/Roles.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Roles.cshtml", typeof(AspNetCore.Views_Home_Roles))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ede8ec74b58578f4a07b6286b1cf6635cebf0fa", @"/Views/Home/Roles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Roles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Roles.cshtml"
  
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
            BeginContext(581, 1046, true);
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
                 :paging-index=""(mainPageIndex-1)*pageSize"">

        </v-table>

        <div class=""mt20 mb20 bold""></div>

        <div class=""pages"">
            <v-pagination v-on:page-change=""mainPageChange"" v-on:page-size-change=""mainPageSizeChange"" :total=""mainTotalCount"" :page-size=""pageSize"" :layout=""['total', 'prev', 'pager', 'next', 'sizer', 'jumper']"">

            </v-pagination>
        </div>
    </div");
            WriteLiteral(">\r\n    <div>\r\n        ");
            EndContext();
            BeginContext(1628, 24, false);
#line 44 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Roles.cshtml"
   Write(Html.Partial("Roles_js"));

#line default
#line hidden
            EndContext();
            BeginContext(1652, 31, true);
            WriteLiteral("\r\n    </div>\r\n</template>\r\n\r\n\r\n");
            EndContext();
            DefineSection("vueScript", async() => {
                BeginContext(1702, 4204, true);
                WriteLiteral(@"
    <script>
        var vmd = new ViewModel();
        vmd.init(""#app"");
        function ViewModel() {
            var me = this;
            ViewModelBase.call(me);
            vmExtend.call(me);
            me.data.roleMenus = [];
            me.data.deptOptions = [];
            me.data.menuOptions = [];
            me.data.mainTableConfig.columns = [
                {
                    field: 'Id', width: 80, titleAlign: 'center', columnAlign: 'center', title: 'ID', titleCellClassName: 'title-cell-class-name-title', isFrozen: true
                },
                { field: 'Name', width: 120, titleAlign: 'center', title: '名称', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true },

                { field: 'RoleID', width: 120, titleAlign: 'center', title: '角色名', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',isResize: true }
               


            ];

        }
        function vmExtend() {
            va");
                WriteLiteral(@"r me = this;

            me.methods.sortChange = function (params) {
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
            me.mainLoadTablePagedDataUrl = ""/Api/SysConf/Roles"";
            me.mainAddUrl = ""/Api/SysConf/AuRole"";
            me.mainDeleteUrl = ""/Api/SysConf/DeleteRole"";
            me.mainUpdateUrl = ""/Api/SysConf/AuRole"";
            me.data.mainDialog = new userDialog(this, true);
            me.mainDetailUrl = """";
            me.methods.OnCreate = function () {
                $vmpa.get(""/Api/SysConf/Me");
                WriteLiteral(@"nus"", {PageSize:100}, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.menuOptions = result.Data.DataList;
                    }
                });
                
            }
            me.methods.mainAdd = function () {
                var newmodel = { Id: -1, allMenus: []};
                me.OnMainEdit(newmodel, ""新增"");
            }
        }
        function userDialog(vm, _ismain) {
            var me = this;
           
            DialogBase.call(me, vm, _ismain);
            me.roleMenus = [];
            me.OnOpen = function (model) {

                $vmpa.get(""/Api/SysConf/RoleMenuTree?roleId="" + me.EditModel.Id, { PageSize: 100 }, function (result) {
                      if (result.Status == ResultStatus.OK) {
                          me._vm.data.mainDialog.roleMenus = result.Data;
                    }
                });
            };
            me.OnSave = function (model) {
                var up");
                WriteLiteral(@"dateurl = """";
                
                if (me.EditModel.OrigonPassword != null &&
                    me.EditModel.OrigonPassword !="""")
                    me.EditModel[""Password""] = $.md5($.trim(me.EditModel[""OrigonPassword""]))
                me.EditModel[""menuIds""] = me.roleMenus.join("","");
                if (me.EditModel[me._vm.ModelKeyName] > 0) {


                    if (me.isMain)
                        updateurl = me._vm.mainUpdateUrl;
                    else
                        updateurl = me._vm.slaveUpdateUrl;
                    $vmpa.post(updateurl, me.EditModel, function (result) {
                        me.refreshTable();
                    })
                } else {
                    if (me.isMain)
                        updateurl = me._vm.mainAddUrl;
                    else
                        updateurl = me._vm.slaveAddUrl;
                    $vmpa.post(updateurl, me.EditModel, function (result) {
                        me.refreshTable();
 ");
                WriteLiteral("                   })\r\n                }\r\n            }\r\n        \r\n           \r\n        }\r\n    </script>\r\n\r\n");
                EndContext();
            }
            );
            BeginContext(5909, 2, true);
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
