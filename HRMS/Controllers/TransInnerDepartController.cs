using HRMS.App_Start;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using HRMS.Services.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
                 
    public class TransInnerDepartController : Controller
    {
        private IUserService _userService;
        private ITransInnerDepartService _transInnerDepart;

        public TransInnerDepartController(IUserService userService, ITransInnerDepartService transInnerDepartService)
        {
            _userService = userService;
            _transInnerDepart = transInnerDepartService;
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
        public IActionResult TransInnerDepartIndexData()
        {
            var result = this._transInnerDepart.DTData(HttpContext);

          
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult TransInnerDepartIndexCreate()
        {
            var result = this._transInnerDepart.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult TransInnerDepartIndexUpdate()
        {
            var result = this._transInnerDepart.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult TransInnerDepartIndexDelete()
        {
            var result = this._transInnerDepart.DTData(HttpContext);
            return Json(result.DtResponse);
        }
    }
}
