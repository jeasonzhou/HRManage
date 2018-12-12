using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class User:BaseEntity<int>
    {
        [MaxLength(150)]
        public string AliasName { get; set; }
        [MaxLength(150)]
        public string LoginName { get; set; }
        [MaxLength(150)]
        public string Password { get; set; }
        public bool IsDisabled { get; set; }
        public Nullable<System.DateTime> LastLoginDatetime { get; set; }
        public Nullable<int> LoginNumber { get; set; }

        public bool IsAdmin { get; set; }
    }
}
