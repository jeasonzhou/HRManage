using Core.WebServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRMS.Services.DTO
{
    public enum IdentityType
    {
        DomesticID = 1,
        Passport,
        HongKongID,
        Other
    }
    public enum MaritalStatus
    {
        Single = 1,
        Married,
        Secrit
    }
    public enum EmployeeStatus
    {
        Unemployed = 1,
        InService,
        Dimission,
        Blacklist,
        NA
    }
    public enum EmployeeType
    {
        Normal = 1,
        Trainee,
        LongTerm,
        Temporary
    }
    public class EmployeeDTO : BaseDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(50)]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string Name { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(10)]
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(30)]
        public string IdentityNumber { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(20)]
        public string Ethnicity { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birth { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(4)]
        public string Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        [MaxLength(4)]
        public MaritalStatus Marital { get; set; }
        /// <summary>
        /// 文化程度
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(10)]
        public string Education { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        [MaxLength(50)]
        public string GraduateFrom { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduationDate { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        [MaxLength(20)]
        public string Political { get; set; }
        /// <summary>
        /// 户口所在地
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(200)]
        public string RegisteredResidence { get; set; }
        /// <summary>
        /// 常住地址
        /// </summary>
        [MaxLength(200)]
        public string Residence { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(20)]
        public string MobilePhone { get; set; }
        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(20)]
        public string EmergencyContact { get; set; }
        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        [MaxLength(20)]
        public string EmContactPhone { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [MaxLength(20)]
        public string ZipCode { get; set; }
        /// <summary>
        /// 资格证书
        /// </summary>
        [MaxLength(200)]
        public string Certificates { get; set; }
        /// <summary>
        /// 取得资格证书日期
        /// </summary>
        public DateTime? CertificateDate { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? EntryDate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? DimissionDate { get; set; }
        /// <summary>
        /// 劳动合同期限
        /// </summary>
        [MaxLength(10)]
        public string TermOfContract { get; set; }
        /// <summary>
        /// 员工状态
        /// </summary>
        [MaxLength(4)]
        public EmployeeStatus EmployeeStatus { get; set; }
        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool Valid { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        [MaxLength(4)]
        public EmployeeType EmployeeType { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string Company { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
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
        [Core.Infrastructure.DataTables.Attributes.RequireAttribute("不能为空")]
        [MaxLength(150)]
        public string LabourCompany { get; set; }
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
