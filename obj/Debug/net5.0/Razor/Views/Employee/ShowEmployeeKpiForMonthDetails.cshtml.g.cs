#pragma checksum "C:\Users\ABDUL LATEEF RAHEEM\OneDrive\Desktop\KPIMVC\KpiNew\KpiNew\Views\Employee\ShowEmployeeKpiForMonthDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6c64cc033765a940a6b28705d6d1b0ea45268bc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Employee_ShowEmployeeKpiForMonthDetails), @"mvc.1.0.view", @"/Views/Employee/ShowEmployeeKpiForMonthDetails.cshtml")]
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
#nullable restore
#line 1 "C:\Users\ABDUL LATEEF RAHEEM\OneDrive\Desktop\KPIMVC\KpiNew\KpiNew\Views\_ViewImports.cshtml"
using KpiNew;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ABDUL LATEEF RAHEEM\OneDrive\Desktop\KPIMVC\KpiNew\KpiNew\Views\_ViewImports.cshtml"
using KpiNew.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6c64cc033765a940a6b28705d6d1b0ea45268bc", @"/Views/Employee/ShowEmployeeKpiForMonthDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eca0aaccd1ee5fb6c1157e6b474131b19633247d", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Employee_ShowEmployeeKpiForMonthDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<KpiNew.Dto.EmployeeDto>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Profile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        SumTotal: \r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n       ");
#nullable restore
#line 9 "C:\Users\ABDUL LATEEF RAHEEM\OneDrive\Desktop\KPIMVC\KpiNew\KpiNew\Views\Employee\ShowEmployeeKpiForMonthDetails.cshtml"
  Write(Model.SumTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n\r\n    <dt class=\"col-sm-2\">\r\n        FullName:\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 16 "C:\Users\ABDUL LATEEF RAHEEM\OneDrive\Desktop\KPIMVC\KpiNew\KpiNew\Views\Employee\ShowEmployeeKpiForMonthDetails.cshtml"
   Write(Model.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n\r\n  \r\n\r\n\r\n</dl>\r\n\r\n<button style=\"background-color:darkblue;\"> ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c6c64cc033765a940a6b28705d6d1b0ea45268bc4467", async() => {
                WriteLiteral(" Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" </button>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<KpiNew.Dto.EmployeeDto> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
