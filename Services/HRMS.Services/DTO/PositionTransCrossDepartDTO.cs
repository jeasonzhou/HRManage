using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public class PositionTransCrossDepartDTO : BaseDTO
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
        [MaxLength(150)]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 调出部门
        /// </summary>
        [MaxLength(150)]
        public string DepartmentOut { get; set; }
        /// <summary>
        /// 调出岗位
        /// </summary>
        [MaxLength(150)]
        public string PositionOut { get; set; }
        /// <summary>
        /// 调入部门
        /// </summary>
        [MaxLength(150)]
        public string DepartmentIn { get; set; }
        /// <summary>
        /// 调入岗位
        /// </summary>
        [MaxLength(150)]
        public string PositionIn { get; set; }
        /// <summary>
        /// 调出时间
        /// </summary>
        public DateTime TransferOutTime { get; set; }
        /// <summary>
        /// 在途时长
        /// </summary>
        [MaxLength(10)]
        public string OnTheWay { get; set; }
        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime BackTime { get; set; }
        /// <summary>
        /// 调出负责人
        /// </summary>
        [MaxLength(150)]
        public string TransferedOutBy { get; set; }
        /// <summary>
        /// 调入负责人
        /// </summary>
        [MaxLength(150)]
        public string TransferedInBy { get; set; }
    }
}
