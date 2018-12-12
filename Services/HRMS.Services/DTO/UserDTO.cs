using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class UserDTO: BaseDTO
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string AliasName { get; set; }
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string LoginName { get; set; }
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string Password { get; set; }
        public bool IsDisabled { get; set; }
        public Nullable<System.DateTime> LastLoginDatetime { get; set; }
        public Nullable<int> LoginNumber { get; set; }

        public bool IsAdmin { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }

        public IEnumerable<UserRoleDTO> UserRole { get; set; }


    }
}
