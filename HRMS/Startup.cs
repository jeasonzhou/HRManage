using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using HRMS.App_Start;
using HRMS.Middleware;
using HRMS.Middleware.PermissionMiddleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog;

namespace HRMS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Core.Infrastructure.Global.Configuration = this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = new PathString("/login");
                 options.AccessDeniedPath = new PathString("/denied");
             }
             );
            services.AddMvc(options =>
            {
                //设计全局错误返回
                options.Filters.Add(typeof(CustomExceptionFilterAttribute)); // an instance
            }
            ).AddJsonOptions(options =>
            {
                //设计全局JSON返回格式
                options.SerializerSettings.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                IsoDateTimeConverter timeFormate = new IsoDateTimeConverter();
                timeFormate.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.Converters.Add(timeFormate);
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;//必须包含


            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //添加redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "127.0.0.1:6379,defaultDatabase=1,password=china";
                options.InstanceName = "urn:HRMS";
            });
            //添加session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10); //session活期时间
                options.Cookie.HttpOnly = true;//设为httponly
            });
            //配置 转接头
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            //文字被编码 https://github.com/aspnet/HttpAbstractions/issues/315
            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注册NLOG，目前来说不需要
            //builder.Register(s => LogManager.GetCurrentClassLogger()).As<ILogger>().SingleInstance();

            builder.RegisterModule(new Services.ServiceModules());

            builder.RegisterModule(new AutofacModule());

            this.RegisterAutomapper(builder);

            //可以注入IServiceProvider 来自定义RESOLVE方法
        }


        private void RegisterAutomapper(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(configuration =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    configuration.AddProfile(profile);
                }
            }))
           .AsSelf()
           .SingleInstance();

            builder.Register(context => context.Resolve<MapperConfiguration>()
                .CreateMapper(context.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //配置 转接头
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");//添加自定义的ERROR显示
                app.UseStatusCodePages("text/plain", "状态码页面, 状态代码 {0}");
                app.UseHsts();
            }
            InitializeDatabase(Configuration);
            //使用会话
            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            //验证中间件
            app.UseAuthentication();
            //添加权限验证
            app.UsePermission(new PermissionMiddlewareOption()
            {
                LoginAction = @"/login",
                NoPermissionAction = @"/denied",
                MainAction = @"/home/index",
                NoMainAction = @"/home/welcome"
            });
            //添加IP访问的记录
            app.UseRequestIPMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IConfigurationRoot Configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Database.DataContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            using (var db = new Database.DataContext(optionsBuilder.Options))
            {
                db.Database.Migrate();

                var item = db.Users.Where(p => p.LoginName == "admin").FirstOrDefault();
                if (item == null)
                {
                    var model = new Database.Entities.User()
                    {
                        AliasName = "admin",
                        LoginName = "admin",
                        Password = "3e7bf1383bcdd338f3d837f3dc948f80",//admin@1
                        IsDisabled = false,
                        IsAdmin = true,
                        CreateTime = DateTime.Now,
                        Creator = "system_generate",
                    };
                    db.Users.Add(model);
                    db.SaveChanges();
                }
            }

        }
    }
}
