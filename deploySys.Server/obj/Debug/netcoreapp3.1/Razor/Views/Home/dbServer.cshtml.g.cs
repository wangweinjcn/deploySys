#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\dbServer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "664ee1081a6175100cccabc9c6d8e5be62fa8e7a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_dbServer), @"mvc.1.0.view", @"/Views/Home/dbServer.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"664ee1081a6175100cccabc9c6d8e5be62fa8e7a", @"/Views/Home/dbServer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_dbServer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\dbServer.cshtml"
  
    Layout = "_dataLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("_search", async() => {
                WriteLiteral("\r\n    <searchEditor v-bind:SearchModel=\"mainSearchModel\" v-on:dosearch=\"getMainTableData\"></searchEditor>\r\n");
            }
            );
            DefineSection("_command", async() => {
                WriteLiteral(@"
    <el-button-group>
        <el-button type=""primary"" icon=""el-icon-edit"" v-on:click=""mainEdit"" :disabled=""mainCommandDisnable""></el-button>
        <el-button type=""primary"" icon=""el-icon-circle-plus"" v-on:click=""mainAdd"" ></el-button>
        <el-button type=""primary"" icon=""el-icon-delete"" v-on:click=""mainDelete"" :disabled=""mainCommandDisnable""></el-button>
        <el-button type=""primary"" icon=""el-icon-s-tools"" v-on:click=""openDbInstance"" :disabled=""mainCommandDisnable"">查看数据库</el-button>
       
    </el-button-group>
");
            }
            );
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
#nullable restore
#line 46 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\dbServer.cshtml"
   Write(Html.Partial("dbServer_js"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</template>\r\n\r\n\r\n");
            DefineSection("vueScript", async() => {
                WriteLiteral(@"
    <script>
        var vmd = new ViewModel();
        vmd.init(""#app"");
        function ViewModel() {
            var me = this;
            ViewModelBase.call(me);
            vmExtend.call(me);
            me.data.deptOptions = [];
            me.data.rolesOptions = [{Id:0,Name:""mysql""},{Id:1,Name:""sqlite""},{Id:2,Name:""sqlserver""},{Id:3,Name:""oracle""},{Id:4,Name:""pgsql""}];
            me.data.mainTableConfig.columns = [
                {
                    field: 'Id', width: 80, titleAlign: 'center', columnAlign: 'center', title: 'ID', titleCellClassName: 'title-cell-class-name-title', isFrozen:true
                },
                { field: 'Name', width: 120, titleAlign: 'center', title: '名称', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',isResize: true},
                { field: 'dbType', width: 120, titleAlign: 'center', title: '数据库类型', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',isResize: true },
                { field: 'IP");
                WriteLiteral(@"', width: 120, titleAlign: 'center', title: 'Ip地址', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',isResize: true },

                { field: 'Port', width: 200, titleAlign: 'center', title: '端口号', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true },
                { field: 'rootUser', width: 200, titleAlign: 'center', title: 'root用户', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true },


            ];

        }
        function vmExtend() {
            var me = this;

            me.methods.sortChange = function (params) {
                if (params.height.length > 0) {

                    this.mainTableConfig.tableData.sort(function (a, b) {

                        if (params.height === 'asc') {

                            return a.height - b.height;
                        } else if (params.height === 'desc') {

                            return b.height - a.height;
      ");
                WriteLiteral(@"                  } else {

                            return 0;
                        }
                    });
                }

            };
            me.mainLoadTablePagedDataUrl = ""/Api/SysConf/DbServers"";
            me.mainAddUrl = ""/Api/SysConf/auDbServer"";
            me.mainDeleteUrl = ""/Api/SysConf/deleteDbServer"";
            me.mainUpdateUrl = ""/Api/SysConf/auDbServer"";
            me.data.mainDialog = new userDialog(this, true);
            me.mainDetailUrl = "" "";
           
            me.methods.mainAdd = function () {
                var newmodel = { Id: -1, allroles: [],Password:"""" };
                me.OnMainEdit(newmodel, ""新增"");
            };
              me.methods.openDbInstance = function () {
                var xx = top.layui.element;
                var tabUrl = ""/home/DbInstance/"" + me.data.mainSelectedModel.Id;
                var tabDDid = ""DbInstance-"" + me.data.mainSelectedModel.Id;
                var tabTitle = ""数据库["" + me.data.mainSelectedMod");
                WriteLiteral(@"el.Name + ""]"";
                $vmpa.addOrChangeToTab(tabUrl, tabDDid, tabTitle);
            }
        }
        function userDialog(vm, _ismain) {
            var me = this;
            DialogBase.call(me, vm, _ismain);
            me.AesEncrypt = function (source, key) {
                var key = CryptoJS.enc.Utf8.parse(key);//32位
                var iv = CryptoJS.enc.Utf8.parse(""");
#nullable restore
#line 122 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\dbServer.cshtml"
                                             Write(ViewBag.AesIV);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""");//16位
                var srcs = CryptoJS.enc.Utf8.parse(source);
                var encrypted = CryptoJS.AES.encrypt(srcs, key, {
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                });
                return encrypted.ciphertext.toString();
            };
            me.OnSave = function (model) {
                var updateurl = """";
                if (me.EditModel[""Password""] != null) {
                    me.EditModel[""rootPassword""] = me.AesEncrypt(me.EditModel[""Password""], """);
#nullable restore
#line 134 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\dbServer.cshtml"
                                                                                       Write(ViewBag.AesKey);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""")
                }
                if (me.EditModel[me._vm.ModelKeyName] > 0) {


                    if (me.isMain)
                        updateurl = me._vm.mainUpdateUrl;
                    else
                        updateurl = me._vm.slaveUpdateUrl;
                    var ret = $vmpa.postSync(updateurl, me.EditModel, function (result) {

                    });
                     return ret.responseJSON.Status;
                } else {
                    if (me.isMain)
                        updateurl = me._vm.mainAddUrl;
                    else
                        updateurl = me._vm.slaveAddUrl;
                    var ret = $vmpa.postSync(updateurl, me.EditModel, function (result) {

                    });
                     return ret.responseJSON.Status;
                }
            }
        
           
        }
    </script>

");
            }
            );
            WriteLiteral("\r\n");
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