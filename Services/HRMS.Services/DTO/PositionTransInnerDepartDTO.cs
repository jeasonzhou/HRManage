using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public enum TransferType
    {
        Scan,
        Manual
    }
    public class PositionTransInnerDepartDTO : BaseDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 调出岗位
        /// </summary>
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string PositionOut { get; set; }
        /// <summary>
        /// 调入岗位
        /// </summary>
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string PositionIn { get; set; }
        /// <summary>
        /// 调岗时间
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public DateTime TransferTime { get; set; }
        /// <summary>
        /// 调入负责人
        /// </summary>
        [MaxLength(150)]
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        public string TransferedBy { get; set; }
        /// <summary>
        /// 调岗方式
        /// </summary>
        [MaxLength(4)]
        public string TransferType { get; set; }
    }
}
