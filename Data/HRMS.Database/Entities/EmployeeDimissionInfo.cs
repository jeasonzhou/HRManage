using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class EmployeeDimissionInfo:BaseEntity<int>
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
        [MaxLength(150)]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 劳动合同生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }
        /// <summary>
        /// 劳动合同到期日期
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Company { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Department { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        [MaxLength(150)]
        public string Position { get; set; }
        /// <summary>
        /// 所属劳务公司
        /// </summary>
        [MaxLength(150)]
        public string LabourCompany { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        [MaxLength(500)]
        public string Reason { get; set; }
        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime DimissionTime { get; set; }
        /// <summary>
        /// 离职类型
        /// </summary>
        [MaxLength(50)]
        public string DimissionType { get; set; }
    }
}
