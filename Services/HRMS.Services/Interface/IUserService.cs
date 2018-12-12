using Core.WebServices.Interface;
using HRMS.Services.DTO;
using HRMS.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Services.Interface
{
    public interface IUserService : IBase<UserDTO, int>, IDatatable
    {
        UserDTO ValidateUser(string username, string password);
        UserDTO ValidateUserByName(string username);
        bool ResetPassword(int id, string newpassword);
        List<UserPermission> GetUserPermission(string username);
    }
}
