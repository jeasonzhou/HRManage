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
    public class PermissionService : BaseService<Permission, DataContext, PermissionDTO, int>, IPermissionService
    {
        public PermissionService(IRepository<Permission, DataContext> Repository, IMapper mapper) : base(Repository, mapper)
        {
        }
        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);
            List<PermissionDTO> list = result.DtResponse.data as List<PermissionDTO>;

            var parents = list.Select(s => s.ParentID).Distinct().ToList();
            var parentitems = this.GetAll().Where(p => parents.Contains(p.Id)).Select(s => new { value = s.Id, label = s.Name }).ToList();
            foreach (var item in list)
            {
                var parent = parentitems.FirstOrDefault(p => p.value == item.ParentID);
                if (parent != null)
                {
                    item.ParentName = parent.label;
                }
                else
                {
                    item.ParentName = item.Name;
                }
            }
            result.DtResponse.data = list;
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("ParentID", parentitems);
            result.DtResponse.options = options;

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
                PermissionDTO orgin;

                orgin = new PermissionDTO();
                base.ConvertDictionaryToObject(orgin, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;
                //登录名要唯一
                var existUser = this.GetAll().Where(p => p.Name == orgin.Name).FirstOrDefault();
                if (existUser != null)
                {
                    Core.Infrastructure.DataTables.DtResponse.FieldError fe = new Core.Infrastructure.DataTables.DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "创建的角色名已经存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                orgin.CreateTime = DateTime.Now;
                orgin.Creator = loginsession.LoginName;

                dbresult = this.Add(orgin);
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
                PermissionDTO orgin, update_compare;
                int id = int.Parse(item.Key);
                orgin = this.GetByID(id);
                update_compare = (PermissionDTO)orgin.Clone();

                base.ConvertDictionaryToObject(orgin, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;
                //检查登录名是否存在，保证唯一
                if (orgin.Name != update_compare.Name)
                {
                    var existUser = this.GetAll().Where(p => p.Name == orgin.Name).FirstOrDefault();
                    if (existUser != null)
                    {
                        Core.Infrastructure.DataTables.DtResponse.FieldError fe = new Core.Infrastructure.DataTables.DtResponse.FieldError();
                        fe.name = "Name";
                        fe.status = "更新的角色名已经存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }
                orgin.Modifier = loginsession.LoginName;
                orgin.ModifyTime = DateTime.Now;

                DBResult dbresult;
                dbresult = base.Update(orgin);

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
