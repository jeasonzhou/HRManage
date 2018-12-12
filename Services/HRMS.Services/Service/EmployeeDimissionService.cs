using AutoMapper;
using Core.Database.Repository;
using Core.Infrastructure.DataTables;
using Core.WebServices.Model;
using Core.WebServices.Service;
using HRMS.Database;
using HRMS.Database.Entities;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRMS.Services.Service
{
    public class EmployeeDimissionService : BaseService<EmployeeDimissionInfo, DataContext, EmployeeDimissionDTO, int>, IEmployeeDimissionService
    {
        IEmployeeService _employeeService;
        public EmployeeDimissionService(IRepository<EmployeeDimissionInfo, DataContext> Repository, IMapper mapper,
                                        IEmployeeService employeeService) : base(Repository, mapper)
        {
            _employeeService = employeeService;
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);

            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
               result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<EmployeeDimissionDTO> employeeDimissionList = result.DtResponse.data as List<EmployeeDimissionDTO>;
                if (employeeDimissionList != null)
                {
                    Dictionary<string, object> options = new Dictionary<string, object>();

                    result.DtResponse.data = employeeDimissionList;
                    result.DtResponse.options = options;
                }
            }

            return result;
        }

        protected override CoreResponse Create(CoreRequest core_request)
        {
            CoreResponse core_response = new CoreResponse(core_request);
            UserDTO loginsession = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(core_request.HttpContext.Session.GetString("User"));

            foreach (var item in core_request.DtRequest.Data)
            {
                string key = item.Key;
                List<Dictionary<string, object>> list_pair = new List<Dictionary<string, object>>();
                var pair = item.Value as Dictionary<string, object>;

                EmployeeDimissionDTO newEmployeeDimission = new EmployeeDimissionDTO();

                base.ConvertDictionaryToObject(newEmployeeDimission, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;


                var existEmployee = this._employeeService.GetAll().Where(p => p.EmployeeId == newEmployeeDimission.EmployeeId).FirstOrDefault();
                if (existEmployee == null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "员工不存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }


                DBResult dbresult;

                newEmployeeDimission.EmployeeId = existEmployee.EmployeeId;
                newEmployeeDimission.EmployeeName = existEmployee.Name;
                newEmployeeDimission.Company = existEmployee.Company;
                newEmployeeDimission.Department = existEmployee.Department;
                newEmployeeDimission.Position = existEmployee.Position;
                newEmployeeDimission.LabourCompany = existEmployee.LabourCompany;
                newEmployeeDimission.DimissionTime = DateTime.Now;//离职日期
                newEmployeeDimission.CreateTime = DateTime.Now;
                newEmployeeDimission.Creator = loginsession.LoginName;
                dbresult = this.Add(newEmployeeDimission);
                if (dbresult.Code != 0)
                {
                    core_response.DtResponse.error += dbresult.ErrMsg;
                }
            }
            return core_response;
        }

        protected override CoreResponse Edit(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }

        protected override CoreResponse Remove(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }

        protected override CoreResponse Upload(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }
    }
}
