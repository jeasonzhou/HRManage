using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
namespace HRMS.App_Start
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider, ILogger<CustomExceptionFilterAttribute> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                // do nothing
                return;
            }
            var result = new ViewResult (){ ViewName = "Error" };

            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
            result.ViewData.Model = new Models.ErrorViewModel { RequestId = context.HttpContext.TraceIdentifier };
            result.ViewData.Add("Exception", context.Exception.Message);
            _logger.LogError("访问ID:" + context.HttpContext.TraceIdentifier + " 异常信息:" + context.Exception);
            // TODO: Pass additional detailed data via ViewData
            context.Result = result;
        }
    }
}
