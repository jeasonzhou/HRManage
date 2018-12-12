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
    public class DepartmentService:BaseService<Department,DataContext,DepartmentDTO,int>,IDepartmentService
    {
        private ICompanyService _companyService;
        public DepartmentService(IRepository<Department, DataContext> Repository, IMapper mapper,ICompanyService companyService) : base(Repository, mapper)
        {
            _companyService = companyService;
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);

            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
               result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<DepartmentDTO> departmentList = result.DtResponse.data as List<DepartmentDTO>;
                if (departmentList != null)
                {
                    var companies = _companyService.GetAll().Where(c => c.Valid == true).Select(company => new { value = company.Name, label = company.Name }).Distinct().ToList();

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    options.Add("Company", companies);

                    result.DtResponse.data = departmentList;
                    result.DtResponse.options = options;
                }
            }

            return result;
        }


        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="department"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DepartmentAdd(DepartmentDTO department, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(department, null))
            {
                message += $"部门信息为空。";
                return false;
            }
            var existDepartment = this.GetAll().Where(c => c.Name == department.Name &&
                                                         c.Company == department.Company).First();
            if (!ReferenceEquals(existDepartment, null))
            {
                message += $"存在相同部门名【{existDepartment.Name}】的【{existDepartment.Company}】公司。";
                return false;
            }

            var departmentDbResult = this.Update(department);
            if (departmentDbResult.Code == 0)
            {
                message += $"部门【{department.Name}】信息添加成功。";
            }
            else
            {
                message += $"部门【{department.Name}】信息添加失败。";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 启用停用操作
        /// </summary>
        /// <param name="department"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DepartmentOnOff(DepartmentDTO department, bool status, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(department, null))
            {
                message += $"部门信息为空。";
                return false;
            }
            if (department.Valid != status)
            {
                department.Valid = status;

                var departmentDbResult = this.Update(department);
                if (departmentDbResult.Code == 0)
                {
                    message += $"部门【{department.Name}】状态信息更新成功。";
                }
                else
                {
                    message += $"部门【{department.Name}】状态信息更新失败。";
                    return false;
                }
            }
            else
            {
                message += $"部门【{department.Name}】状态已为【{department.Valid}】。";
                return false;
            }

            return true;
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

                DepartmentDTO newDepartment = new DepartmentDTO();

                base.ConvertDictionaryToObject(newDepartment, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                var existDepartment = this.GetAll().Where(company => company.Name == newDepartment.Name).FirstOrDefault();
                if (existDepartment != null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "部门名已经存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                newDepartment.CreateTime = DateTime.Now;
                newDepartment.Creator = loginsession.LoginName;
                dbresult = this.Add(newDepartment);
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
                DepartmentDTO updateDepartment, originDepartment;

                updateDepartment = this.GetAll().Where(c => c.Id == Convert.ToInt32(key)).FirstOrDefault();

                originDepartment = (DepartmentDTO)updateDepartment.Clone();

                base.ConvertDictionaryToObject(updateDepartment, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                if (updateDepartment.Name != originDepartment.Name)
                {
                    var existCompany = this.GetAll().Where(p => p.Name == updateDepartment.Name).FirstOrDefault();
                    if (existCompany != null)
                    {
                        DtResponse.FieldError fe = new DtResponse.FieldError();
                        fe.name = "Name";
                        fe.status = "部门名已经存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }

                DBResult dbresult;
                dbresult = this.Update(updateDepartment);

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
    }
}
