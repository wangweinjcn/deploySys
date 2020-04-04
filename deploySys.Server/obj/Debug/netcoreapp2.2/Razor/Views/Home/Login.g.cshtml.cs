#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Login.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c0c8fd0669330bc3065c0ea0859dba58e73943ec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Login), @"mvc.1.0.view", @"/Views/Home/Login.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Login.cshtml", typeof(AspNetCore.Views_Home_Login))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c0c8fd0669330bc3065c0ea0859dba58e73943ec", @"/Views/Home/Login.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Login : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/jquery.md5.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Login.cshtml"
  
    ViewData["Title"] = "Login";

#line default
#line hidden
#line 5 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Login.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
            BeginContext(75, 42, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c0c8fd0669330bc3065c0ea0859dba58e73943ec3664", async() => {
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
            EndContext();
            BeginContext(117, 4749, true);
            WriteLiteral(@"
<style>
    .login-wrap {
        left: 0;
        top: 0;
        bottom: 0;
        right: 0;
        width: 100%;
        position: absolute;
        background: #3283AC url(../img/login/admin-login-bg.jpg) no-repeat center;
    }
    .ms-title {
        width:400px;
        margin:0px auto;
        margin-top:80px;
        font-size: 36px;
        color:white;
    }
    .ms-login {
        width: 400px;
        padding: 40px;
        border: none;
        position: absolute;
        left: 50%;
        top: 50%;
        margin-left: -200px;
        margin-top: -200px;
        background: url(../img/login/admin-loginform-bg.png) no-repeat;
    }
    .infoMess {
        display: flex;
        flex-direction: column;
        flex-wrap: wrap;
    }
    .demo-ruleForm p{
        font-size:12px;
        line-height:30px;
        color:#686868;
        text-align:center;
        margin-top:10px;
    }
</style>
<script language=""JavaScript"">

    if (window != top)
   ");
            WriteLiteral(@"     top.location.href = location.href;
    layui.use('layer', function () {
        var layer = layui.layer;

    });
</script>

<div id=""app""> 
    <template>
        <div class=""login-wrap"">
            <div class=""ms-title""></div>
            <div class=""ms-login"">
                <el-form :model=""ruleForm"" :rules=""rules"" ref=""ruleForm"" label-width=""0px"" class=""demo-ruleForm"">
                    <el-form-item prop=""LoginId"">
                        <el-input v-model=""ruleForm.LoginId"" auto-complete=""off"" placeholder=""用户名""></el-input>
                    </el-form-item>
                    <el-form-item prop=""password"">
                        <el-input type=""password"" auto-complete=""off"" placeholder=""密码"" v-model=""ruleForm.PasswordShow""></el-input>
                        <span>{{ruleForm.errorMsg}}</span>
                        <div class=""info-item infoMess"">
                            <el-input type=""password"" style=""display:none"" auto-complete=""off"" placeholder=""password"" v-model");
            WriteLiteral(@"=""ruleForm.Password""></el-input>
                        </div>
                    </el-form-item>
                    <el-checkbox v-model=""ruleForm.saveauto"">自动登陆</el-checkbox>
                    <div class=""login-btn"">
                        <el-button type=""primary"" v-on:click.native=""submitForm()"">登录</el-button>
                    </div>
                    <p style=""font-size:12px;line-height:30px;color:#686868;text-align:center;margin-top:10px""> </p>
                </el-form>
            </div>
        </div>
    </template>
</div>

<script>
    var instance = axios.create();
    var app = new Vue({
        el: '#app',
        data: function () {
            return {
                ruleForm: {
                    LoginId: '',
                    Password: '',
                    saveauto:false,
                    errorMsg: ''
                },
                rules: {
                    LoginId: [
                        { required: true, message: '请输入用户名', trigger:");
            WriteLiteral(@" 'blur' }
                    ],
                    Password: [
                        { required: true, message: '请输入密码', trigger: 'blur' }
                    ]
                }
            }
        },
        methods: {
            submitForm() {
                app.ruleForm.Password = $.md5($.trim(app.ruleForm.PasswordShow))
                $vmpa.post(""/comm/account/login"", app.ruleForm, function (res) {
                 
                    if (res.Status === ResultStatus.OK) {

                        console.log(res);
                        if (res && res.Data.access_token) {
                            var type = ''
                            if (!app.ruleForm.saveauto) {
                                type = 'sessionStorage'
                                storage.set('jwtToken', res.Data.access_token, res.Data.profile.expires_at, type)
                            } else {
                                cookie.set('jwtToken', res.Data.access_token, res.Data.profile.expir");
            WriteLiteral(@"es_at)
                            }
                            storage.set('userinfo', res.Data)
                        }
                        top.location.href  = ""/Home/Index"";

                    } else {
                        app.ruleForm.errorMsg = res.Msg
                    }
                });
             
               
            }
        }
    })
</script>

<style scoped lang=""less"">
    .ms-title {
       
    }
    .login-btn {
        text-align: center;
        margin-top: 22px;
    }

        .login-btn button {
            width: 100%;
            height: 36px;
        }
</style>


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
