using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMS.Database;
using HRMS.Database.Entities;
using HRMS.App_Start;
using HRMS.Services.Interface;
using HRMS.Services.DTO;
using Core.WebServices.Model;

namespace HRMS.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;
        private IUserService _userService;
        private ICompanyService _companyService;
        private IDepartmentService _departmentService;
        private IPositionService _positionService;
        private ISupplierService _supplierService;

        public EmployeeController(IUserService userService,
                                  IEmployeeService employeeService,
                                  ICompanyService companyService,
                                  IDepartmentService departmentService,
                                  IPositionService positionService,
                                  ISupplierService supplierService)
        {
            _userService = userService;
            _employeeService = employeeService;
            _companyService = companyService;
            _departmentService = departmentService;
            _positionService = positionService;
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
        public IActionResult EmployeeIndexData()
        {
            var result = this._employeeService.DTData(HttpContext);

           
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeIndexCreate()
        {
            var result = this._employeeService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeIndexUpdate()
        {
            var result = this._employeeService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult EmployeeIndexDelete()
        {
            var result = this._employeeService.DTData(HttpContext);
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

        [HttpGet]
        public IActionResult PositionSelectData(string term, int page = 0)
        {
            var positions = this._positionService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                positions = positions.Where(p => (p.Name.StartsWith(term)));
            }
            var data = positions.OrderBy(o => o.Id).Skip(page * 30).Take(30).Select(s => new { id = s.Name, text = s.Name });
            Select2Model sm = new Select2Model();
            sm.incomplete_results = true;
            sm.total_count = positions.Count();
            sm.items = data.ToList();

            return Json(sm);
        }

        [HttpGet]
        public IActionResult EmployeeSelectData(string term, int page = 0)
        {
            var employees = this._employeeService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                employees = employees.Where(p => (p.Name.StartsWith(term)));
            }
            var data = employees.OrderBy(o => o.Id).Skip(page * 30).Take(30).Select(s => new { id = s.Name, text = s.Name });
            Select2Model sm = new Select2Model();
            sm.incomplete_results = true;
            sm.total_count = employees.Count();
            sm.items = data.ToList();

            return Json(sm);
        }

        [HttpGet]
        public IActionResult SupplierSelectData(string term, int page = 0)
        {
            var suppliers = this._supplierService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                suppliers = suppliers.Where(p => (p.Name.StartsWith(term)));
            }
            var data = suppliers.OrderBy(o => o.Id).Skip(page * 30).Take(30).Select(s => new { id = s.Name, text = s.Name });
            Select2Model sm = new Select2Model();
            sm.incomplete_results = true;
            sm.total_count = suppliers.Count();
            sm.items = data.ToList();

            return Json(sm);
        }
        */

    }
}
