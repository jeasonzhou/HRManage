using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class Department:BaseEntity<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// 状态,有效,禁用
        /// </summary>
        public bool Valid { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Company { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
