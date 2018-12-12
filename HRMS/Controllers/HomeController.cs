using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using HRMS.Middleware.PermissionMiddleware;
using HRMS.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Autofac;
using HRMS.App_Start;
using HRMS.Services.Interface;
using Core.Infrastructure;
using HRMS.Services.Model;

namespace PrivilegeManagement.Controllers
{

    public class HomeController : BaseController
    {
        readonly ILogger<HomeController> _logger;
        private readonly IComponentContext _ComponentContext;
        private IUserService _IUserService;
        public HomeController(ILogger<HomeController> logger, IComponentContext ComponentContext,
            IUserService IUserService)
        {
            _logger = logger;
            _ComponentContext = ComponentContext;
            _IUserService = IUserService;
        }
        public IActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutDatatableAjax.cshtml";
            }
            else
            {
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(HRMS.Models.LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _IUserService.ValidateUser(model.UserName, model.Password.ToMD5String());
                    if (user != null)
                    {
                        //用户标识
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Sid, model.UserName));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.AliasName));


                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                        var user_permissions = this._IUserService.GetUserPermission(model.UserName);

                        HttpContext.Session.SetString("User", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                        HttpContext.Session.SetString("UserPermissions", Newtonsoft.Json.JsonConvert.SerializeObject(user_permissions));
                        _logger.LogDebug("登录成功");
                        if (returnUrl == null)
                        {
                            returnUrl = TempData["returnUrl"]?.ToString();
                        }
                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "帐号密码错误、被禁用或者不存在");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "参数错误");
            }

            return View(model);
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            if (Request.IsAjaxRequest())
            {
                ViewBag.Layout = null;
            }
            else
            {
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            }
            return View();
        }
    }


}
