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
    class EmployeeEntryService : BaseService<EmployeeEntryInfo, DataContext, EmployeeEntryDTO, int>, IEmployeeEntryService
    {
        IEmployeeService _employeeService;
        public EmployeeEntryService(IRepository<EmployeeEntryInfo, DataContext> Repository, IMapper mapper,
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
                List<EmployeeEntryDTO> employeeEntryList = result.DtResponse.data as List<EmployeeEntryDTO>;
                if (employeeEntryList != null)
                {

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    result.DtResponse.data = employeeEntryList;
                    result.DtResponse.options = options;
                }
            }

            return result;
        }

        protected override CoreResponse Create(CoreRequest core_request)
        {
            CoreResponse core_response = new CoreResponse(core_request);
            UserDTO loginSession = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(core_request.HttpContext.Session.GetString("User"));

            foreach (var item in core_request.DtRequest.Data)
            {
                string key = item.Key;
                List<Dictionary<string, object>> list_pair = new List<Dictionary<string, object>>();
                var pair = item.Value as Dictionary<string, object>;

                EmployeeEntryDTO newEmployeeEntry = new EmployeeEntryDTO();

                base.ConvertDictionaryToObject(newEmployeeEntry, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;


                var existEmployee = this._employeeService.GetAll().Where(p => p.EmployeeId == newEmployeeEntry.EmployeeId).FirstOrDefault();
                if (existEmployee == null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "员工不存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }


                DBResult dbresult;

                newEmployeeEntry.EmployeeId = existEmployee.EmployeeId;
                newEmployeeEntry.EmployeeName = existEmployee.Name;
                newEmployeeEntry.Company = existEmployee.Company;
                newEmployeeEntry.Department = existEmployee.Department;
                newEmployeeEntry.Position = existEmployee.Position;
                newEmployeeEntry.LabourCompany = existEmployee.LabourCompany;
                newEmployeeEntry.EntryTime = DateTime.Now;//入职日期
                newEmployeeEntry.CreateTime = DateTime.Now;
                newEmployeeEntry.Creator = loginSession.LoginName;
                dbresult = this.Add(newEmployeeEntry);
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
