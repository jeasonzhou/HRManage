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
    public class EmployeeOperationController : Controller
    {
        private IUserService _userService;
        private IDepartmentService _departmentService;
        private IEmployeeEntryService _employeeEntryService;
        private IEmployeeDimissionService _employeeDimissionService;
        public EmployeeOperationController(IUserService userService,
                                    IEmployeeEntryService employeeEntryService,
                                    IEmployeeDimissionService employeeDimissionService,
                                    IDepartmentService departmentService)
        {
            _userService = userService;
            _departmentService = departmentService;
            _employeeEntryService = employeeEntryService;
            _employeeDimissionService = employeeDimissionService;
        }

        public IActionResult EntryIndex()
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

        public IActionResult DimissionIndex()
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
        public IActionResult EmployeeEntryIndexData()
        {
            var result = this._employeeEntryService.DTData(HttpContext);

            
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeEntryIndexCreate()
        {
            var result = this._employeeEntryService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeEntryIndexUpdate()
        {
            var result = this._employeeEntryService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeEntryIndexDelete()
        {
            var result = this._employeeEntryService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeDimissionIndexData()
        {
            var result = this._employeeDimissionService.DTData(HttpContext);

           
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeDimissionIndexCreate()
        {
            var result = this._employeeDimissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeDimissionIndexUpdate()
        {
            var result = this._employeeDimissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeDimissionIndexDelete()
        {
            var result = this._employeeDimissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
    }
}