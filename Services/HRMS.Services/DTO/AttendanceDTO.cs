using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class AttendanceDTO : BaseDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(50)]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string Department { get; set; }
        /// <summary>
        /// 所属岗位
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string Position { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(4)]
        public EmployeeType EmployeeType { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 打卡设备编码
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(30)]
        public string EquipmentCode { get; set; }
        /// <summary>
        /// 打卡方式
        /// </summary>
        [MaxLength(4)]
        public string CheckClass { get; set; }
        /// <summary>
        /// 打卡类型
        /// </summary>
        [MaxLength(4)]
        public string CheckType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
