using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class SupplierDTO : BaseDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 供应商全称
        /// </summary>
        [Required]
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        [Required]
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string ShortName { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string Address { get; set; }
        /// <summary>
        /// 注册地址
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string RegisteredAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(150)]
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        [MaxLength(20)]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 法定代表
        /// </summary>
        [Required]
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string Representative { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        public DateTime Establishment { get; set; }
        /// <summary>
        /// 营业期限
        /// </summary>
        [MaxLength(10)]
        public string OperationPeriod { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        [MaxLength(20)]
        public string RegisteredCapital { get; set; }
        /// <summary>
        /// 社会信用代码
        /// </summary>
        [Required]
        [MaxLength(30)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string CreditCode { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        [MaxLength(4)]
        public string BusinessType { get; set; }

        /// <summary>
        /// 是否劳务派遣资质
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public bool IsLabour { get; set; }
        /// <summary>
        /// 保证金
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public decimal Deposit { get; set; }

        /// <summary>
        /// 状态:有效;禁用
        /// </summary>
        public bool Valid { get; set; }
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
