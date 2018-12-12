using AutoMapper;
using Core.Database.Repository;
using Core.WebServices.Service;
using HRMS.Database;
using HRMS.Database.Entities;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.WebServices.Model;
using Core.Infrastructure.DataTables;
using Microsoft.AspNetCore.Http;

namespace HRMS.Services.Service
{
    public class TransInnerDepartService : BaseService<PositionTransferInnerDepart, DataContext, PositionTransInnerDepartDTO, int>, ITransInnerDepartService
    {
        private IEmployeeService _EmployeeService;
        public TransInnerDepartService(IRepository<PositionTransferInnerDepart, DataContext> Repository, IMapper mapper,
                                       IEmployeeService employeeService) : base(Repository, mapper)
        {
            _EmployeeService = employeeService;
        }

        /// <summary>
        /// 调岗操作
        /// </summary>
        /// <param name="positionTrans"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        //public bool PositionTransfer(PositionTransInnerDepartDTO positionTrans,out string message)
        //{
        //    message = string.Empty;
        //    if(ReferenceEquals(positionTrans,null))
        //    {
        //        message += $"调岗信息为空。";
        //        return false;
        //    }
        //    var employee = this._EmployeeService.GetAll().Where(e => e.EmployeeId == positionTrans.EmployeeId).FirstOrDefault();
        //    if(ReferenceEquals(employee,null))
        //    {
        //        message += $"不存在该员工【{positionTrans.EmployeeId}】信息。";
        //        return false;
        //    }
        //    if (employee.EmployeeStatus == EmployeeStatus.InService)
        //    {
        //        var transferDbResult = this.Add(positionTrans);
        //        if (transferDbResult.Code == 0)
        //        {
        //            message += $"该员工【{positionTrans.EmployeeId}】调岗信息添加成功。";
        //        }
        //        else
        //        {
        //            message += $"该员工【{positionTrans.EmployeeId}】调岗信息添加失败。";
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        message += $"该员工【{positionTrans.EmployeeId}】不为在职状态。";
        //        return false;
        //    }


        //    return true;
        //}


        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);

            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
              result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<PositionTransInnerDepartDTO> TransInnerDepartList = result.DtResponse.data as List<PositionTransInnerDepartDTO>;
                if (TransInnerDepartList != null)
                {

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    result.DtResponse.data = TransInnerDepartList;
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

                PositionTransInnerDepartDTO newTransInnerDepart = new PositionTransInnerDepartDTO();

                base.ConvertDictionaryToObject(newTransInnerDepart, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

               
                DBResult dbresult;
                newTransInnerDepart.CreateTime = DateTime.Now;
                newTransInnerDepart.Creator = loginsession.LoginName;
                dbresult = this.Add(newTransInnerDepart);
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
                List<Dictionary<string, object>> list_pair = new List<Dictionary<string, object>>();
                var pair = item.Value as Dictionary<string, object>;

                PositionTransInnerDepartDTO updateTransInnerDepart, originTransInnerDepart;

                updateTransInnerDepart = this.GetAll().Where(c => c.Id == Convert.ToInt32(key)).FirstOrDefault();

                originTransInnerDepart = (PositionTransInnerDepartDTO)updateTransInnerDepart.Clone();

                base.ConvertDictionaryToObject(updateTransInnerDepart, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;


                DBResult dbresult;
               
                dbresult = this.Update(updateTransInnerDepart);
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
