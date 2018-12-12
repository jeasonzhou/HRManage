using AutoMapper;
using Core.Database.Repository;
using Core.WebServices.Service;
using HRMS.Database;
using HRMS.Database.Entities;
using HRMS.Services.DTO;
using HRMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.WebServices.Model;
using Microsoft.AspNetCore.Http;
using Core.Infrastructure.DataTables;

namespace HRMS.Services.Service
{
    public class AttendanceService : BaseService<Attendance, DataContext, AttendanceDTO, int>, IAttendanceService
    {
        IEmployeeService _employeeService;
        public AttendanceService(IRepository<Attendance, DataContext> Repository, IMapper mapper,
                                 IEmployeeService employeeService) : base(Repository, mapper)
        {
            _employeeService = employeeService;
        }

        protected override CoreResponse PageData(CoreRequest core_request)
        {
            var result = base.PageData(core_request);

            if (result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesGet ||
               result.CoreRequest.DtRequest.RequestType == Core.Infrastructure.DataTables.DtRequest.RequestTypes.DataTablesSsp)
            {
                List<AttendanceDTO> attendanceList = result.DtResponse.data as List<AttendanceDTO>;
                if (attendanceList != null)
                {

                    Dictionary<string, object> options = new Dictionary<string, object>();

                    result.DtResponse.data = attendanceList;
                    result.DtResponse.options = options;
                }
            }

            return result;
        }

        protected override CoreResponse Create(CoreRequest core_request)
        {
            CoreResponse core_response = new CoreResponse(core_request);
            UserDTO loginsession = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(core_request.HttpContext.Session.GetString("User"));

            foreach (var item in core_request.DtRequest.Data)
            {
                string key = item.Key;
                List<Dictionary<string, object>> list_pair = new List<Dictionary<string, object>>();
                var pair = item.Value as Dictionary<string, object>;

                AttendanceDTO newAttendance = new AttendanceDTO();

                base.ConvertDictionaryToObject(newAttendance, pair, core_response.DtResponse.fieldErrors);

                if (core_response.DtResponse.fieldErrors != null && core_response.DtResponse.fieldErrors.Count > 0)
                    return core_response;


                var existEmployee = this._employeeService.GetAll().Where(p => p.EmployeeId == newAttendance.EmployeeId).FirstOrDefault();
                if (existEmployee == null)
                {
                    DtResponse.FieldError fe = new DtResponse.FieldError();
                    fe.name = "Name";
                    fe.status = "员工不存在";
                    core_response.DtResponse.fieldErrors.Add(fe);
                    return core_response;
                }


                DBResult dbresult;
                newAttendance.EmployeeId = existEmployee.EmployeeId;
                newAttendance.EmployeeName = existEmployee.Name;
                newAttendance.Department = existEmployee.Department;
                newAttendance.Position = existEmployee.Position;
                newAttendance.EmployeeType = existEmployee.EmployeeType;
                newAttendance.EquipmentCode = "0";

                newAttendance.CreateTime = DateTime.Now;
                newAttendance.Creator = loginsession.LoginName;
                dbresult = this.Add(newAttendance);
                if (dbresult.Code != 0)
                {
                    core_response.DtResponse.error += dbresult.ErrMsg;
                }
            }
            return core_response;
        }

        protected override CoreResponse Edit(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }

        protected override CoreResponse Remove(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }

        protected override CoreResponse Upload(CoreRequest core_request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 打卡操作
        /// </summary>
        /// <param name="attendance"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        //public bool CheckInOrOut(AttendanceDTO attendance,out string message)
        //{
        //    message = string.Empty;
        //    if(ReferenceEquals(attendance,null))
        //    {
        //        message += $"打卡信息为空。";
        //        return false;
        //    }
        //    var employee = this._employeeService.GetAll().Where(e => e.EmployeeId == attendance.EmployeeId).FirstOrDefault();
        //    if(ReferenceEquals(employee,null))
        //    {
        //        message += $"不存在该员工【{attendance.EmployeeId}】信息。";
        //        return false;
        //    }
        //    if(employee.EmployeeStatus==EmployeeStatus.InService)
        //    {
        //        var attendanceDbResult = this.Add(attendance);
        //        if(attendanceDbResult.Code==0)
        //        {
        //            message += $"该员工【{attendance.EmployeeId}】打卡信息添加成功。";
        //        }
        //        else
        //        {
        //            message += $"该员工【{attendance.EmployeeId}】打卡信息添加失败。";
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        message += $"该员工【{attendance.EmployeeId}】不为在职状态。";
        //        return false;
        //    }

        //    return true;
        //}
        /// <summary>
        /// 补卡操作
        /// </summary>
        /// <param name="attendance"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Mend(AttendanceDTO attendance,out string message)
        {
            message = string.Empty;

            return true;
        }

      
    }
}
