using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Database.Entities
{
    public class Contract:BaseEntity<int>
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ContractNumber { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// 甲方名称
        /// </summary>
        [MaxLength(150)]
        public string Owner { get; set; }
        /// <summary>
        /// 乙方名称
        /// </summary>
        [MaxLength(150)]
        public string Contractor { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime ValidDate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime InvalidDate { get; set; }
        /// <summary>
        /// 预警天数
        /// </summary>
        public int AlertDays { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [MaxLength(4)]
        public string Status { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [MaxLength(150)]
        public string Department { get; set; }
        /// <summary>
        /// 基本内容
        /// </summary>
        [MaxLength(500)]
        public string Contents { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        [MaxLength(500)]
        public string Attachment { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; }
    }
}
