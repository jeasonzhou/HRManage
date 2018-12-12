using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    /// <summary>
    /// 打卡信息
    /// </summary>
    public class Attendance:BaseEntity<int>
    {
        /// <summary>
        /// 工号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Department { get; set; }
        /// <summary>
        /// 所属岗位
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Position { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string EmployeeType { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        [Required]
        public DateTime CheckTime { get; set; }
        /// <summary>
        /// 打卡设备编码
        /// </summary>
        [Required]
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
