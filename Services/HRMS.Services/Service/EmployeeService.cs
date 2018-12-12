using AutoMapper;
using Core.Database.Repository;
using Core.WebServices.Model;
using Core.WebServices.Service;
using HRMS.Database;
using HRMS.Database.Entities;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Core.Infrastructure.DataTables;

namespace HRMS.Services.Service
{
    public class EmployeeService : BaseService<Employee, DataContext, EmployeeDTO, int>, IEmployeeService
    {
        private ICompanyService _companyService;
        private IDepartmentService _departmentService;
        private IPositionService _positionService;
        private ISupplierService _supplierService;
        public EmployeeService(IRepository<Employee, DataContext> Repository, IMapper mapper,
                               IDepartmentService departmentService,
                               IPositionService  positionService,
                               ISupplierService  supplierService,
                               ICompanyService companyService) : base(Repository, mapper)
        {
            _companyService = companyService;
            _departmentService = departmentService;
            _positionService = positionService;
            _supplierService = supplierService;
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);

            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
               result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<EmployeeDTO> employeeList = result.DtResponse.data as List<EmployeeDTO>;
                if (employeeList != null)
                {
                    var companies = _companyService.GetAll().Where(c => c.Valid == true).Select(company => new { value = company.Name, label = company.Name }).Distinct().ToList();
                    var departments = _departmentService.GetAll().Where(d => d.Valid == true).Select(depart => new { value = depart.Name, label = depart.Name }).Distinct().ToList();
                    var positions = _positionService.GetAll().Where(p => p.Valid == true).Select(pos => new { value = pos.Name, label = pos.Name }).Distinct().ToList();
                    var suppliers = _supplierService.GetAll().Where(s => s.Valid == true).Select(sup => new { value = sup.Name, label = sup.Name }).Distinct().ToList();

                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("Company", companies);
                    options.Add("Department", departments);
                    options.Add("Position", positions);
                    options.Add("LabourCompany", positions);

                    result.DtResponse.data = employeeList;
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

                EmployeeDTO newEmployee = new EmployeeDTO();

                base.ConvertDictionaryToObject(newEmployee, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                var existEmployee = this.GetAll().Where(p => p.Name == newEmployee.Name).FirstOrDefault();
                if (existEmployee != null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "员工名已经存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                newEmployee.CreateTime = DateTime.Now;
                newEmployee.Creator = loginsession.LoginName;
                dbresult = this.Add(newEmployee);
                if (dbresult.Code != 0)
                {
                    core_response.DtResponse.error += dbresult.ErrMsg;
                }
            }
            return core_response;
        }

        protected override CoreResponse Edit(CoreRequest core_request)
        {
            CoreResponse core_response = new CoreResponse(core_request);
            foreach (var item in core_request.DtRequest.Data)
            {
                string key = item.Key;
                List<Dictionary<string, object>> listPair = new List<Dictionary<string, object>>();
                var pair = item.Value as Dictionary<string, object>;
                EmployeeDTO updateEmployee, originEmployee;

                updateEmployee = this.GetAll().Where(c => c.Id == Convert.ToInt32(key)).FirstOrDefault();

                originEmployee = (EmployeeDTO)updateEmployee.Clone();

                base.ConvertDictionaryToObject(updateEmployee, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                if (updateEmployee.Name != originEmployee.Name)
                {
                    var existEmployee = this.GetAll().Where(p => p.Name == updateEmployee.Name).FirstOrDefault();
                    if (existEmployee != null)
                    {
                        DtResponse.FieldError fe = new DtResponse.FieldError();
                        fe.name = "Name";
                        fe.status = "员工名已经存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }

                DBResult dbresult;
                dbresult = this.Update(updateEmployee);

                if (dbresult.Code != 0)
                {
                    core_response.DtResponse.error += dbresult.ErrMsg;
                }
            }
            return core_response;
        }

        protected override CoreResponse Remove(CoreRequest core_request)
        {
            CoreResponse core_response = new CoreResponse(core_request);
            int[] ids = core_request.DtRequest.Data.Select(s => int.Parse(s.Key)).ToArray();
            var result = this.DeleteRange(ids);
            if (result.Code != 0)
            {
                core_response.DtResponse.error += result.ErrMsg;
            }
            return core_response;
        }


        protected override CoreResponse Upload(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }
        /*

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool EmployeeAdd(EmployeeDTO employee,out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(employee, null))
            {
                message += $"员工信息为空。";
                return false;
            }
            var existEmployee = this.GetAll().Where(e => e.Name == employee.Name || e.IdentityNumber == employee.IdentityNumber).First();
            if (!ReferenceEquals(existEmployee, null))
            {
                message += $"存在相同姓名【{existEmployee.Name}】或证件号码【{existEmployee.IdentityNumber}】员工。";
                return false;
            }

            var employeeDbResult = this.Add(employee);
            if (employeeDbResult.Code == 0)
            {
                message += $"员工【{employee.EmployeeId}】信息添加成功。";
            }
            else
            {
                message += $"员工【{employee.EmployeeId}】信息添加失败。";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 入职操作
        /// </summary>
        /// <param name="employeeEntry"></param>
        public bool EmployeeEntry(EmployeeEntryDTO employeeEntry,out string message)
        {
            message = string.Empty;
            if(ReferenceEquals( employeeEntry,null))
            {
                message += $"入职信息为空。";
                return false;
            }
            var employee = this.GetAll().Where(e=>e.EmployeeId ==employeeEntry.EmployeeId).FirstOrDefault();
            if (ReferenceEquals(employee, null))
            {
                message += $"找不到员工【{employeeEntry.EmployeeId}】。";
                return false;
            }
            if (employee.EmployeeStatus == EmployeeStatus.Unemployed || employee.EmployeeStatus == EmployeeStatus.Dimission)
            {
                employee.EmployeeStatus = EmployeeStatus.InService;
                employee.EntryDate = DateTime.Now;

                var unitOfWork = this.Repository.UnitOfWork;
                using (var transaction = unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var entryDbResult = this._EmployeeEntryService.Add(employeeEntry);
                    var employeeDbResult = this.Update(employee);

                    if (entryDbResult.Code == 0 && employeeDbResult.Code == 0)
                    {
                        transaction.Commit();
                        message += $"员工【{employeeEntry.EmployeeId}】信息更新成功。";
                    }
                    else
                    {
                        message += $"员工【{employeeEntry.EmployeeId}】信息更新失败。";
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                message += $"员工【{employeeEntry.EmployeeId}】状态不为【未入职】【离职】。";
                return false;
            }

           
            return true;
        }

        /// <summary>
        /// 离职操作
        /// </summary>
        /// <param name="employeeDimission"></param>
        /// <returns></returns>
        public bool EmployeeDimission(EmployeeDimissionDTO employeeDimission, out string message)
        {
            message= string.Empty;
            if (ReferenceEquals(employeeDimission, null))
            {
                message += $"离职信息为空。";
                return false;
            }
            var employee = this.GetAll().Where(e => e.EmployeeId == employeeDimission.EmployeeId).FirstOrDefault();
            if (ReferenceEquals(employee, null))
            {
                message += $"找不到员工【{employeeDimission.EmployeeId}】。";
                return false;
            }

            if (employee.EmployeeStatus == EmployeeStatus.InService)
            {
                employee.EmployeeStatus = EmployeeStatus.Dimission;
                employee.DimissionDate = DateTime.Now;

                var unitOfWork = this.Repository.UnitOfWork;
                using (var transaction = unitOfWork.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    var dimissionDbResult = this._EmployeeDimissionService.Add(employeeDimission);
                    var employeeDbResult = this.Update(employee);
                    if (dimissionDbResult.Code==0&& employeeDbResult.Code == 0)
                    {
                        transaction.Commit();
                        message += $"员工【{employeeDimission.EmployeeId}】信息更新成功。";
                    }
                    else
                    {
                        message += $"员工【{employeeDimission.EmployeeId}】信息更新失败。";
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                message += $"员工【{employeeDimission.EmployeeId}】状态不为【在职】。";
                return false;
            }

          
            return true;
        }
        /// <summary>
        /// 加入黑名单
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BlackListAdd(EmployeeDTO employee ,out string message)
        {
            message = string.Empty;
            if(ReferenceEquals(employee,null))
            {
                message += $"员工信息为空。";
                return false;
            }

            if(employee.EmployeeStatus==EmployeeStatus.Dimission)
            {
                employee.EmployeeStatus = EmployeeStatus.Blacklist;

                var employeeDbResult = this.Update(employee);
                if(employeeDbResult.Code==0)
                {
                    message += $"员工【{employee.EmployeeId}】信息更新成功。";
                }
                else
                {
                    message += $"员工【{employee.EmployeeId}】信息更新失败。";
                    return false;
                }
                
            }
            else
            {
                message += $"员工【{employee.EmployeeId}】状态不为【离职】。";
                return false;
            }


            return true;
        }
        /// <summary>
        /// 移出黑名单
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool BlackListRemove(EmployeeDTO employee,out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(employee, null))
            {
                message += $"员工信息为空。";
                return false;
            }
            if(employee.EmployeeStatus==EmployeeStatus.Blacklist)
            {
                employee.EmployeeStatus = EmployeeStatus.Dimission;

                var employeeDbResult = this.Update(employee);
                if (employeeDbResult.Code == 0)
                {
                    message += $"员工【{employee.EmployeeId}】信息更新成功。";
                }
                else
                {
                    message += $"员工【{employee.EmployeeId}】信息更新失败。";
                    return false;
                }
            }
            else
            {
                message += $"员工【{employee.EmployeeId}】状态不为【黑名单】。";
                return false;
            }

            return true;
        }
        /// <summary>
        /// 删除操作，改为失效状态
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool EmployeeRemove(EmployeeDTO employee, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(employee, null))
            {
                message += $"员工信息为空。";
                return false;
            }
            if (employee.EmployeeStatus == EmployeeStatus.InService || employee.EmployeeStatus == EmployeeStatus.Dimission)
            {
                employee.EmployeeStatus = EmployeeStatus.NA;

                var employeeDbResult = this.Update(employee);
                if (employeeDbResult.Code == 0)
                {
                    message += $"员工【{employee.EmployeeId}】信息更新成功。";
                }
                else
                {
                    message += $"员工【{employee.EmployeeId}】信息更新失败。";
                    return false;
                }
            }
            else
            {
                message += $"员工【{employee.EmployeeId}】状态不为【在职】【离职】。";
                return false;
            }

            return true;
        }
        */
     
    }
}
