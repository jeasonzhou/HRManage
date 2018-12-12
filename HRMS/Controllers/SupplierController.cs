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
    public class SupplierController : Controller
    {
        private IUserService _userService;

        private ISupplierService _supplierService;

        public SupplierController(IUserService userService, ISupplierService supplierService)
        {
            _userService = userService;
            _supplierService = supplierService;

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
        public IActionResult SupplierIndexData()
        {
            var result = this._supplierService.DTData(HttpContext);

         
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult SupplierIndexCreate()
        {
            var result = this._supplierService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult SupplierIndexUpdate()
        {
            var result = this._supplierService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult SupplierIndexDelete()
        {
            var result = this._supplierService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

    }
}