using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    /// <summary>
    /// 仓内调岗
    /// </summary>
    public class PositionTransferInnerDepart:BaseEntity<int>
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
        /// 调出岗位
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string PositionOut { get; set; }
        /// <summary>
        /// 调入岗位
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string PositionIn { get; set; }
        /// <summary>
        /// 调岗时间
        /// </summary>
        [Required]
        public DateTime TransferTime { get; set; }
        /// <summary>
        /// 调入负责人
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string TransferedBy { get; set; }
        /// <summary>
        /// 调岗方式
        /// </summary>
        [MaxLength(4)]
        public string TransferType { get; set; }
    }
}
