using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.App_Start;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class CompanyController : Controller
    {
        private IUserService _userService;
        private ICompanyService _companyService;

        public CompanyController(IUserService userService,
                                  ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutDatatableAjax.cshtml";
            }
            else
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutDatatable.cshtml";
            }
            return View();
        }

        [HttpPost]
        public IActionResult CompanyIndexCreate()
        {
            var result = this._companyService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult CompanyIndexUpdate()
        {
            var result = this._companyService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult CompanyIndexDelete()
        {
            var result = this._companyService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult CompanyIndexData()
        {
            var result = this._companyService.DTData(HttpContext);

            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
                result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<CompanyDTO> companyList = result.DtResponse.data as List<CompanyDTO>;
                if (companyList != null)
                {

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    result.DtResponse.data = companyList;
                    result.DtResponse.options = options;
                }
            }
            return Json(result.DtResponse);
        }

    }
}