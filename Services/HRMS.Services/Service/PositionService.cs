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
    public class PositionService : BaseService<Position, DataContext, PositionDTO, int>, IPositionService
    {
        private IDepartmentService _departmentService;
        public PositionService(IRepository<Position, DataContext> Repository, IMapper mapper,IDepartmentService departmentService) : base(Repository, mapper)
        {
            _departmentService = departmentService;
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);


            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
                result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<PositionDTO> positionList = result.DtResponse.data as List<PositionDTO>;
                if (positionList != null)
                {
                    var departments = _departmentService.GetAll().Where(d => d.Valid == true).Select(depart => new { value = depart.Name, label = depart.Name }).Distinct().ToList();

                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("Department", departments);

                    result.DtResponse.data = positionList;
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

                PositionDTO newPosition = new PositionDTO();

                base.ConvertDictionaryToObject(newPosition, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                var existPosition = this.GetAll().Where(p => p.Name == newPosition.Name).FirstOrDefault();
                if (existPosition != null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "岗位名已经存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                newPosition.CreateTime = DateTime.Now;
                newPosition.Creator = loginsession.LoginName;
                dbresult = this.Add(newPosition);
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
                PositionDTO updatePosition, originPosition;

                updatePosition = this.GetAll().Where(c => c.Id == Convert.ToInt32(key)).FirstOrDefault();

                originPosition = (PositionDTO)updatePosition.Clone();

                base.ConvertDictionaryToObject(updatePosition, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;

                if (updatePosition.Name != originPosition.Name)
                {
                    var existPosition = this.GetAll().Where(p => p.Name == updatePosition.Name).FirstOrDefault();
                    if (existPosition != null)
                    {
                        DtResponse.FieldError fe = new DtResponse.FieldError();
                        fe.name = "Name";
                        fe.status = "岗位名已经存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }

                DBResult dbresult;
                dbresult = this.Update(updatePosition);

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
        /// <param name="position"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool PositionAdd(PositionDTO position, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(position, null))
            {
                message += $"岗位信息为空。";
                return false;
            }
            var existPosition = this.GetAll().Where(c => c.Name == position.Name &&
                                                         c.DepartmentId == position.DepartmentId).First();
            if (!ReferenceEquals(existPosition, null))
            {
                message += $"存在相同岗位名【{existPosition.Name}】的【{existPosition.DepartmentId}】【{existPosition.Department}】部门。";
                return false;
            }

            var positionDbResult = this.Update(position);
            if (positionDbResult.Code == 0)
            {
                message += $"部门【{position.Name}】信息添加成功。";
            }
            else
            {
                message += $"部门【{position.Name}】信息添加失败。";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 启用停用操作
        /// </summary>
        /// <param name="position"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool PositionOnOff(PositionDTO position, bool status, out string message)
        {
            message = string.Empty;
            if (ReferenceEquals(position, null))
            {
                message += $"岗位信息为空。";
                return false;
            }
            if (position.Valid != status)
            {
                position.Valid = status;

                var positionDbResult = this.Update(position);
                if (positionDbResult.Code == 0)
                {
                    message += $"岗位【{position.Name}】状态信息更新成功。";
                }
                else
                {
                    message += $"岗位【{position.Name}】状态信息更新失败。";
                    return false;
                }
            }
            else
            {
                message += $"岗位【{position.Name}】状态已为【{position.Valid}】。";
                return false;
            }

            return true;
        }

    
    }
}
