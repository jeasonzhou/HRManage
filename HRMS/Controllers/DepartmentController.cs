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
    public class DepartmentController : Controller
    {
        private IUserService _userService;
        private IDepartmentService _departmentService;
        private ICompanyService _companyService;

        public DepartmentController(IUserService userService,
                                    ICompanyService companyService,
                                    IDepartmentService departmentService)
        {
            _userService = userService;
            _companyService = companyService;
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
        public IActionResult DepartmentIndexData()
        {
            var result = this._departmentService.DTData(HttpContext);

           
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult DepartmentIndexCreate()
        {
            var result = this._departmentService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult DepartmentIndexUpdate()
        {
            var result = this._departmentService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult DepartmentIndexDelete()
        {
            var result = this._departmentService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        /*
        [HttpGet]
        public IActionResult CompanySelectData(string term, int page = 0)
        {
            var companies = this._companyService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                companies = companies.Where(p => (p.Name.StartsWith(term)));
            }
            var data = companies.OrderBy(o => o.Id).Skip(page * 30).Take(30).Select(s => new { id = s.Name, text = s.Name });
            Select2Model sm = new Select2Model();
            sm.incomplete_results = true;
            sm.total_count = companies.Count();
            sm.items = data.ToList();

            return Json(sm);
        }
        */
    }
}