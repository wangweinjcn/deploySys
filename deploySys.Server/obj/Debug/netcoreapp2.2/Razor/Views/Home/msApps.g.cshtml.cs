#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\msApps.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bc3cbf0fcb4f04993eae2cddf817d06f33f01f5f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_msApps), @"mvc.1.0.view", @"/Views/Home/msApps.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/msApps.cshtml", typeof(AspNetCore.Views_Home_msApps))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc3cbf0fcb4f04993eae2cddf817d06f33f01f5f", @"/Views/Home/msApps.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_msApps : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\msApps.cshtml"
  
    Layout = "_dataLayout";

#line default
#line hidden
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("_search", async() => {
                BeginContext(56, 751, true);
                WriteLiteral(@"
    <el-row :gutter=""20"">
        <el-col :span=""8"">
            <div class=""grid-content"">
                <el-select v-model=""mainSearchModel.Id"" placeholder=""请选择"">
                    <el-option v-for=""item in allAppTypes""
                               :key=""item.Id""
                               :label=""item.name""
                               :value=""item.Id"">
                    </el-option>
                </el-select>
            </div>
        </el-col>
            <el-col :span=""12"">
            <div class=""grid-content"">
                <searchEditor v-bind:SearchModel=""mainSearchModel"" v-on:dosearch=""getMainTableData""></searchEditor>
            </div>
        </el-col>
        
    </el-row>


       
");
                EndContext();
            }
            );
            DefineSection("_command", async() => {
                BeginContext(828, 942, true);
                WriteLiteral(@"
    <el-button-group>
        <el-button type=""primary"" icon=""el-icon-edit"" v-on:click=""mainEdit"" :disabled=""mainCommandDisnable""></el-button>
        <el-button type=""primary"" icon=""el-icon-circle-plus"" v-on:click=""mainAdd""></el-button>
        <el-button type=""primary"" icon=""el-icon-delete"" v-on:click=""mainDelete"" :disabled=""mainCommandDisnable""></el-button>
         <el-button type=""primary"" icon=""el-icon-s-grid"" v-on:click=""openAppVersions"" :disabled=""mainCommandDisnable"">查看版本</el-button>
        <el-button type=""primary"" icon=""el-icon-s-tools"" v-on:click=""openAppInstances"" :disabled=""mainCommandDisnable"">查看实例</el-button>
         <el-button type=""primary"" icon=""el-icon-s-tools"" v-on:click=""openAppWebsites"" :disabled=""mainCommandDisnable"">查看站点</el-button>
       <el-button type=""primary"" icon=""el-icon-document-copy"" v-on:click=""openCopyAppEdit"" :disabled=""mainCommandDisnable"">复制</el-button>
    </el-button-group>
");
                EndContext();
            }
            );
            BeginContext(1773, 1118, true);
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
                  v-on:on-custom-comp=""customCompFunc""
                 :paging-index=""(mainPageIndex-1)*pageSize"">
                
        </v-table>

        <div class=""mt20 mb20 bold""></div>

        <div class=""pages"">
            <v-pagination v-on:page-change=""mainPageChange"" v-on:page-size-change=""mainPageSizeChange"" :total=""mainTotalCount"" :page-size=""pageSize"" :layout=""['total', 'prev', 'pager', 'next', 'sizer");
            WriteLiteral("\', \'jumper\']\">\r\n\r\n            </v-pagination>\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
            EndContext();
            BeginContext(2892, 25, false);
#line 70 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\msApps.cshtml"
   Write(Html.Partial("msApps_js"));

#line default
#line hidden
            EndContext();
            BeginContext(2917, 31, true);
            WriteLiteral("\r\n    </div>\r\n</template>\r\n\r\n\r\n");
            EndContext();
            DefineSection("vueScript", async() => {
                BeginContext(2967, 8507, true);
                WriteLiteral(@"

    <script>
        var vmd = new ViewModel();
        vmd.init(""#app"");
        function ViewModel() {
            var me = this;
            ViewModelBase.call(me);
            vmExtend.call(me);
            me.data.allAppTypes = [];
            me.data.regions = [];
             me.data.regionIds = [];
            me.data.mainSearchModel = { ""keyword"": null, ""Id"": 1 };


            me.data.mainTableConfig.columns = [
                 {
                    field: 'Id', width: 50, titleAlign: 'center', columnAlign: 'center', title: 'ID', titleCellClassName: 'title-cell-class-name-title', isFrozen:true
                },
                { field: 'key', width: 150, titleAlign: 'center', title: 'key', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { field: 'appName', width: 200, titleAlign: 'center', title: '应用名称', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { fie");
                WriteLiteral(@"ld: 'port', width: 100, titleAlign: 'center', title: '监听端口', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { field: 'memo', width: 300, titleAlign: 'center', title: '备注', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },
                { field: 'useSsl', width: 300, titleAlign: 'center', title: '是否使用ssl', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen:false },

            ];

        }
        function vmExtend() {
            var me = this;
            me.methods.customCompFunc = function (params) {
                console.log(params);

               
            }
             me.methods.openAppVersions = function () {
                if (me.data.appVDialog != null) {
                    me.data.appVDialog.Open(me.data.mainSelectedModel, ""版本列表"");
                }
            }
              me.methods.openCopyAppEdit = function () {
             ");
                WriteLiteral(@"   var newobj = {};
                $.extend(true, newobj, me.data.mainSelectedModel);
                  newobj.Id = -1;
                  newobj.Key = """";
               
                me.data.mainDialog.Open(newobj, ""新增"");
            }
         me.methods.openAppInstances = function () {
                var xx = top.layui.element;
                var tabUrl = ""/home/appInstances/"" + me.data.mainSelectedModel.Id;
                var tabDDid = ""appInstances-"" + me.data.mainSelectedModel.Id;
                var tabTitle = ""应用运行实例["" + me.data.mainSelectedModel.key + ""]"";
                $vmpa.addOrChangeToTab(tabUrl, tabDDid, tabTitle);
            }
             me.methods.openAppWebsites = function () {
                var xx = top.layui.element;
                var tabUrl = ""/home/webSites/"" + me.data.mainSelectedModel.Id;
                var tabDDid = ""website-"" + me.data.mainSelectedModel.Id;
                var tabTitle = ""应用运行站点["" + me.data.mainSelectedModel.key + ""]"";
             ");
                WriteLiteral(@"   $vmpa.addOrChangeToTab(tabUrl, tabDDid, tabTitle);
            }
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
            me.mainLoadTablePagedDataUrl = ""/Api/sysconf/MicroServiceApps"";
            me.mainAddUrl = ""/Api/sysconf/auMicroServiceApp"";
            me.mainDeleteUrl = ""/Api/sysconf/deleteMicroServiceApp"";
            me.mainUpdateUrl = ""/Api/sysconf/auMicroServiceApp"";
            me.data.mainDialog = new goodsDialog(this, true);
            me.data.appVDial");
                WriteLiteral(@"og = new appVersionsDialog(this,true);
            me.mainDetailUrl = """";
            me.methods.OnCreate = function () {

                $vmpa.get(""/Api/sysconf/AppTypes?PageSize=100"", null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.allAppTypes = result.Data.DataList;
                    }
                });
               $vmpa.get(""/Api/sysconf/Regions?PageSize=100"", null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.regions = result.Data.DataList;
                       
                    }
                });
            }
            me.methods.mainAdd = function () {
                var newmodel = { Id: -1,useSsl:false,serviceType:0};
                me.OnMainEdit(newmodel, ""新增"");
            }
             me.methods.mainEdit = function () {
                 if (me.data.mainSelectedModel.regionIds == null) {
                     me.data.regi");
                WriteLiteral(@"onIds = [];
                }
                else {
                    me.data.regionIds = me.data.mainSelectedModel.regionIds.split(',');
                        }
                    me.OnMainEdit(me.data.mainSelectedModel, ""编辑"");
             }
        }
        function goodsDialog(vm, _ismain) {
            var me = this;
            DialogBase.call(me, vm, _ismain);
            me.editAppTypeOnchange = function () {
                me._vm.data.allAppTypes.forEach(function (value, index, array) {
                    if (value.Id == me.EditModel.Ass_AppType_Id) {
                        if (me.EditModel.buildScript == null) {
                            me.EditModel.buildScript = value.buildScript;
                        };
                        if (me.EditModel.startCommand == null) {
                            me.EditModel.startCommand = value.startCommand;
                        };
                        if (me.EditModel.stopCommand == null) {
                            me");
                WriteLiteral(@".EditModel.stopCommand = value.stopCommand;
                        };
                        if (me.EditModel.createContainerParams == null) {
                            me.EditModel.createContainerParams = value.createContainerParams;

                        };
                        if (me.EditModel.createContainerParams2 == null) {
                            me.EditModel.createContainerParams2 = value.createContainerParams2;
                        }
                    }
                });
                
            }
            me.OnSave = function (model) {
                var updateurl = """";

                
                    me.EditModel.regionIds = me._vm.data.regionIds.join(',');
                  
                if (me.EditModel[me._vm.ModelKeyName] > 0) {


                    if (me.isMain)
                        updateurl = me._vm.mainUpdateUrl + ""/"" + me.EditModel[""Ass_AppType_Id""];
                    else
                        updateurl = me._vm.slave");
                WriteLiteral(@"UpdateUrl;
                   var ret= $vmpa.postSync(updateurl, me.EditModel, function (result) {

                    });
                     return ret.responseJSON.Status;
                } else {
                    if (me.isMain)
                        updateurl = me._vm.mainAddUrl + ""/"" + me.EditModel[""Ass_AppType_Id""];
                    else
                        updateurl = me._vm.slaveAddUrl;
                  
                    var ret= $vmpa.postSync(updateurl, me.EditModel, function (result) {

                    });
                     return ret.responseJSON.Status;
                }
            }


        }

        function appVersionsDialog(vm, _ismain) {
            var me = this;
             me.tableData = [];
            DialogBase.call(me, vm, _ismain);
             me.refreshTable = function () {
                $vmpa.get(""/Api/sysconf/lastAppVersions/"" + me.EditModel.Id, null, function (result) {
                    if (result.Status == ResultStat");
                WriteLiteral(@"us.OK) {
                        console.log(""now refresh tabledata"")
                        me.tableData = result.Data;
                    }
                });
            }
            me.OnOpen = function () {


                me.refreshTable();
            }
        }
       
    </script>

");
                EndContext();
            }
            );
            BeginContext(11477, 2, true);
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
