#pragma checksum "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5b805694a5c74e2936734f05210ffa04faf52852"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 4 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5b805694a5c74e2936734f05210ffa04faf52852", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"495fbe8d9d5ee75fa928b45d133cd21238862f9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<JObject>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
  
    Layout = "_homeLayout";

#line default
#line hidden
            BeginContext(81, 1149, true);
            WriteLiteral(@"<style>
    .layui-logo i, .layui-logo span {
        font-size:20px;
        color:white;
    }
    .layui-footer {
        text-align:center;
    }
        .layui-footer span {
            padding:0px 20px;
        }
</style>
<div class=""layui-layout layui-layout-admin"">
    <div class=""layui-header"">
        <div class=""layui-logo"">
            <i class=""el-icon-menu""></i>
            <span>微服务发布管理</span>
        </div>
        <!-- 头部区域（可配合layui已有的水平导航）
        <ul class=""layui-nav layui-layout-left"">
            <li class=""layui-nav-item""><a href="""">控制台</a></li>

            <li class=""layui-nav-item""><a href="""">用户</a></li>
            <li class=""layui-nav-item"">
                <a href=""javascript:;"">其它系统</a>
                <dl class=""layui-nav-child"">
                    <dd><a href="""">邮件管理</a></dd>

                </dl>
            </li>
        </ul>
            -->
        <ul class=""layui-nav layui-layout-right"">
            <li class=""layui-nav-item"">
          ");
            WriteLiteral("      <a href=\"javascript:;\">\r\n                    <img src=\"/Api/Account/Photo\" class=\"layui-nav-img\">\r\n                    ");
            EndContext();
            BeginContext(1231, 40, false);
#line 42 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
               Write(Model["loginuser"]["LoginId"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(1271, 147, true);
            WriteLiteral("\r\n                </a>\r\n               \r\n            </li>\r\n            <li class=\"layui-nav-item\"></li>\r\n            <li class=\"layui-nav-item\"><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1418, "\"", 1452, 1);
#line 47 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
WriteAttributeValue("", 1425, Url.Action("login","home"), 1425, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1453, 246, true);
            WriteLiteral(">退出</a></li>\r\n        </ul>\r\n    </div>\r\n\r\n    <div class=\"layui-side layui-bg-black\">\r\n        <div class=\"layui-side-scroll\">\r\n            <!-- 左侧导航区域（可配合layui已有的垂直导航） -->\r\n\r\n            <ul class=\"layui-nav layui-nav-tree\" lay-filter=\"test\">\r\n");
            EndContext();
#line 56 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
                  
                    foreach (var obj in Model["menus"] as JArray)
                    {
                        if (obj["IsMenu"].ToObject<bool>())
                        { 

                        }
                        else
                        {

#line default
#line hidden
            BeginContext(1984, 168, true);
            WriteLiteral("                           <li class=\"layui-nav-item layui-nav-itemed\">\r\n                            <a class=\"\" href=\"javascript:;\">\r\n\r\n                               ");
            EndContext();
            BeginContext(2153, 22, false);
#line 68 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
                          Write(obj["Name"].ToString());

#line default
#line hidden
            EndContext();
            BeginContext(2175, 124, true);
            WriteLiteral("\r\n                          </a>\r\n                            <dl class=\"layui-nav-child\">\r\n                                ");
            EndContext();
            BeginContext(2300, 59, false);
#line 71 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
                           Write(Html.Partial("subMenuPartial", obj["ChildNodes"] as JArray));

#line default
#line hidden
            EndContext();
            BeginContext(2359, 69, true);
            WriteLiteral("\r\n                            </dl>\r\n                         </li>\r\n");
            EndContext();
#line 74 "D:\document\workSpace\projects\dotnetCore\open_microService\deploySys\deploySys.Server\Views\Home\Index.cshtml"
                        }
                    }
                

#line default
#line hidden
            BeginContext(2497, 525, true);
            WriteLiteral(@"               
            </ul>
        </div>
    </div>

    <div class=""layui-tab"" lay-filter=""content"" lay-allowclose=""true"" style=""margin-left: 200px;"">
        <ul class=""layui-tab-title""></ul>
        <ul class=""rightmenu"" style=""display: none;position: absolute;"">
            <li data-type=""closethis"">关闭当前</li>
            <li data-type=""closeall"">关闭所有</li>
        </ul>
        <div class=""layui-tab-content"">
        </div>
    </div>


    <div class=""layui-footer"">
        <!-- 底部固定区域 -->
");
            EndContext();
            BeginContext(3055, 4796, true);
            WriteLiteral(@"        <span>技术支持：wangwei.njcn@gmail.com</span>    </div>
</div>
<script>

    layui.use('layer', function () {
        var layer = layui.layer;

    });
    layui.use('element', function () {
        var $ = layui.jquery
            , element = layui.element; //Tab的切换功能，切换事件监听等，需要依赖element模块

        //触发事件
        var active = {
            tabAdd: function (url, id, name) {
                //新增一个Tab项 传入三个参数，分别对应其标题，tab页面的地址，还有一个规定的id，是标签中data-id的属性值
                //关于tabAdd的方法所传入的参数可看layui的开发文档中基础方法部分
                element.tabAdd('content', {
                    title: name,
                    content: '<iframe data-frameid=""' + id + '"" scrolling=""auto"" frameborder=""0"" src=""' + url + '"" style=""width:100%;height:99%;""></iframe>',
                    // content:url,
                    id: id //规定好的id
                })
                //CustomRightClick(id); //给tab绑定右击事件

                FrameWH();  //计算ifram层的大小
            }
            , tabDelete: function (id) {
      ");
            WriteLiteral(@"          element.tabDelete(""content"", id);//删除
            }
            , tabDeleteAll: function (ids) {//删除所有
                $.each(ids, function (i, item) {
                    element.tabDelete(""content"", item); //ids是一个数组，里面存放了多个id，调用tabDelete方法分别删除
                })
            }
        };

        //当点击有site-demo-active属性的标签时，即左侧菜单栏中内容 ，触发点击事件
        $('.site-demo-active').on('click', function () {
            var dataid = $(this);

            //这时会判断右侧.layui-tab-title属性下的有lay-id属性的li的数目，即已经打开的tab项数目
            if ($("".layui-tab-title li[lay-id]"").length <= 0) {
                //如果比零小，则直接打开新的tab项
                active.tabAdd(dataid.attr(""data-url""), dataid.attr(""data-id""), dataid.attr(""data-title""));
            } else {
                //否则判断该tab项是否以及存在

                var isData = false; //初始化一个标志，为false说明未打开该tab项 为true则说明已有
                $.each($("".layui-tab-title li[lay-id]""), function () {
                    //如果点击左侧菜单栏所传入的id 在右侧tab项中的lay-id属性可以找到，则说明该tab项已经打开
 ");
            WriteLiteral(@"                   if ($(this).attr(""lay-id"") == dataid.attr(""data-id"")) {
                        isData = true;
                    }
                })
                if (isData == false) {
                    //标志为false 新增一个tab项
                    active.tabAdd(dataid.attr(""data-url""), dataid.attr(""data-id""), dataid.attr(""data-title""));
                }
            }
            //最后不管是否新增tab，最后都转到要打开的选项页面上
            element.tabChange('content', dataid.attr(""data-id""));
        });
        function CustomRightClick(id) {
            //取消右键  rightmenu属性开始是隐藏的 ，当右击的时候显示，左击的时候隐藏
            $('.layui-tab-title li').on('contextmenu', function () { return false; })
            $('.layui-tab-title,.layui-tab-title li').click(function () {
                $('.rightmenu').hide();
            });
            //桌面点击右击
            $('.layui-tab-title li').on('contextmenu', function (e) {
                var popupmenu = $("".rightmenu"");
                popupmenu.find(""li"").attr(""data-id"", id");
            WriteLiteral(@"); //在右键菜单中的标签绑定id属性

                //判断右侧菜单的位置
                l = ($(document).width() - e.clientX) < popupmenu.width() ? (e.clientX - popupmenu.width()) : e.clientX;
                t = ($(document).height() - e.clientY) < popupmenu.height() ? (e.clientY - popupmenu.height()) : e.clientY;
                popupmenu.css({ left: l, top: t }).show(); //进行绝对定位
                //alert(""右键菜单"")
                return false;
            });
        }
        $("".rightmenu li"").click(function () {

            //右键菜单中的选项被点击之后，判断type的类型，决定关闭所有还是关闭当前。
            if ($(this).attr(""data-type"") == ""closethis"") {
                //如果关闭当前，即根据显示右键菜单时所绑定的id，执行tabDelete
                active.tabDelete($(this).attr(""data-id""))
            } else if ($(this).attr(""data-type"") == ""closeall"") {
                var tabtitle = $("".layui-tab-title li"");
                var ids = new Array();
                $.each(tabtitle, function (i) {
                    ids[i] = $(this).attr(""lay-id"");
                }");
            WriteLiteral(@")
                //如果关闭所有 ，即将所有的lay-id放进数组，执行tabDeleteAll
                active.tabDeleteAll(ids);
            }

            $('.rightmenu').hide(); //最后再隐藏右键菜单
        })
        function FrameWH() {
            var h = $(window).height() - 41 - 10 - 60 - 10 - 44 - 10;
            $(""iframe"").css(""height"", h + ""px"");
        }

        $(window).resize(function () {
            FrameWH();
        })
        //Hash地址的定位
        var layid = location.hash.replace(/^#tab=/, '');
        element.tabChange('content', layid);

        element.on('tab(content)', function (elem) {
            location.hash = 'tab=' + $(this).attr('lay-id');
        });

    });
</script>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<JObject> Html { get; private set; }
    }
}
#pragma warning restore 1591
