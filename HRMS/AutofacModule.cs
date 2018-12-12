using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace HRMS
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(typeof(PrivilegeManagement.Controllers.HomeController).GetTypeInfo().Assembly)
            //                .Where(
            //                    t =>
            //                        typeof(Controller).IsAssignableFrom(t) &&
            //                        t.Name.EndsWith("Controller", StringComparison.Ordinal)).PropertiesAutowired();
        }
    }
}
