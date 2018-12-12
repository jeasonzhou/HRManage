using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRMS.Database.Entities
{
    public class RolePermission:BaseEntity<int>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
