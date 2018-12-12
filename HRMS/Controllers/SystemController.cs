using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.App_Start;
using HRMS.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using PrivilegeManagement.Controllers;

namespace HRMS.Controllers
{
    public class SystemController : Controller
    {
        private IUserService _IUserService;
        private IRoleService _IRoleService;
        private IPermissionService _IPermissionService;
        private IRolePermissionService _IRolePermissionService;
        public SystemController(IUserService IUserService, IRoleService IRoleService
            , IPermissionService IPermissionService, IRolePermissionService IRolePermissionService)
        {
            _IUserService = IUserService;
            _IRoleService = IRoleService;
            _IPermissionService = IPermissionService;
            _IRolePermissionService = IRolePermissionService;
        }

        #region 用户

        public IActionResult UserIndex()
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
        public IActionResult UserIndexData()
        {
            var result = this._IUserService.DTData(HttpContext);

            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult UserIndexCreate()
        {
            var result = this._IUserService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult UserIndexUpdate()
        {
            var result = this._IUserService.DTData(HttpContext);
            return Json(result.DtResponse);
        }

        [HttpPost]
        public IActionResult UserIndexDelete()
        {
            var result = this._IUserService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        #endregion

        #region 角色
        public IActionResult RoleIndex()
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
        public IActionResult RoleIndexData()
        {
            var result = this._IRoleService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult RoleIndexCreate()
        {
            var result = this._IRoleService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult RoleIndexUpdate()
        {
            var result = this._IRoleService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult RoleIndexDelete()
        {
            var result = this._IRoleService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        #endregion

        public IActionResult PermissionIndex()
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
        public IActionResult PermissionIndexData()
        {
            var result = this._IPermissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult PermissionIndexCreate()
        {
            var result = this._IPermissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult PermissionIndexUpdate()
        {
            var result = this._IPermissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
        [HttpPost]
        public IActionResult PermissionIndexDelete()
        {
            var result = this._IPermissionService.DTData(HttpContext);
            return Json(result.DtResponse);
        }
    }
}