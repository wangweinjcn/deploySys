#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\zones.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3f96a8b8a4c5efec3076ce900e10071ae23aeef0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_zones), @"mvc.1.0.view", @"/Views/Home/zones.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/zones.cshtml", typeof(AspNetCore.Views_Home_zones))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3f96a8b8a4c5efec3076ce900e10071ae23aeef0", @"/Views/Home/zones.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_zones : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\zones.cshtml"
  
    Layout = "_dataLayout";

#line default
#line hidden
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("_search", async() => {
                BeginContext(56, 749, true);
                WriteLiteral(@"
    <el-row :gutter=""20"">
        <el-col :span=""8"">
            <div class=""grid-content"">
                <el-select v-model=""mainSearchModel.Id"" placeholder=""请选择"">
                    <el-option v-for=""item in categorys""
                               :key=""item.Id""
                               :label=""item.Name""
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
                BeginContext(826, 402, true);
                WriteLiteral(@"
    <el-button-group>
        <el-button type=""primary"" icon=""el-icon-edit"" v-on:click=""mainEdit"" :disabled=""mainCommandDisnable""></el-button>
        <el-button type=""primary"" icon=""el-icon-circle-plus"" v-on:click=""mainAdd""></el-button>
        <el-button type=""primary"" icon=""el-icon-delete"" v-on:click=""mainDelete"" :disabled=""mainCommandDisnable""></el-button>
       
    </el-button-group>
");
                EndContext();
            }
            );
            BeginContext(1231, 1118, true);
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
            BeginContext(2350, 24, false);
#line 67 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\zones.cshtml"
   Write(Html.Partial("zones_js"));

#line default
#line hidden
            EndContext();
            BeginContext(2374, 31, true);
            WriteLiteral("\r\n    </div>\r\n</template>\r\n\r\n\r\n");
            EndContext();
            DefineSection("vueScript", async() => {
                BeginContext(2424, 5287, true);
                WriteLiteral(@"

    <script>
        var vmd = new ViewModel();
        vmd.init(""#app"");
        Vue.component('table-Image',{
        template:`<span>
        <a href="""" v-on:click.stop.prevent=""ViewBar(rowData,index)"">查看二维码</a>&nbsp;
      
     <!--   <img :src='rowData[""barCodeUrl""]'  alt=""""  style=""width: 200px; height: 100px"" /> -->
    
        </span>`,
            props: {
               
            rowData:{
                type:Object
            },
            field:{
                type:String
            },
            index:{
                type:Number
            }
        },
        methods:{
            ViewBar(){

               // 参数根据业务场景随意构造
               let params = {type:'ViewBar',index:this.index,rowData:this.rowData};
               this.$emit('on-custom-comp',params);
            },

            deleteRow(){

                // 参数根据业务场景随意构造
                let params = {type:'delete',index:this.index};
                this.$emit('on-custom-comp',params);");
                WriteLiteral(@"

            }
        }
    })
        function ViewModel() {
            var me = this;
            ViewModelBase.call(me);
            vmExtend.call(me);
            me.data.categorys = [];
            me.data.barImageUrl = """";
            me.data.barCode = """";
            me.data.mainSearchModel = { ""keyword"": null, ""Id"": 1 };
            me.data.CurrencyList = [];
            me.data.CountryKeyList = [];
            me.data.UMlist = [];

            me.data.mainTableConfig.columns = [
                 {
                    field: 'Id', width: 50, titleAlign: 'center', columnAlign: 'center', title: 'ID', titleCellClassName: 'title-cell-class-name-title', isFrozen: true
                },
                { field: 'Key', width: 330, titleAlign: 'center', title: 'Key', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isFrozen: true },
                { field: 'Name', width: 330, titleAlign: 'center', title: '集群名称', columnAlign: 'center', titleCellClassName: 'tit");
                WriteLiteral(@"le-cell-class-name-title', isFrozen: true },

            ];

        }
        function vmExtend() {
            var me = this;
            me.methods.customCompFunc = function (params) {
                console.log(params);

                if (params.type === 'delete') { // do delete operation

                    //this.$delete(this.tableData, params.index);

                } else if (params.type === 'ViewBar') { // do edit operation
                    me.data.barImageUrl = params.rowData['barCodeUrl'];
                    me.data.barCode = params.rowData['BarCode'];
                    me.data.barImageDialog.Open(null);
                }
            }

            me.methods.sortChange = function (params) {
                if (params.height.length > 0) {

                    this.mainTableConfig.tableData.sort(function (a, b) {

                        if (params.height === 'asc') {

                            return a.height - b.height;
                        } else if (");
                WriteLiteral(@"params.height === 'desc') {

                            return b.height - a.height;
                        } else {

                            return 0;
                        }
                    });
                }

            };
            me.mainLoadTablePagedDataUrl = ""/Api/sysconf/Zones"";
            me.mainAddUrl = ""/Api/sysconf/auZone"";
            me.mainDeleteUrl = ""/Api/sysconf/deleteZone"";
            me.mainUpdateUrl = ""/Api/sysconf/auZone"";
            me.data.mainDialog = new goodsDialog(this, true);
            me.mainDetailUrl = """";
            me.methods.OnCreate = function () {

                $vmpa.get(""/Api/sysconf/Regions?PageSize=100"", null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.categorys = result.Data.DataList;
                    }
                });
               
            }
            me.methods.mainAdd = function () {
                var newmodel = { Id: -1, allrole");
                WriteLiteral(@"s: [] };
                me.OnMainEdit(newmodel, ""新增"");
            }
        }
        function goodsDialog(vm, _ismain) {
            var me = this;
            DialogBase.call(me, vm, _ismain);

            me.OnSave = function (model) {
                var updateurl = """";


                if (me.EditModel[me._vm.ModelKeyName] > 0) {


                    if (me.isMain)
                        updateurl = me._vm.mainUpdateUrl + ""/"" + me.EditModel[""Ass_Region_Id""];
                    else
                        updateurl = me._vm.slaveUpdateUrl;
                    $vmpa.post(updateurl, me.EditModel, function (result) {
                        me.refreshTable();
                    })
                } else {
                    if (me.isMain)
                        updateurl = me._vm.mainAddUrl + ""/"" + me.EditModel[""Ass_Region_Id""];
                    else
                        updateurl = me._vm.slaveAddUrl;
                    $vmpa.post(updateurl, me.EditModel, function");
                WriteLiteral(" (result) {\r\n                        me.refreshTable();\r\n                    })\r\n                }\r\n            }\r\n\r\n\r\n        }\r\n\r\n       \r\n       \r\n    </script>\r\n\r\n");
                EndContext();
            }
            );
            BeginContext(7714, 2, true);
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
