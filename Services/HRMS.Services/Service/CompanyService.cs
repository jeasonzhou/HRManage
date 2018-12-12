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
    public class CompanyService:BaseService<Company,DataContext,CompanyDTO,int>,ICompanyService
    {
        public CompanyService(IRepository<Company,DataContext> Repository,IMapper mapper):base(Repository,mapper)
        {
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);
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

                CompanyDTO newCompany = new CompanyDTO();

                base.ConvertDictionaryToObject(newCompany, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                var existCompany = this.GetAll().Where(company => company.Name == newCompany.Name).FirstOrDefault();
                if (existCompany != null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "公司名已经存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                newCompany.CreateTime= DateTime.Now;
                newCompany.Creator = loginsession.LoginName;
                dbresult = this.Add(newCompany);
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
                CompanyDTO updateCompany, originCompany;

                updateCompany = this.GetAll().Where(c => c.Id == Convert.ToInt32(key)).FirstOrDefault();

                originCompany = (CompanyDTO)updateCompany.Clone();

                base.ConvertDictionaryToObject(updateCompany, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                if (updateCompany.Name != originCompany.Name)
                {
                    var existCompany = this.GetAll().Where(p => p.Name == updateCompany.Name).FirstOrDefault();
                    if (existCompany != null)
                    {
                        DtResponse.FieldError fe = new DtResponse.FieldError();
                        fe.name = "Name";
                        fe.status = "公司名已经存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }

                DBResult dbresult;
                dbresult = this.Update(updateCompany);

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

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="company"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CompanyAdd(CompanyDTO company, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(company, null))
            {
                message += $"公司信息为空。";
                return false;
            }
            var existCompanay = this.GetAll().Where(c => c.Name == company.Name ||
                                                         c.ShortName == company.ShortName ||
                                                         c.CreditCode == company.CreditCode).First();
            if (!ReferenceEquals(existCompanay, null))
            {
                message += $"存在相同公司名【{existCompanay.Name}】或信用代码【{existCompanay.CreditCode}】公司。";
                return false;
            }

            var companyDbResult = this.Add(company);
            if (companyDbResult.Code == 0)
            {
                message += $"公司【{company.Name}】信息添加成功。";
            }
            else
            {
                message += $"公司【{company.Name}】信息添加失败。";
                return false;
            }

            return true;
        }
        /// <summary>
        /// 启用停用操作
        /// </summary>
        /// <param name="company"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CompanyOnOff(CompanyDTO company, bool status, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(company, null))
            {
                message += $"公司信息为空。";
                return false;
            }
            if (company.Valid != status)
            {
                company.Valid = status;

                var companyDbResult = this.Update(company);
                if (companyDbResult.Code == 0)
                {
                    message += $"公司【{company.Name}】状态信息更新成功。";
                }
                else
                {
                    message += $"公司【{company.Name}】状态信息更新失败。";
                    return false;
                }
            }
            else
            {
                message += $"公司【{company.Name}】状态已为【{company.Valid}】。";
                return false;
            }

            return true;
        }

    }
}
