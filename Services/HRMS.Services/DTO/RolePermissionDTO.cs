using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class RolePermissionDTO : BaseDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
