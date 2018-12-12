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
    public class AttendanceController : Controller
    {
        private IUserService _userService;
        private IDepartmentService _departmentService;
        private IAttendanceService _attendanceService;

        public AttendanceController(IUserService userService,
                                    IAttendanceService attendanceService,
                                    IDepartmentService departmentService)
        {
            _userService = userService;
            _departmentService = departmentService;
            _attendanceService = attendanceService;
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
        public IActionResult AttendanceIndexData()
        {
            var result = this._attendanceService.DTData(HttpContext);

           
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult AttendanceIndexCreate()
        {
            var result = this._attendanceService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult AttendanceIndexUpdate()
        {
            var result = this._attendanceService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult AttendanceIndexDelete()
        {
            var result = this._attendanceService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
    }
}