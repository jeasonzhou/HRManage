using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class Position:BaseEntity<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Valid { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Department { get; set; }
        /// <summary>
        /// 岗位类型
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; }

    }
}
