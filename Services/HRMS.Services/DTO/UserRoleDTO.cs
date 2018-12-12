using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class UserRoleDTO : BaseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }

        public string RoleName { get; set; }
    }
}
