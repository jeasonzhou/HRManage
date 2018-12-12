#pragma checksum "D:\HRManage\HRMS\Views\EmployeeOperation\EntryIndex.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07bf4fe027e4e0e04b742ed2c142657be7758c11"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmployeeOperation_EntryIndex), @"mvc.1.0.view", @"/Views/EmployeeOperation/EntryIndex.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/EmployeeOperation/EntryIndex.cshtml", typeof(AspNetCore.Views_EmployeeOperation_EntryIndex))]
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
#line 1 "D:\HRManage\HRMS\Views\_ViewImports.cshtml"
using HRMS.Middleware.PermissionMiddleware;

#line default
#line hidden
#line 2 "D:\HRManage\HRMS\Views\_ViewImports.cshtml"
using HRMS.App_Start;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"07bf4fe027e4e0e04b742ed2c142657be7758c11", @"/Views/EmployeeOperation/EntryIndex.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"308e485a9cf43d1ee77f9280bbe22e6601398bd4", @"/Views/_ViewImports.cshtml")]
    public class Views_EmployeeOperation_EntryIndex : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "D:\HRManage\HRMS\Views\EmployeeOperation\EntryIndex.cshtml"
  
    ViewBag.Title = @"人员入职记录";
    Layout = ViewBag.Layout;

#line default
#line hidden
            BeginContext(71, 1416, true);
            WriteLiteral(@"<div class=""page-content-body"">
    <div class=""row"">
        <div class=""col-md-12"">
            <div class=""booking-search"">
                <div class=""row form-group"">
                    <div class=""fiter col-md-6 col-xs-6"" data-column=""0"">
                        <label class=""control-label"">工号:</label>
                        <div class=""input-icon"">
                            <input class=""form-control"" type=""text"" id=""col0_filter"">
                        </div>
                    </div>
                    <div class=""fiter col-md-6 col-xs-6"" data-column=""1"">
                        <label class=""control-label"">姓名:</label>
                        <div class=""input-icon"">
                            <input class=""form-control"" type=""text"" id=""col1_filter"">
                        </div>
                    </div>
                </div>
                <div class=""row"">
                    <div class=""col-md-2"">
                        <button class=""btn green btn-block  margin-b");
            WriteLiteral(@"ottom-20"" id=""Search"">查询<i class=""m-icon-swapright m-icon-white""></i></button>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <br />
    <div class=""row"">
        <div class=""col-md-12"">
            <table id=""myTable"" class=""table table-striped table-bordered nowrap""></table>
        </div>
    </div>
    <br />
</div>
");
            EndContext();
            DefineSection("scripts", async() => {
                BeginContext(1504, 480, true);
                WriteLiteral(@"

    <script>
        function filterColumn(i) {
            $('#myTable').DataTable().column(i).search(
                $('#col' + i + '_filter').val(),
                false,
                false
            ).draw();

        }
        var editor;
        $(document).ready(function () {
            App.addResizeHandler(Resize);
            editor = new $.fn.dataTable.Editor({
                idSrc: ""Id"",
                ajax: {
                    url: """);
                EndContext();
                BeginContext(1985, 57, false);
#line 59 "D:\HRManage\HRMS\Views\EmployeeOperation\EntryIndex.cshtml"
                     Write(Url.Action("EmployeeEntryIndexData", "EmployeeOperation"));

#line default
#line hidden
                EndContext();
                BeginContext(2042, 917, true);
                WriteLiteral(@""",
                    type: ""post""
                },
                table: ""#myTable"",
                fields: [
                    { label: ""工号:"", name: ""EmployeeId"" },
                    { label: ""姓名:"", name: ""EmployeeName"" },
                    {
                        type: ""datetime"",
                        label: ""入职日期:"",
                        name: ""EntryTime""
                    },
                    {
                        type: ""datetime"",
                        label: ""劳动合同生效时间:"",
                        name: ""EffectiveDate""
                    },
                    {
                        type: ""datetime"",
                        label: ""劳动合同到期时间:"",
                        name: ""ExpirationDate""
                    }
                ]
            });
            var table = $('#myTable').DataTable({
                ajax: {
                    url: """);
                EndContext();
                BeginContext(2960, 57, false);
#line 85 "D:\HRManage\HRMS\Views\EmployeeOperation\EntryIndex.cshtml"
                     Write(Url.Action("EmployeeEntryIndexData", "EmployeeOperation"));

#line default
#line hidden
                EndContext();
                BeginContext(3017, 4850, true);
                WriteLiteral(@""",
                    type: ""post""
                },
                order: [[0, 'asc']],//一定要添加
                columns: [
                    { data: ""EmployeeId"", title: ""工号"", searchable: true, orderable: true, width: ""50px"", responsivePriority: 1 },
                    { data: ""EmployeeName"", title: ""姓名"", searchable: true, orderable: true, width: ""100px"", responsivePriority: 1 },
                    { data: ""Company"", title: ""所属公司"", searchable: true, orderable: true, width: ""100px"", responsivePriority: 1 },
                    { data: ""Department"", title: ""所属仓库/部门"", searchable: true, orderable: true, width: ""100px"", responsivePriority: 5 },
                    { data: ""Position"", title: ""所属岗位"", searchable: true, orderable: true, width: ""100px"", responsivePriority: 5 },
                    { data: ""LabourCompany"", title: ""所属劳务公司"", searchable: true, orderable: true, width: ""100px"", responsivePriority: 5 },
                    {
                        data: ""EffectiveDate"", title: ""劳动合同生效日期"", ");
                WriteLiteral(@"searchable: true, orderable: true, width: ""150px"",
                        render: function (val, type, row) {
                            if (val !== null && val.match(/\/Date\((\d+)\)\//gi)) {
                                val = moment(eval(val.replace(/\/Date\((\d+)\)\//gi, ""new Date($1)""))).format('YYYY-MM-DD HH:mm:ss');
                                row.createtime = val;
                            }
                            return val;
                        }
                    },
                    {
                        data: ""ExpirationDate"", title: ""劳动合同到期日期"", searchable: true, orderable: true, width: ""150px"",
                        render: function (val, type, row) {
                            if (val !== null && val.match(/\/Date\((\d+)\)\//gi)) {
                                val = moment(eval(val.replace(/\/Date\((\d+)\)\//gi, ""new Date($1)""))).format('YYYY-MM-DD HH:mm:ss');
                                row.createtime = val;
                            }
    ");
                WriteLiteral(@"                        return val;
                        }
                    },
                    {
                        data: ""EntryTime"", title: ""入职日期"", searchable: true, orderable: true, width: ""150px"",
                        render: function (val, type, row) {
                            if (val !== null && val.match(/\/Date\((\d+)\)\//gi)) {
                                val = moment(eval(val.replace(/\/Date\((\d+)\)\//gi, ""new Date($1)""))).format('YYYY-MM-DD HH:mm:ss');
                                row.createtime = val;
                            }
                            return val;
                        }
                    }
                ],
                buttons: [
                    { extend: ""create"", text: ""创建"", editor: editor },
                    {
                        extend: 'collection',
                        text: '导出',
                        buttons: [
                            { extend: ""copyHtml5"", text: ""复制"" },
                 ");
                WriteLiteral(@"           { extend: ""excelHtml5"", text: ""导出EXCEL"" },
                            { extend: ""csvHtml5"", text: ""导出CSV"" }
                            //{ extend: ""pdfHtml5"", text: ""导出PDF"" }
                        ]
                    }
                ]
            });

            //editor.on('initCreate', function () {
            //    editor.show(); //Shows all fields
            //});

            //editor.on('initEdit', function () {
            //    editor.show(); //Shows all fields
            //    editor.hide('password');
            //});

            editor.on('preSubmit', function (e, o, action) {
                if (action !== 'remove') {
                    ;
                }
            });

            $('#Search').click(function () {
                var items = $(this).parents('.booking-search').find('.fiter');
                $.each(items, function (index, obj) {
                    var i = $(obj).attr('data-column');
                    var val = $('#col' + i +");
                WriteLiteral(@" '_filter').val();
                    if (val === null)
                        val = '';
                    var selectot = '.search-' + i;
                    if ($(selectot).length === 0) {
                        $('#myTable').DataTable().column(i).search(val, false, false);
                    }
                    else {
                        $('#myTable').DataTable().column($(selectot)).search(val, false, false);
                    }
                });

                $('#myTable').DataTable().columns().search().draw();
            });
        });

        function Resize() {

            $('#myTable').DataTable()
                .columns.adjust()
                .responsive.recalc();
        }
    </script>
");
                EndContext();
            }
            );
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