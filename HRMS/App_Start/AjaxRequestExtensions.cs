using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.App_Start
{
    public static class AjaxRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
          
            if (!((request.GetRequestInfo("X-Requested-With") == "XMLHttpRequest")))
            {
                if (request.Headers != null)
                {
                    return request.Headers["X-Requested-With"] == "XMLHttpRequest";
                }
                return false;
            }
            return true;
        }

        private static string GetRequestInfo(this HttpRequest request,string key)
        {
            
            string text = request.Query[key];
            if (text != null)
            {
                return text;
            }
            var httpCookie = request.Cookies[key];
            if (httpCookie != null)
            {
                return httpCookie.ToString();
            }
            return null;
        }
    }
}
