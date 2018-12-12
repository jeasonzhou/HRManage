using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class Role:BaseEntity<int>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }

    }
}
