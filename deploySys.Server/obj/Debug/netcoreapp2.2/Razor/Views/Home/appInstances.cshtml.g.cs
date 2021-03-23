#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\appInstances.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c38b6abf37763a94547b3b53268e9796e3d1a23"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_appInstances), @"mvc.1.0.view", @"/Views/Home/appInstances.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/appInstances.cshtml", typeof(AspNetCore.Views_Home_appInstances))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c38b6abf37763a94547b3b53268e9796e3d1a23", @"/Views/Home/appInstances.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_appInstances : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\appInstances.cshtml"
  
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
                BeginContext(184, 597, true);
                WriteLiteral(@"
    <el-button-group>
           <el-button type=""primary"" icon=""el-icon-switch-button"" v-on:click=""openStopInstance"" :disabled=""mainCommandDisnable"">停止</el-button>
           <el-button type=""primary"" icon=""el-icon-magic-stick"" v-on:click=""openRestartInstance"" :disabled=""mainCommandDisnable"">重启</el-button>
          <el-button type=""primary"" icon="""" v-on:click=""openInstanceLogs"" :disabled=""mainCommandDisnable"">日志</el-button>
         <el-button type=""primary"" icon=""el-icon-delete"" v-on:click=""openRemoveInstance"" :disabled=""mainCommandDisnable"">删除</el-button>
    </el-button-group>
");
                EndContext();
            }
            );
            BeginContext(784, 1135, true);
            WriteLiteral(@"
<template>
    <div>
 <v-table is-vertical-resize
                 :vertical-resize-offset='60'
                 is-horizontal-resize
                  column-width-drag
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
                  v-on:on-custom-comp=""customCompFunc""
                 :paging-index=""(mainPageIndex-1)*pageSize"">
                
        </v-table>

       <div class=""mt20 mb20 bold""></div>

        <div class=""pages"">
            <v-pagination v-on:page-change=""mainPageChange"" v-on:page-size-change=""mainPageSizeChange"" :total=""mainTotalCount"" :page-size=""pageSize"" :layout=""['total', 'p");
            WriteLiteral("rev\', \'pager\', \'next\', \'sizer\', \'jumper\']\">\r\n\r\n            </v-pagination>\r\n        </div>\r\n\r\n</template>\r\n\r\n\r\n");
            EndContext();
            DefineSection("vueScript", async() => {
                BeginContext(1938, 6403, true);
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
                { field: 'instanceId', width: 200, titleAlign: 'center', title: '实例Id', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { field: 'proxyPort', width: 100, titleAlign: 'center', title: '端口', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen: false },
                { field: 'IP', width: 100, titleAlign: 'center', title: '访问IP', columnAlign: 'center', titleCellClassName");
                WriteLiteral(@": 'title-cell-class-name-title', isFrozen: false },
                { field: 'domain', width: 100, titleAlign: 'center', title: '域名', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen: false },
                { field: 'isNginx', width: 100, titleAlign: 'center', title: '是Nginx', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen: false },
                
                { field: 'status', width: 100, titleAlign: 'center', title: '实例状态', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',formatter: function (rowData, rowIndex, pagingIndex, field) {
                        console.log(rowData);
                        return rowData.status == 0 ? '预分配' : rowData.status == 10 ? '待处理' : rowData.status == 20 ? '运行中' :rowData.status == -1 ? '已停止':rowData.status == 100 ? '等待停止':rowData.status == 101 ? '等待重启':rowData.status == 102 ? '等待删除': '其他未知'
                    }, isFrozen:false },
                { field: 'CreateDt");
                WriteLiteral(@"', width: 100, titleAlign: 'center', title: '创建时间', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },

                { field: 'appName', width: 200, titleAlign: 'center', title: '应用名称', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { field: 'key', width: 100, titleAlign: 'center', title: '应用Key', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen: false },
             { field: 'version', width: 100, titleAlign: 'center', title: '版本', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
           { field: 'port', width: 100, titleAlign: 'center', title: '应用端口', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },

            ];

        }
        function vmExtend() {
            var me = this;
             me.methods.openRemoveInstance = function () {
                $vmpa.confirm(");
                WriteLiteral(@"""确定要删除该实例吗？"", me.methods.removeInstance);
            };
            me.methods.removeInstance = function () {
                 var url =""/Api/SysManage/removeInstance/"" + me.data.mainSelectedModel[me.ModelKeyName];
                var params = {};
                var _this = this;
                $vmpa.post(url, params, function (result) {
                    var msg = result.Msg || ""处理成功"";
                    $vmpa.msg(msg);
                    if (result.Status == ResultStatus.OK) {
                        me.methods.getMainTableData();
                        me.methods.mainSelectedModel = null;
                    }
                });
            };
            me.methods.stopInstance = function () {
                 var url =""/Api/SysManage/stopInstance/"" + me.data.mainSelectedModel[me.ModelKeyName];
                var params = {};
                var _this = this;
                $vmpa.post(url, params, function (result) {
                    var msg = result.Msg || ""处理成功"";
     ");
                WriteLiteral(@"               $vmpa.msg(msg);
                    if (result.Status == ResultStatus.OK) {
                        me.methods.getMainTableData();
                        me.methods.mainSelectedModel = null;
                    }
                });
            };
            me.methods.openInstanceLogs = function () {
                var xx = top.layui.element;
                var tabUrl = ""/home/instanceLogs/"" + me.data.mainSelectedModel.Id;
                var tabDDid = ""instanceLogs-"" + me.data.mainSelectedModel.Id;
                var tabTitle = "" 实例日志["" + me.data.mainSelectedModel.key + ""]"";
                $vmpa.addOrChangeToTab(tabUrl, tabDDid, tabTitle);
            };
            me.methods.openStopInstance = function () {
               $vmpa.confirm(""确定要停止该实例吗？"", me.methods.stopInstance);
            };
             me.methods.restartInstance = function () {
                 var url =""/Api/SysManage/restartInstance/"" + me.data.mainSelectedModel[me.ModelKeyName];
                v");
                WriteLiteral(@"ar params = {};
                var _this = this;
                $vmpa.post(url, params, function (result) {
                    var msg = result.Msg || ""处理成功"";
                    $vmpa.msg(msg);
                    if (result.Status == ResultStatus.OK) {
                        me.methods.getMainTableData();
                        me.methods.mainSelectedModel = null;
                    }
                });
            };
            me.methods.openRestartInstance = function () {
                $vmpa.confirm(""确定要重启该实例吗？"", me.methods.restartInstance);
            };
            me.methods.sortChange = function (params) {
                if (params.height.length > 0) {

                    this.mainTableConfig.tableData.sort(function (a, b) {

                        if (params.height === 'asc') {

                            return a.height - b.height;
                        } else if (params.height === 'desc') {

                            return b.height - a.height;
         ");
                WriteLiteral(@"               } else {

                            return 0;
                        }
                    });
                }

            };
            
            me.mainLoadTablePagedDataUrl = ""/Api/SysManage/MicroServiceAppsInstance?appId=");
                EndContext();
                BeginContext(8342, 13, false);
#line 160 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\appInstances.cshtml"
                                                                                     Write(ViewBag.appId);

#line default
#line hidden
                EndContext();
                BeginContext(8355, 9, true);
                WriteLiteral(" &hostId=");
                EndContext();
                BeginContext(8365, 14, false);
#line 160 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\appInstances.cshtml"
                                                                                                            Write(ViewBag.hostId);

#line default
#line hidden
                EndContext();
                BeginContext(8379, 1393, true);
                WriteLiteral(@""";
            me.mainAddUrl = """";
            me.mainDeleteUrl = """";
            me.mainUpdateUrl = """";
            me.data.mainDialog = new appTypeDialog(this, true);
            me.mainDetailUrl = """";
            me.methods.OnCreate = function () {
              
            }
        }
        function appTypeDialog(vm, _ismain) {
            var me = this;
            DialogBase.call(me, vm, _ismain);
            me.OnSave = function (model) {
                var updateurl = """";
                
               
                if (me.EditModel[me._vm.ModelKeyName] > 0) {


                   

                    if (me.isMain)
                        updateurl = me._vm.mainUpdateUrl;
                    else
                        updateurl = me._vm.slaveUpdateUrl;
                    $vmpa.postJson(updateurl, me.EditModel, function (result) {
                        me.refreshTable();
                    })
                } else {
                    if (me.isMain)
    ");
                WriteLiteral(@"                    updateurl = me._vm.mainAddUrl;
                    else
                        updateurl = me._vm.slaveAddUrl;
                    $vmpa.postJson(updateurl, me.EditModel, function (result) {
                        me.refreshTable();
                    })
                }
            }
        
           
        }
    </script>

");
                EndContext();
            }
            );
            BeginContext(9775, 2, true);
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
