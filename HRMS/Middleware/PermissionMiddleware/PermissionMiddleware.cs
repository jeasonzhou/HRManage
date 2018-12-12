using HRMS.Services.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRMS.Middleware.PermissionMiddleware
{
    /// <summary>
    /// 权限中间件
    /// </summary>
    public class PermissionMiddleware
    {
        /// <summary>
        /// 管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 权限中间件的配置选项
        /// </summary>
        private readonly PermissionMiddlewareOption _option;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next">管道代理对象</param>
        /// <param name="permissionResitory">权限仓储对象</param>
        /// <param name="option">权限中间件配置选项</param>
        public PermissionMiddleware(RequestDelegate next, PermissionMiddlewareOption option)
        {
            _option = option;
            _next = next;
        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context, IServiceProvider provider)
        {
            //请求Url
            var questUrl = context.Request.Path.Value.ToLower();
            if (questUrl == _option.NoMainAction|| questUrl== _option.NoPermissionAction)
                return this._next(context);
            if (questUrl.StartsWith("/infra"))
                return this._next(context);
            //是否经过验证
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //用户名
                var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                Services.DTO.UserDTO m_userdto = null;
                if (string.IsNullOrEmpty(context.Session.GetString("User")))
                {
                    var m_userservice = provider.GetService(typeof(Services.Interface.IUserService)) as Services.Interface.IUserService;
                    //如果是cookie过来验证
                    m_userdto = m_userservice.ValidateUserByName(userName);
                    if (m_userdto == null)
                    {
                        //无权限跳转到拒绝页面
                        context.Response.Redirect(_option.NoPermissionAction);
                    }
                    else
                    {
                        context.Session.SetString("User", Newtonsoft.Json.JsonConvert.SerializeObject(m_userdto));
                    }
                }
                else
                {
                    m_userdto = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.DTO.UserDTO>(context.Session.GetString("User"));
                }
                if (m_userdto.IsDisabled)
                {
                    context.Response.Redirect(_option.NoPermissionAction);
                }
                else
                {
                    if (!m_userdto.IsAdmin)
                    {
                        var userPermissions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserPermission>>(context.Session.GetString("UserPermissions"));
                        if (userPermissions != null && userPermissions.Count > 0)
                        {
                            if (questUrl == "/")
                                questUrl = _option.MainAction;
                            if (userPermissions.Where(w => w.UserName == userName && w.Url.ToLower() == questUrl).Count() > 0)
                            {
                                return this._next(context);
                            }
                            else
                            {
                                if (questUrl == _option.MainAction)
                                {
                                    context.Response.Redirect(_option.NoMainAction);
                                }
                                else
                                {
                                    //无权限跳转到拒绝页面
                                    context.Response.Redirect(_option.NoPermissionAction);
                                }
                            }
                        }
                        else
                        {
                            //无权限跳转到拒绝页面
                            context.Response.Redirect(_option.NoPermissionAction);
                        }
                    }
                }
            }
            return this._next(context);
        }
    }
}
