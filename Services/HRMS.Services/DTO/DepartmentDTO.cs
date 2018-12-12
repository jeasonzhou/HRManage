using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class DepartmentDTO : BaseDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// 状态,有效,禁用
        /// </summary>
        public bool Valid { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string Company { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
