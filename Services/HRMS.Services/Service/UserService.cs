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
using HRMS.Services.Model;

namespace HRMS.Services.Service
{
    public class UserService : BaseService<User, DataContext, UserDTO, int>, IUserService
    {
        private IUserRoleService _IUserRoleService;
        private IRoleService _IRoleService;

        public UserService(IRepository<User, DataContext> Repository, IMapper mapper,
            IUserRoleService IUserRoleService, IRoleService IRoleService) : base(Repository, mapper)
        {
            _IUserRoleService = IUserRoleService;
            _IRoleService = IRoleService;
        }

        public UserDTO ValidateUser(string username, string password)
        {
            var result = this.GetAll().Where(p => p.LoginName == username && p.Password == password && p.IsDisabled == false).FirstOrDefault();

            //增加登录次数和修改最后登录时间
            if (result != null)
            {
                if (result.LoginNumber.HasValue)
                {
                    result.LoginNumber++;
                }
                else
                {
                    result.LoginNumber = 1;
                }
                result.LastLoginDatetime = DateTime.Now;
                this.Update(result);
            }
            return result;
        }

        public UserDTO ValidateUserByName(string username)
        {
            var result = this.GetAll().Where(p => p.LoginName == username && p.IsDisabled == false).FirstOrDefault();
            if (result != null)
            {
                if (result.LoginNumber.HasValue)
                {
                    result.LoginNumber++;
                }
                else
                {
                    result.LoginNumber = 1;
                }
                result.LastLoginDatetime = DateTime.Now;

                this.Update(result);
            }
            return result;
        }

        public bool ResetPassword(int id, string newpassword)
        {
            var item = base.GetByID(id);
            item.Password = newpassword.ToMD5String();
            var dbresult = base.Update(item);
            if (dbresult.Code != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #region 重写datatables的方法
        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var query = from u in this.GetAll()
                        select new UserDTO
                        {
                            AliasName = u.AliasName,
                            CreateTime = u.CreateTime,
                            Creator = u.Creator,
                            Id = u.Id,
                            IsAdmin = u.IsAdmin,
                            IsDisabled = u.IsDisabled,
                            LastLoginDatetime = u.LastLoginDatetime,
                            LoginName = u.LoginName,
                            LoginNumber = u.LoginNumber,
                            Modifier = u.Modifier,
                            ModifyTime = u.ModifyTime,
                            Password = u.Password,
                            UserRole = (from ur in _IUserRoleService.GetAll()
                                        where ur.UserId == u.Id
                                        select ur)
                        };
            var result = base.PageDataWithQuery(core_request, query);
            List<UserDTO> list = result.DtResponse.data as List<UserDTO>;
            if (list != null)
            {
                List<int> roleids = new List<int>();
                foreach (var user in list)
                {
                    if (user.UserRole != null)
                    {
                        roleids.AddRange(user.UserRole.Select(s => s.RoleId).ToList());
                    }
                }
                roleids = roleids.Distinct().ToList();
                var roles = this._IRoleService.GetAll().Where(p => roleids.Contains(p.Id)).Select(s => new { value = s.Id, label = s.Name }).ToList();

                foreach (var user in list)
                {
                    user.UserRole= user.UserRole.ToList();
                    if (user.UserRole != null)
                    {
                        foreach (var userrole in user.UserRole)
                        {
                            var item = roles.FirstOrDefault(p => p.value == userrole.RoleId);
                            if (item != null)
                                userrole.RoleName = item.label;
                        }
                    }
                }
                result.DtResponse.data = list;
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("UserRole[].RoleId", roles);
                result.DtResponse.options = options;
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
                UserDTO orgin;

                orgin = new UserDTO();
                base.ConvertDictionaryToObject(orgin, pair, core_response.DtResponse.fieldErrors);
                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;
                //登录名要唯一
                var existUser = this.GetAll().Where(p => p.LoginName == orgin.LoginName).FirstOrDefault();
                if (existUser != null)
                {
                    Core.Infrastructure.DataTables.DtResponse.FieldError fe = new Core.Infrastructure.DataTables.DtResponse.FieldError();
                    fe.name = "Loginname";
                    fe.status = "创建的登录名已经存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }

                DBResult dbresult;
                orgin.Password = orgin.Password.ToMD5String();
                orgin.CreateTime = DateTime.Now;
                orgin.Creator = loginsession.LoginName;
                dbresult = this.Add(orgin);
                if (dbresult.Code != 0)
                {
                    core_response.DtResponse.error += dbresult.ErrMsg;
                }
                else
                {
                    User user = dbresult.Result as User;
                    if (orgin.UserRole != null)
                    {
                        foreach (var v in orgin.UserRole)
                        {
                            v.UserId = user.Id;
                            v.CreateTime = DateTime.Now;
                            v.Creator = loginsession.LoginName;
                        }
                        dbresult = _IUserRoleService.AddRange(orgin.UserRole.ToArray());
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
                UserDTO orgin, update_compare;
                int id = int.Parse(item.Key);
                orgin = this.GetByID(id);
                update_compare = (UserDTO)orgin.Clone();

                base.ConvertDictionaryToObject(orgin, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;
                //检查登录名是否存在，保证唯一
                if (orgin.LoginName != update_compare.LoginName)
                {
                    var existUser = this.GetAll().Where(p => p.LoginName == orgin.LoginName).FirstOrDefault();
                    if (existUser != null)
                    {
                        Core.Infrastructure.DataTables.DtResponse.FieldError fe = new Core.Infrastructure.DataTables.DtResponse.FieldError();
                        fe.name = "LoginName";
                        fe.status = "更新的登录名已经存在";
                        core_response.DtResponse.fieldErrors.Add(fe);
                        return core_response;
                    }
                }
                orgin.Modifier = loginsession.LoginName;
                orgin.ModifyTime = DateTime.Now;

                var userroles = this._IUserRoleService.GetAll().Where(p => p.UserId == id).ToList();
                if (userroles != null && userroles.Count > 0)
                {
                    this._IUserRoleService.DeleteRangeByEx(false, userroles.ToArray());
                }
                if (orgin.UserRole != null)
                {
                    foreach (var v in orgin.UserRole)
                    {
                        v.UserId = orgin.Id;
                        v.CreateTime = DateTime.Now;
                        v.Creator = loginsession.LoginName;
                    }
                }
                _IUserRoleService.AddRangeEx(orgin.UserRole.ToArray(), false);

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
            var userroles = this._IUserRoleService.GetAll().Where(p => ids.Contains(p.RoleId)).ToList();
            if (userroles != null && userroles.Count > 0)
            {
                this._IUserRoleService.DeleteRangeByEx(false, userroles.ToArray());
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


        public List<UserPermission> GetUserPermission(string username)
        {
            var db = this.Repository.UnitOfWork.DbContext;
            var result = (from u in db.Users
                          join ur in db.UserRoles on u.Id equals ur.UserId
                          join rp in db.RolePermissions on ur.RoleId equals rp.RoleId
                          join p in db.Permissions on rp.PermissionId equals p.Id
                          where u.LoginName == username
                          group u by new
                          {
                              u.LoginName,
                              p.Action,
                              p.Control
                          } into gp

                          select new UserPermission
                          {
                              UserName = gp.Key.LoginName,
                              Action = gp.Key.Action,
                              Control = gp.Key.Control
                          }).ToList();
            return result;
        }
    }
}
