using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AutoMapper;
using Core.Database.Repository;
using HRMS.Database;
using HRMS.Services.Interface;
using HRMS.Services.Profile;
using HRMS.Services.Service;
using HRMS.Services.UnitOfWork;

namespace HRMS.Services
{
    public class ServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HRMSDatabaseUniofwork>().As<IUnitOfWork<DataContext>>().InstancePerLifetimeScope();
            builder.RegisterType<DTOProfile>().As<AutoMapper.Profile>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AttendanceService>().As<IAttendanceService>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeEntryService>().As<IEmployeeEntryService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeDimissionService>().As<IEmployeeDimissionService>().InstancePerLifetimeScope();
            builder.RegisterType<PositionService>().As<IPositionService>().InstancePerLifetimeScope();
            builder.RegisterType<SupplierService>().As<ISupplierService>().InstancePerLifetimeScope();
            builder.RegisterType<TransCrossDepartService>().As<ITransCrossDepartService>().InstancePerLifetimeScope();
            builder.RegisterType<TransInnerDepartService>().As<ITransInnerDepartService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<RolePermissionService>().As<IRolePermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleService>().As<IUserRoleService>().InstancePerLifetimeScope();

        }
    }
}
