using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Builder;
using Autofac.Extensions.DependencyInjection;
using NLog.Web;

namespace HRMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("启动Main");
                CreateWebHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "异常停止进程");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>().CaptureStartupErrors(true)
                .UseNLog();
    }
}
