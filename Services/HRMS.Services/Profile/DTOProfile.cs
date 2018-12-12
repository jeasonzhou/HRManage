using HRMS.Database.Entities;
using HRMS.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Services.Profile
{
    public class DTOProfile : AutoMapper.Profile
    {
        public DTOProfile() : base()
        {
            CreateMap<AttendanceDTO, Attendance>().ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => ((int)src.EmployeeType).ToString()));
            CreateMap<Attendance, AttendanceDTO>().ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => (EmployeeType)Convert.ToInt32(src.EmployeeType)));

            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, CompanyDTO>();

            CreateMap<ContractDTO, Contract>();
            CreateMap<Contract, ContractDTO>();

            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentDTO>();

            CreateMap<EmployeeDTO, Employee>().ForMember(dest => dest.EmployeeStatus, opt => opt.MapFrom(src => ((int)src.EmployeeStatus).ToString()))
                                              .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => ((int)src.EmployeeType).ToString()))
                                              .ForMember(dest => dest.Marital, opt => opt.MapFrom(src => ((int)src.Marital).ToString()))
                                              .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(src => ((int)src.IdentityType).ToString()));
            //CreateMap<EmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>().ForMember(dest => dest.EmployeeStatus, opt => opt.MapFrom(src => (EmployeeStatus)Convert.ToInt32(src.EmployeeStatus)))
                                              .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => (EmployeeType)Convert.ToInt32(src.EmployeeType)))
                                              .ForMember(dest => dest.Marital, opt => opt.MapFrom(src => (MaritalStatus)Convert.ToInt32(src.Marital)))
                                              .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(src => (IdentityType)Convert.ToInt32(src.IdentityType)));
            //CreateMap<Employee, EmployeeDTO>();

            CreateMap<EmployeeDimissionDTO, EmployeeDimissionInfo>();
            CreateMap<EmployeeDimissionInfo, EmployeeDimissionDTO>();

            CreateMap<EmployeeEntryDTO, EmployeeEntryInfo>();
            CreateMap<EmployeeEntryInfo, EmployeeEntryDTO>();

            CreateMap<PermissionDTO, Permission>();
            CreateMap<Permission, PermissionDTO>();

            CreateMap<PositionDTO, Position>();
            CreateMap<Position, PositionDTO>();

            CreateMap<PositionTransCrossDepartDTO, PositionTransferCrossDepart>();
            CreateMap<PositionTransferCrossDepart, PositionTransCrossDepartDTO>();

            CreateMap<PositionTransInnerDepartDTO, PositionTransferInnerDepart>();
            CreateMap<PositionTransferInnerDepart, PositionTransInnerDepartDTO>();

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();

            CreateMap<RolePermissionDTO, RolePermission>();
            CreateMap<RolePermission, RolePermissionDTO>();

            CreateMap<SupplierDTO, Supplier>();
            CreateMap<Supplier, SupplierDTO>();

            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<UserRoleDTO, UserRole>();
            CreateMap<UserRole, UserRoleDTO>();
        }
    }
} 