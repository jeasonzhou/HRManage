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
    public class SupplierService : BaseService<Supplier, DataContext, SupplierDTO, int>, ISupplierService
    {
        public SupplierService(IRepository<Supplier, DataContext> Repository, IMapper mapper) : base(Repository, mapper)
        {
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);
            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
             result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<SupplierDTO> supplierList = result.DtResponse.data as List<SupplierDTO>;
                if (supplierList != null)
                {

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    result.DtResponse.data = supplierList;
                    result.DtResponse.options = options;
                }
            }
            return result;
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SupplierAdd(SupplierDTO supplier, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(supplier, null))
            {
                message += $"供应商信息为空。";
                return false;
            }
            var existSupplier = this.GetAll().Where(c => c.Name == supplier.Name ||
                                                         c.ShortName == supplier.ShortName ||
                                                         c.CreditCode == supplier.CreditCode).First();
            if (!ReferenceEquals(existSupplier, null))
            {
                message += $"存在相同供应商名【{existSupplier.Name}】或信用代码【{existSupplier.CreditCode}】的供应商。";
                return false;
            }

            var supplierDbResult = this.Add(supplier);
            if (supplierDbResult.Code == 0)
            {
                message += $"供应商【{supplier.Name}】信息添加成功。";
            }
            else
            {
                message += $"供应商【{supplier.Name}】信息添加失败。";
                return false;
            }

            return true;
        }

        public bool SupplierOnOff(SupplierDTO supplier, bool status, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(supplier, null))
            {
                message += $"供应商信息为空。";
                return false;
            }
            if (supplier.Valid != status)
            {
                supplier.Valid = status;

                var supplierDbResult = this.Update(supplier);
                if (supplierDbResult.Code == 0)
                {
                    message += $"供应商【{supplier.Name}】状态信息更新成功。";
                }
                else
                {
                    message += $"供应商【{supplier.Name}】状态信息更新失败。";
                    return false;
                }
            }
            else
            {
                message += $"供应商【{supplier.Name}】状态已为【{supplier.Valid}】。";
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

                SupplierDTO newSupplier = new SupplierDTO();

                base.ConvertDictionaryToObject(newSupplier, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                var existSupplier = this.GetAll().Where(supplier => supplier.Name == newSupplier.Name).FirstOrDefault();
                if (existSupplier != null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "供应商名称已存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                newSupplier.CreateTime = DateTime.Now;
                newSupplier.Creator = loginsession.LoginName;
                dbresult = this.Add(newSupplier);
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
            UserDTO loginsession = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(core_request.HttpContext.Session.GetString("User"));

            foreach (var item in core_request.DtRequest.Data)
            {
                string key = item.Key;
                List<Dictionary<string, object>> list_pair = new List<Dictionary<string, object>>();
                var pair = item.Value as Dictionary<string, object>;

                SupplierDTO updateSupplier, originSupplier;

                updateSupplier = this.GetAll().Where(c => c.Id == Convert.ToInt32(key)).FirstOrDefault();

                originSupplier = (SupplierDTO)updateSupplier.Clone();

                base.ConvertDictionaryToObject(updateSupplier, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                if (updateSupplier.Name != originSupplier.Name)
                {
                    var existSupplier = this.GetAll().Where(p => p.Name == updateSupplier.Name).FirstOrDefault();
                    if (existSupplier != null)
                    {
                        DtResponse.FieldError fe = new DtResponse.FieldError();
                        fe.name = "Name";
                        fe.status = "供应商名称已存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }

                DBResult dbresult;
                dbresult = this.Update(updateSupplier);

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
