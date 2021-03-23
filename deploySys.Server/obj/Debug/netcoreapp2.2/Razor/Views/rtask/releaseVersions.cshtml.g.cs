#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\rtask\releaseVersions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f2d24496924c5f6cf2367836ecb55c9d7cb883b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_rtask_releaseVersions), @"mvc.1.0.view", @"/Views/rtask/releaseVersions.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/rtask/releaseVersions.cshtml", typeof(AspNetCore.Views_rtask_releaseVersions))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f2d24496924c5f6cf2367836ecb55c9d7cb883b", @"/Views/rtask/releaseVersions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_rtask_releaseVersions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\rtask\releaseVersions.cshtml"
  
    Layout = "_dataLayout";

#line default
#line hidden
            BeginContext(36, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("_search", async() => {
                BeginContext(56, 2243, true);
                WriteLiteral(@"
    <el-row :gutter=""10"">
         <el-col :span=""12"">
            <div class=""grid-content"">
                <el-select v-model=""mainSearchModel.regionId"" placeholder=""请选择"" v-on:change=""regionOnchange()"">
                    <el-option v-for=""item in regions""
                               :key=""item.Id""
                               :label=""item.Name""
                               :value=""item.Id"">
                    </el-option>
                </el-select>
            </div>
        </el-col>
        <el-col :span=""12"">
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
          
         </el-row>
        <el-row>
 ");
                WriteLiteral(@"            <el-col :span=""8"">
            <div class=""grid-content"">
                <el-select v-model=""mainSearchModel.appTypeId"" placeholder=""请选择"" v-on:change=""appTypeOnchange()"">
                    <el-option v-for=""item in appTypes""
                               :key=""item.Id""
                               :label=""item.name""
                               :value=""item.Id"">
                    </el-option>
                </el-select>
            </div>
        </el-col>
                      <el-col :span=""8"">
            <div class=""grid-content"">
                <el-select v-model=""mainSearchModel.msAppId"" placeholder=""请选择"">
                    <el-option v-for=""item in msApps""
                               :key=""item.Id""
                               :label=""item.appName""
                               :value=""item.Id"">
                    </el-option>
                </el-select>
            </div>
        </el-col>
          <el-col :span=""8"">
            <div class=""gri");
                WriteLiteral("d-content\">\r\n                <searchEditor v-bind:SearchModel=\"mainSearchModel\" v-on:dosearch=\"getMainTableData\"></searchEditor>\r\n            </div>\r\n        </el-col>\r\n       \r\n    </el-row>\r\n\r\n");
                EndContext();
            }
            );
            DefineSection("_command", async() => {
                BeginContext(2320, 1337, true);
                WriteLiteral(@"
    <el-button-group>
        <el-button type=""primary"" icon=""el-icon-edit"" v-on:click=""mainEdit"" :disabled=""mainCommandDisnable""></el-button>
        <el-button type=""primary"" icon=""el-icon-circle-plus"" v-on:click=""mainAdd""></el-button>
        <el-button type=""primary"" icon=""el-icon-delete"" v-on:click=""mainDelete"" :disabled=""mainCommandDisnable""></el-button>
         <el-button type=""primary"" icon=""el-icon-key"" v-on:click=""auditTask"" :disabled=""mainCommandDisnable"">审核</el-button>
         <el-button type=""primary"" icon=""el-icon-switch-button"" v-on:click=""openRestartTask"" :disabled=""mainCommandDisnable"">重启</el-button>
        <el-button type=""primary"" icon=""el-icon-s-operation"" v-on:click=""openFileItems"" :disabled=""mainCommandDisnable"">发布文件</el-button>
        <el-button type=""primary"" icon=""el-icon-set-up"" v-on:click=""openHostTask"" :disabled=""mainCommandDisnable"">主机任务</el-button>
        <el-button type=""primary"" icon=""el-icon-download"" v-on:click=""downFile"" :disabled=""mainCommandDisnable""></el-bu");
                WriteLiteral(@"tton>
        <el-button type=""primary"" icon=""el-icon-document-copy"" v-on:click=""openCopyRtEdit"" :disabled=""mainCommandDisnable"">复制</el-button>
        <el-button type=""primary"" icon=""el-icon-document-copy"" v-on:click=""openUpdateDialog"" :disabled=""mainCommandDisnable"">版本更新</el-button>
    </el-button-group>
");
                EndContext();
            }
            );
            BeginContext(3660, 1155, true);
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
            <v-pagination v-on:page-change=""mainPageChange"" v-on:page-size-change=""mainPageSizeChange"" :total=""mainTotalCount"" :page-size=""pageSize"" :layout=""['to");
            WriteLiteral("tal\', \'prev\', \'pager\', \'next\', \'sizer\', \'jumper\']\">\r\n\r\n            </v-pagination>\r\n        </div>\r\n    </div>\r\n    <div>\r\n        ");
            EndContext();
            BeginContext(4816, 34, false);
#line 108 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\rtask\releaseVersions.cshtml"
   Write(Html.Partial("releaseVersions_js"));

#line default
#line hidden
            EndContext();
            BeginContext(4850, 31, true);
            WriteLiteral("\r\n    </div>\r\n</template>\r\n\r\n\r\n");
            EndContext();
            DefineSection("vueScript", async() => {
                BeginContext(4900, 21031, true);
                WriteLiteral(@"

    <script>
        var vmd = new ViewModel();
        vmd.init(""#app"");
      
        function ViewModel() {
            var me = this;
            ViewModelBase.call(me);
            vmExtend.call(me);
            me.data.categorys = [];
            me.data.regions = [];
            me.data.appTypes = [];
            me.data.msApps = [];
            me.data.selectHostIds = [];
            me.data.mainSearchModel = { ""keyword"": null, ""Id"": null ,""regionId"":null,""appTypeId"":null,""msAppId"":null,""unzipInServer"":0};


            me.data.mainTableConfig.columns = [
                 {
                    field: 'Id', width: 50, titleAlign: 'center', columnAlign: 'center', title: 'ID', titleCellClassName: 'title-cell-class-name-title', isFrozen:true
                },
                { field: 'appName', width: 200, titleAlign: 'center', title: '应用名称', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true },
                { field: 'Version', width: 200");
                WriteLiteral(@", titleAlign: 'center', title: '发布版本', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true },
                { field: 'releaseType', width: 100, titleAlign: 'center', title: '发布类型', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', formatter: function (rowData, rowIndex, pagingIndex, field) {                        
                        return rowData.releaseType == 0 ? '新增实例' : rowData.releaseType == 1 ? '更新实例' : '其他'
                    },isResize: true },
                { field: 'overridePolicy', width: 100, titleAlign: 'center', title: '文件覆盖', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',  formatter: function (rowData, rowIndex, pagingIndex, field) {                        
                        return rowData.overridePolicy == 0 ? '部分文件全覆盖' : rowData.overridePolicy == 2 ? '全部文件全覆盖' : rowData.overridePolicy == 1 ?""对比覆盖"" :'其他'
                    },isResize: true },

                 { field: 'FileName',");
                WriteLiteral(@" width: 200, titleAlign: 'center', title: '文件名', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true },
                { field: 'status', width: 50, titleAlign: 'center', title: '状态', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title',  formatter: function (rowData, rowIndex, pagingIndex, field) {
                       
                        return rowData.status == 0 ? '待处理' : rowData.status == 10 ? '已分配' : rowData.status == 20? '已发布' : rowData.status == 11 ? '已审核' : rowData.status == 30 ? ""已注册"" : rowData.status == -1 ? ""已取消"" :'其他'
                    }, isResize: true },
                { field: 'count', width: 100, titleAlign: 'center', title: '数量', columnAlign: 'center', titleCellClassName: 'title-cell-class-name-title', isResize: true }  


            ];

        }
        function vmExtend() {
            var me = this;
            me.methods.openFileItems = function () {
                var xx = top.layui.element;
          ");
                WriteLiteral(@"      var tabUrl = ""/rtask/taskFiles/"" + me.data.mainSelectedModel.Id;
                var tabDDid = ""taskFiles-"" + me.data.mainSelectedModel.Id;
                var tabTitle = ""版本文件["" + me.data.mainSelectedModel.Version + ""]"";
                $vmpa.addOrChangeToTab(tabUrl, tabDDid, tabTitle);
            };
            me.methods.openHostTask = function () {
                 var tabUrl = ""/rtask/taskHosts/"" + me.data.mainSelectedModel.Id;
                var tabDDid = ""taskHosts-"" + me.data.mainSelectedModel.Id;
                var tabTitle = ""分配主机任务["" + me.data.mainSelectedModel.Version + ""]"";
                $vmpa.addOrChangeToTab(tabUrl, tabDDid, tabTitle);
            };
             me.methods.downFile = function () {
                  var $eleForm = $(""<form method='get'></form>"");
                $eleForm.attr(""action"", '/Api/SysManage/getRleaseVersionFile/' + me.data.mainSelectedModel.Id);
                $(document.body).append($eleForm);
                $eleForm.submit();
          ");
                WriteLiteral(@"  };
            me.methods.openTaskAudit = function () {
                $vmpa.confirm(""确定要发布该任务吗？"", me.methods.auditTask);
            };
             me.methods.auditTask = function () {
                 var url =""/Api/SysManage/auditReleaseTask""+ ""/"" + me.data.mainSelectedModel[me.ModelKeyName];
                var params = {};
                var _this = this;
                $vmpa.post(url, params, function (result) {
                    var msg = result.Msg || ""审核成功"";
                    $vmpa.msg(msg);
                    me.methods.getMainTableData();
                    me.methods.mainSelectedModel = null;

                });
            };
            me.methods.openRestartTask = function () {
                if (me.data.mainSelectedModel.status <0) {
                    $vmpa.confirm(""确定要重启该任务吗？"", me.methods.restartTask);
                }
            };
            
             me.methods.restartTask = function () {
                 var url =""/Api/SysManage/restartRelease");
                WriteLiteral(@"Task""+ ""/"" + me.data.mainSelectedModel[me.ModelKeyName];
                var params = {};
                var _this = this;
                $vmpa.post(url, params, function (result) {
                    me.methods.getMainTableData();
                    me.methods.mainSelectedModel = null;
                });
            };
           
            me.methods.regionOnchange = function () {
                me.data.categorys = [];

                $vmpa.get(""/Api/sysconf/Zones?PageSize=100&Id="" + me.data.mainSearchModel.regionId, null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.categorys = result.Data.DataList;
                        me.data.categorys.splice(0, 0, {Id:null});
                        if (me.data.categorys.length > 0) {
                            me.data.mainSearchModel.Id = me.data.categorys[0].Id;
                        }
                    }
                });
            };
            me.methods.a");
                WriteLiteral(@"ppTypeOnchange = function () {
                me.data.msApps = [];

                $vmpa.get(""/Api/sysconf/MyMicroServiceApps?PageSize=100&Id="" + me.data.mainSearchModel.appTypeId, null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.msApps = result.Data.DataList;
                        me.data.msApps.splice(0, 0, {Id:null});
                        if (me.data.msApps.length > 0) {
                            me.data.mainSearchModel.msAppId = me.data.msApps[0].Id;
                        }
                    }
                });
            };
            me.methods.openCopyRtEdit = function () {
                var newobj = {};
                console.log( me.data.mainSelectedModel)
                $.extend(true, newobj, me.data.mainSelectedModel);
                 console.log( newobj)
                newobj.Id = -1;
                var c = newobj.Version.split('.');
                c[c.length - 1] =parseInt(c[c.length ");
                WriteLiteral(@"- 1]) + 1;
                newobj.Version = c.join(""."");
                me.data.mainDialog.Open(newobj, ""新增"");
            }
            me.methods.openUpdateDialog = function () {
                 var newobj = {};
                console.log(""old"", me.data.mainSelectedModel)
                $.extend(true, newobj, me.data.mainSelectedModel);
                 console.log(""new"", newobj)
                newobj.Id = -1;
                var c = newobj.Version.split('.');
                c[c.length - 1] =parseInt(c[c.length - 1]) + 1;
                newobj.Version = c.join(""."");
                newobj.overridePolicy = 2;
                newobj.releaseType = 1;
                me.data.updateDialog.Open(newobj, ""版本更新"");
            }
            me.methods.customCompFunc = function (params) {
                //console.log(params);
            };

            me.methods.sortChange = function (params) {
                if (params.height.length > 0) {

                    this.mainTableConfig.t");
                WriteLiteral(@"ableData.sort(function (a, b) {

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
            me.mainLoadTablePagedDataUrl = ""/Api/SysManage/ReleaseTasks"";
            me.mainAddUrl = ""/Api/SysManage/auReleaseTask"";
            me.mainDeleteUrl = ""/Api/SysManage/deleteReleaseTask"";
            me.mainUpdateUrl = ""/Api/SysManage/auReleaseTask"";
            me.data.mainDialog = new rvAUDialog(this, true);
            me.data.updateDialog = new verUpdateDialog(this,true);
            me.mainDetailUrl = """";
            me.methods.OnCreate = function () {

                $vmpa.get(""/Api/sysconf/Regions?PageSize=100"", null, function (result) {
            ");
                WriteLiteral(@"        if (result.Status == ResultStatus.OK) {
                        me.data.regions = result.Data.DataList;
                         me.data.regions.splice(0, 0, {Id:null});
                        if (me.data.regions.length > 0) {
                           me.data.mainSearchModel.regionId = me.data.regions[0].Id;
                           me.methods.regionOnchange();
                        }
                    }
                });
                $vmpa.get(""/Api/sysconf/appTypes?PageSize=100"", null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.data.appTypes = result.Data.DataList;
                        me.data.appTypes.splice(0, 0, { Id: null });
                        if (me.data.regions.length > 0) {
                            me.data.mainSearchModel.appTypeId = me.data.appTypes[0].Id;
                            me.methods.appTypeOnchange();
                        }
                    }
                });
    ");
                WriteLiteral(@"        }
            me.methods.mainAdd = function () {
                var newmodel = { Id: -1, confFileOverride: false,overridePolicy:0,releaseType:0,dockerNetType:0,useWIP:false,selectHostPolicy:1,unzipInServer:false,count:1 ,memo:"""",Ass_Zone_Id:-1,domainName:"""",sslPem:"""",sslKey:"""",memo:"""",dockerNetType:1,useSSL:2,needRegister:false,domainName:""""};
                me.OnMainEdit(newmodel, ""新增"");
            };
            me.methods.mainEdit = function () {
                console.log(""now edit"")
                
                if (me.data.mainSelectedModel.HostIds == null) {
                    me.data.selectHostIds = [];
                }
                else {
                    
                    me.data.selectHostIds = me.data.mainSelectedModel.HostIds.split(',');
                        }
                    me.OnMainEdit(me.data.mainSelectedModel, ""编辑"");
             }
        }
        function rvAUDialog(vm, _ismain) {
            var me = this;
            me.zoneHosts = [");
                WriteLiteral(@"];
            DialogBase.call(me, vm, _ismain);
            me.editZoneOnchange = function () {

                 $vmpa.get(""/Api/sysconf/HostResources?PageSize=100&Id="" + me.EditModel.Ass_Zone_Id, null, function (result) {
                    if (result.Status == ResultStatus.OK) {
                        me.zoneHosts = result.Data.DataList;
                    }
                });
            }
            me.OnOpen = function () {
                me.editZoneOnchange();
                
            };
            me.OnSave = function (model) {
                var updateurl = """";

                var formData = new window.FormData()
                var files = document.getElementById(""importfile1"").files;
                if (files.length < 1) {
                    if (me.EditModel.Id < 0) {
                        alert(""请添加文件"");
                        return;
                    }
                } else {
                    for (var x = 0; x < files.length; x++) {
             ");
                WriteLiteral(@"           formData.append(""importfile1"", files[x]);
                    };
                }
                formData.append(""Id"", me.EditModel.Id);
                
                formData.append(""Version"", me.EditModel.Version);
                formData.append(""count"", me.EditModel.count);
               formData.append(""releaseType"", me.EditModel.releaseType);
                formData.append(""overridePolicy"", me.EditModel.overridePolicy);
                formData.append(""confFileOverride"", me.EditModel.confFileOverride);
               // formData.append(""regId"", me._vm.EditModel.regionId);
                formData.append(""zoneId"", me.EditModel.Ass_Zone_Id);
                formData.append(""serverAppId"", me.EditModel.Ass_MicroServiceApp_Id);
                formData.append(""useWIP"", me.EditModel.useWIP);
                formData.append(""selectHostPolicy"", me.EditModel.selectHostPolicy);
                formData.append(""memo"", me.EditModel.memo);
                formData.append(""HostIds"", ");
                WriteLiteral(@"me._vm.data.selectHostIds.join(','));
                formData.append(""unzipInServer"", me.EditModel.unzipInServer);
                formData.append(""domainName"", me.EditModel.domainName);
                  formData.append(""sslKey"", me.EditModel.sslKey);
                formData.append(""sslPem"", me.EditModel.sslPem);
                formData.append(""needRegister"", me.EditModel.needRegister);
                formData.append(""useSSL"", me.EditModel.useSSL);
                formData.append(""dockerNetType"", me.EditModel.dockerNetType);

                console.log(formData);

               var ret=   $.ajax({
                    url: vm.mainAddUrl,
                    type: ""POST"",
                    timeout: 1200000,
                   data: formData,
                    async: false, 
                    /**
                    *必须false才会自动加上正确的Content-Type
                    */
                    contentType: false,
                    dataType: ""json"",
                    /**
        ");
                WriteLiteral(@"            * 必须false才会避开jQuery对 formdata 的默认处理
                    * XMLHttpRequest会对 formdata 进行正确的处理
                    */
                    processData: false,
                    complete: function (xhr) {
                       
                    },
                    success: function (result) {
                        var isStandardResult = (""Status"" in result) && (""Msg"" in result);
                        if (isStandardResult) {
                            if (result.Status === ResultStatus.Failed) {
                                $vmpa.alert(result.Msg || ""操作失败"");
                                return result;
                            }
                            if (result.Status === ResultStatus.NotLogin) {
                                $vmpa.alert(result.Msg || ""未登录或登录过期，请重新登录"");
                                return result;
                            }
                            if (result.Status === ResultStatus.Unauthorized) {
                                ");
                WriteLiteral(@"$vmpa.alert(result.Msg || ""权限不足，禁止访问"");
                                return result;
                            }

                            if (result.Status === ResultStatus.OK) {
                                /* 传 result，用 result.Data 还是 result.Msg，由调用者决定 */


                                return result;
                            }
                            $vmpa.alert(""其他错误："" + result);
                            return result;
                        }

                        else {
                            return result;
                        };
                    },
                    error: function () {
                        $vmpa.alert(""上传失败！"");
                    }
                });
                
                return ret.responseJSON.Status;
            }


        }

        function verUpdateDialog(vm, _ismain) {
            var me = this;
            me.zoneHosts = [];
            DialogBase.call(me, vm, _ismain);
          
     ");
                WriteLiteral(@"       me.OnSave = function (model) {
                var updateurl = """";

                var formData = new window.FormData()
                var files = document.getElementById(""importfile2"").files;
                if (files.length < 1) {
                    if (me.EditModel.Id < 0) {
                        alert(""请添加文件"");
                        return;
                    }
                } else {
                    for (var x = 0; x < files.length; x++) {
                        formData.append(""importfile1"", files[x]);
                    };
                }
                formData.append(""Id"", me.EditModel.Id);
                
                formData.append(""Version"", me.EditModel.Version);
                formData.append(""count"", me.EditModel.count);
               formData.append(""releaseType"", me.EditModel.releaseType);
                formData.append(""overridePolicy"", me.EditModel.overridePolicy);
                formData.append(""confFileOverride"", me.EditModel.confFile");
                WriteLiteral(@"Override);
               // formData.append(""regId"", me._vm.EditModel.regionId);
                formData.append(""zoneId"", me.EditModel.Ass_Zone_Id);
                formData.append(""serverAppId"", me.EditModel.Ass_MicroServiceApp_Id);
                formData.append(""useWIP"", me.EditModel.useWIP);
                formData.append(""selectHostPolicy"", me.EditModel.selectHostPolicy);
                if (me.EditModel.memo != null) {
                    formData.append(""memo"", me.EditModel.memo);
                }
                formData.append(""HostIds"", me.EditModel.HostIds);
                formData.append(""unzipInServer"", me.EditModel.unzipInServer);
                formData.append(""domainName"", me.EditModel.domainName);
                if (me.EditModel.sslKey != null) {
                    formData.append(""sslKey"", me.EditModel.sslKey);
                }
                if (me.EditModel.sslPem != null) {
                    formData.append(""sslPem"", me.EditModel.sslPem);
                }
 ");
                WriteLiteral(@"               formData.append(""needRegister"", me.EditModel.needRegister);
                formData.append(""useSSL"", me.EditModel.useSSL);
                formData.append(""dockerNetType"", me.EditModel.dockerNetType);

                console.log(formData);

               var ret=   $.ajax({
                    url: vm.mainAddUrl,
                    type: ""POST"",
                    timeout: 1200000,
                   data: formData,
                    async: false, 
                    /**
                    *必须false才会自动加上正确的Content-Type
                    */
                    contentType: false,
                    dataType: ""json"",
                    /**
                    * 必须false才会避开jQuery对 formdata 的默认处理
                    * XMLHttpRequest会对 formdata 进行正确的处理
                    */
                    processData: false,
                    complete: function (xhr) {
                       
                    },
                    success: function (result) {
      ");
                WriteLiteral(@"                  var isStandardResult = (""Status"" in result) && (""Msg"" in result);
                        if (isStandardResult) {
                            if (result.Status === ResultStatus.Failed) {
                                $vmpa.alert(result.Msg || ""操作失败"");
                                return result;
                            }
                            if (result.Status === ResultStatus.NotLogin) {
                                $vmpa.alert(result.Msg || ""未登录或登录过期，请重新登录"");
                                return result;
                            }
                            if (result.Status === ResultStatus.Unauthorized) {
                                $vmpa.alert(result.Msg || ""权限不足，禁止访问"");
                                return result;
                            }

                            if (result.Status === ResultStatus.OK) {
                                /* 传 result，用 result.Data 还是 result.Msg，由调用者决定 */


                                return result");
                WriteLiteral(@";
                            }
                            $vmpa.alert(""其他错误："" + result);
                            return result;
                        }

                        else {
                            return result;
                        };
                    },
                    error: function () {
                        $vmpa.alert(""上传失败！"");
                    }
                });
                
                return ret.responseJSON.Status;
            }


        }
       
    </script>

");
                EndContext();
            }
            );
            BeginContext(25934, 2, true);
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
