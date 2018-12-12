using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class PermissionDTO : BaseDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 权限名字
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Control { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Action { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Area { get; set; }

        /// <summary>
        /// 排列序号
        /// </summary>
        public Nullable<int> OrderIndex { get; set; }

        /// <summary>
        /// 父权限ID
        /// </summary>
        public int? ParentID { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }


        /// <summary>
        /// 父权限ID
        /// </summary>
        public string ParentName { get; set; }
    }
}
