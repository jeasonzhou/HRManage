using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HRMS.Middleware
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestIPMiddleware> _logger;

        public RequestIPMiddleware(RequestDelegate next, ILogger<RequestIPMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var url = httpContext.Request.Path.ToString();
            if (!(url.Contains("/css") || url.Contains("/js") || url.Contains("/images") || url.Contains("/lib")))
            {
                _logger.LogInformation($"Url:{url} IP:{httpContext.Connection.RemoteIpAddress.ToString()} 时间：{DateTime.Now}");
            }
            await _next(httpContext);
        }
    }


    public static class RequestIPMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestIPMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}
