using AutoMapper;
using Core.Database.Repository;
using Core.Infrastructure;
using Core.Infrastructure.DataTables;
using Core.WebServices.Model;
using Core.WebServices.Service;
using HRMS.Database;
using HRMS.Database.Entities;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace HRMS.Services.Service
{
    public class RoleService : BaseService<Role, DataContext, RoleDTO, int>, IRoleService
    {
        private IRolePermissionService _IRolePermissionService;
        private IPermissionService _IPermissionService;

        public RoleService(IRepository<Role, DataContext> Repository,
            IRolePermissionService IRolePermissionService, IPermissionService IPermissionService,
            IMapper mapper) : base(Repository, mapper)
        {
            _IRolePermissionService = IRolePermissionService;
            _IPermissionService = IPermissionService;
        }

        #region 重写datatables的方法
        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var query = (from r in this.GetAll()
                          select new RoleDTO
                          {
                              Id = r.Id,
                              Name = r.Name,
                              Remarks = r.Remarks,
                              Modifier = r.Modifier,
                              ModifyTime = r.ModifyTime,
                              Creator = r.Creator,
                              CreateTime = r.CreateTime,
                              RolePermission = (from rp in _IRolePermissionService.GetAll()
                                                   where rp.RoleId == r.Id
                                                   select rp)
                          });
            var result = base.PageDataWithQuery(core_request, query);

            var Permissionlitems = this._IPermissionService.GetAll().Select(s => new { value = s.Id, label = s.Name }).ToList();

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("RolePermission[].PermissionId", Permissionlitems);
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
                RoleDTO orgin;

                orgin = new RoleDTO();
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
                else
                {
                    Role role = dbresult.Result as Role;
                    if (orgin.RolePermission != null)
                    {
                        foreach (var v in orgin.RolePermission)
                        {
                            v.RoleId = role.Id;
                            v.CreateTime = DateTime.Now;
                            v.Creator = loginsession.LoginName;
                        }
                        dbresult = _IRolePermissionService.AddRange(orgin.RolePermission.ToArray());
                        if (dbresult.Code != 0)
                        {
                            core_response.DtResponse.error += dbresult.ErrMsg;
                        }
                    }
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
                RoleDTO orgin, update_compare;
                int id = int.Parse(item.Key);
                orgin = this.GetByID(id);
                update_compare = (RoleDTO)orgin.Clone();

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


                var rolepermissions = this._IRolePermissionService.GetAll().Where(p => p.RoleId == id).ToList();
                if (rolepermissions != null && rolepermissions.Count > 0)
                {
                    this._IRolePermissionService.DeleteRangeByEx(false, rolepermissions.ToArray());
                }
                if (orgin.RolePermission != null)
                {
                    foreach (var v in orgin.RolePermission)
                    {
                        v.RoleId = orgin.Id;
                        v.CreateTime = DateTime.Now;
                        v.Creator = loginsession.LoginName;
                    }
                }
                _IRolePermissionService.AddRangeEx(orgin.RolePermission.ToArray(), false);

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

            var rolepermissions = this._IRolePermissionService.GetAll().Where(p => ids.Contains(p.RoleId)).ToList();
            if (rolepermissions != null && rolepermissions.Count > 0)
            {
                this._IRolePermissionService.DeleteRangeByEx(false, rolepermissions.ToArray());
            }

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
        #endregion
    }
}
