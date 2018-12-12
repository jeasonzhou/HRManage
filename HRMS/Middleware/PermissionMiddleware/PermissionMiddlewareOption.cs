using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Middleware.PermissionMiddleware
{
    /// <summary>
    /// 权限中间件选项
    /// </summary>
    public class PermissionMiddlewareOption
    {
        /// <summary>
        /// 登录action
        /// </summary>
        public string LoginAction
        { get; set; }
        /// <summary>
        /// 无权限导航action
        /// </summary>
        public string NoPermissionAction
        { get; set; }
        /// <summary>
        /// 主页
        /// </summary>
        public string MainAction
        { get; set; }
        /// <summary>
        /// 主页无权限默认跳转的页面
        /// </summary>
        public string NoMainAction
        { get; set; }

    }
}
