using Core.Database.Repository;
using Core.Infrastructure;
using HRMS.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Services.UnitOfWork
{
    public class HRMSDatabaseUniofwork : UnitOfWork<DataContext>
    {
        public HRMSDatabaseUniofwork(IHttpContextAccessor contentAccessor, ILoggerFactory logger)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(Global.Configuration.GetConnectionString("DefaultConnection"));
            //optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(logger);
            base.DbContext = new DataContext(optionsBuilder.Options);
        }
    }
}
