using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.WebServices.Model;
using HRMS.App_Start;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    public class PositionController : Controller
    {
        private IUserService _userService;
        private IDepartmentService _departmentService;
        private IPositionService _positionService;

        public PositionController(IUserService userService,
                                    IPositionService positionService,
                                    IDepartmentService departmentService)
        {
            _userService = userService;
            _positionService = positionService;
            _departmentService = departmentService;
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
        public IActionResult PositionIndexData()
        {
            var result = this._positionService.DTData(HttpContext);

            
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult PositionIndexCreate()
        {
            var result = this._positionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult PositionIndexUpdate()
        {
            var result = this._positionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult PositionIndexDelete()
        {
            var result = this._positionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        /*
        [HttpGet]
        public IActionResult DepartmentSelectData(string term, int page = 0)
        {
            var departments = this._departmentService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                departments = departments.Where(p => (p.Name.StartsWith(term)));
            }
            var data = departments.OrderBy(o => o.Id).Skip(page * 30).Take(30).Select(s => new { id = s.Name, text = s.Name });
            Select2Model sm = new Select2Model();
            sm.incomplete_results = true;
            sm.total_count = departments.Count();
            sm.items = data.ToList();

            return Json(sm);
        }
        */
    }
}