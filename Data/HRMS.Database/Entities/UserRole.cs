using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class UserRole:BaseEntity<int>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
