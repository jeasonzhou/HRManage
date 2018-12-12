using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using PrivilegeManagement.Controllers;

namespace HRMS.Controllers
{
    public class InfraController : BaseController
    {
        private IUserService _IUserService;
        private IRoleService _IRoleService;
        private IPermissionService _IPermissionService;
        private IRolePermissionService _IRolePermissionService;
        private ICompanyService _ICompanyService;
        private IDepartmentService _IDepartmentService;
        private IPositionService _IPositionService;
        private IEmployeeService _IEmployeeService;
        private ISupplierService _ISupplierService;
        public InfraController(IUserService IUserService, 
                               IRoleService IRoleService, 
                               IPermissionService IPermissionService, 
                               IRolePermissionService IRolePermissionService,
                               IDepartmentService IDepartmentService,
                               IPositionService IPositionService,
                               IEmployeeService IEmployeeService,
                               ISupplierService ISupplierService,
                               ICompanyService ICompanyService)
        {
            _IUserService = IUserService;
            _IRoleService = IRoleService;
            _IPermissionService = IPermissionService;
            _IRolePermissionService = IRolePermissionService;
            _ICompanyService = ICompanyService;
            _IDepartmentService = IDepartmentService;
            _IPositionService = IPositionService;
            _IEmployeeService = IEmployeeService;
            _ISupplierService = ISupplierService;
        }

        public IActionResult UserRoleInfraData(string term, string value, int page = 0)
        {
            var query = this._IRoleService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))
                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }

        public ActionResult PermissionTreeData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var all_permissions = this._IPermissionService.GetAll().ToList();
                List<Core.WebServices.Model.JSTreeModel> jstree = new List<Core.WebServices.Model.JSTreeModel>();
                if (id == "#")
                {
                    foreach (var p in all_permissions)
                    {
                        string parentid = "#";
                        if (p.ParentID.HasValue)
                        {
                            parentid = p.ParentID.ToString();
                        }
                        jstree.Add(new Core.WebServices.Model.JSTreeModel()
                        {
                            id = p.Id.ToString(),
                            parent = parentid,
                            text = p.Name,
                            icon = "fa fa-globe icon-lg",
                            children = false,
                            state = new Core.WebServices.Model.JSTreeState { selected = false }
                        });
                    }
                }
                return Json(jstree);
            }
            throw new ArgumentNullException();
        }

        public IActionResult PermissionInfraData(string term, string value, int page = 0)
        {
            var query = this._IPermissionService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))
                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }

        #region 人员档案

        public IActionResult CompanyInfraData(string term, string value, int page = 0)
        {
            var query = this._ICompanyService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))
                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }

        public IActionResult DepartmentInfraData(string term, string value, int page = 0)
        {
            var query = this._IDepartmentService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))

                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }

        public IActionResult PositionInfraData(string term, string value, int page = 0)
        {
            var query = this._IPositionService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))
                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }

        public IActionResult SupplierInfraData(string term, string value, int page = 0)
        {
            var query = this._ISupplierService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))
                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }

        public IActionResult EmployeeInfraData(string term, string value, int page = 0)
        {
            var query = this._IEmployeeService.GetAll();
            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(p => p.Name.Contains(term));
            }
            if (!string.IsNullOrEmpty(value))
            {
                int m_value = 0;
                if (int.TryParse(value, out m_value))
                {
                    query = query.Where(p => p.Id == m_value);
                }
            }
            var data = query.OrderBy(o => o.Name).Skip(page * 30).Take(30).Select(s => new { id = s.Id, text = s.Name });
            Core.WebServices.Model.Select2Model sm = new Core.WebServices.Model.Select2Model();
            sm.incomplete_results = true;
            sm.total_count = query.Count();
            sm.items = data.ToList();
            return Json(sm);
        }
        #endregion
    }
}