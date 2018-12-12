using HRMS.Services.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.App_Start
{
    public static class PermssionHelper
    {
        public static bool HasPermission(this HttpContext context, string url)
        {
            url = url.ToLower();
            var m_userdto = Newtonsoft.Json.JsonConvert.DeserializeObject<Services.DTO.UserDTO>(context.Session.GetString("User"));
            if (m_userdto.IsAdmin)
                return true;
            var userPermissions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserPermission>>(context.Session.GetString("UserPermissions"));
            if (userPermissions.Where(w => w.Url.ToLower() == url).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
