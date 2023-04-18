﻿using Bunkering.Access.IContracts;
using Bunkering.Core.Data;
using Bunkering.Core.Utils;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Net;

namespace Bunkering.Access.Services
{
    public class ScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string User;
        ApiResponse _response;
        private int appId;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WorkFlowService _flow;

        public ScheduleService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager,
            WorkFlowService flow)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            User = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            _flow = flow;
        }

        public async Task<ApiResponse> ScheduleInspection(ScheduleViewModel model)
        {
            if (appId > 0)
            {
                try
                {
                    var app = await _unitOfWork.Application.FirstOrDefaultAsync(x => x.Id.Equals(model.ApplicationId), "Facility");
                    var user = _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Email.Equals(User));
                    if (app != null)
                    {
                        var appointment = new Appointment
                        {
                            ApplicationId = app.Id,
                            AppointmentDate = model.ScheduledDate,
                            ScheduledBy = user.Id,
                            ScheduleMessage = model.ScheduleMessage,
                            ScheduleType = model.ScheduleType,
                            ScheduleDate = DateTime.UtcNow.AddHours(1),
                            ExpiryDate = model.ScheduledDate.AddDays(48)
                        };

                        if (await _userManager.IsInRoleAsync(user, "Supervisor"))
                        {
                            appointment.IsApproved = true;
                            appointment.ApprovalMessage = model.ScheduleMessage;
                            appointment.ApprovedBy = user.Id;
                        }

                        var schFlow = await _flow.GetWorkFlow(Enum.GetName(typeof(AppActions), AppActions.ScheduleInspection), user.UserRoles.FirstOrDefault().RoleId, app.Facility.FacilityTypeId);
                        if (schFlow != null)
                        {
                            var nextUser = await _flow.GetNextStaff(appId, Enum.GetName(typeof(AppActions), AppActions.ScheduleInspection), schFlow, user);
                            if (nextUser != null && await _userManager.IsInRoleAsync(user, "Reviewer"))
                                appointment.ApprovedBy = nextUser.Id;

                            await _flow.SaveHistory(Enum.GetName(typeof(AppActions), AppActions.ScheduleInspection), appId, schFlow, user, nextUser, model.ScheduleMessage);
                        }

                        await _unitOfWork.Appointment.Add(appointment);
                        await _unitOfWork.SaveChangesAsync(user.Id);

                        _response = new ApiResponse
                        {
                            Message = await _userManager.IsInRoleAsync(user, "Supervisor")
                            ? "Inspection schedule created successfully. awaiting company's acceptance"
                            : "Inspection schedule created successfully. awaiting Manageger's approval",
                            StatusCode = HttpStatusCode.OK,
                            Success = true
                        };
                    }
                }
                catch (Exception ex)
                {
                    _response = new ApiResponse
                    {
                        Message = ex.Message,
                        StatusCode = HttpStatusCode.InternalServerError,
                        Success = false
                    };
                }
            }
            else
                _response = new ApiResponse
                {
                    Message = "ApplicationID invalid",
                    StatusCode = HttpStatusCode.BadRequest,
                    Success = false
                };
            return _response;
        }

        public async Task<ApiResponse> GetSchedule(int id)
        {
            var sch = await _unitOfWork.Appointment.FirstOrDefaultAsync(x => x.Id == id);
            if (sch != null)
                _response = new ApiResponse
                {
                    Message = "Schedule was found",
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Data = new
                    {
                        sch.ApprovedBy,
                        sch.ScheduledBy,
                        sch.IsApproved,
                        sch.ApprovalMessage,
                        InspectionDate = sch.AppointmentDate.ToString("MMM dd, yyyy HH:mm:ss"),
                        sch.ClientMessage,
                        sch.ContactName,
                        sch.IsAccepted,
                        sch.ScheduleMessage,
                        sch.ScheduleType,
                        ExpiryDate = sch.ExpiryDate.ToString("MMM dd, yyyy HH:mm:ss")
                    }
                };
            else
                _response = new ApiResponse
                {
                    Message = "Schedule not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false,
                };
            return _response;
        }

        public async Task<ApiResponse> ApproveSchedule(ScheduleViewModel model)
        {
            try
            {
                var appointment = await _unitOfWork.Appointment.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
                var user = _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Email.Equals(User));
                var app = await _unitOfWork.Application.FirstOrDefaultAsync(x => x.Id.Equals(appointment.ApplicationId), "User,Facility");
                if (appointment != null)
                {
                    var schFlow = model.Act.Equals(Enum.GetName(typeof(AppActions), AppActions.ApproveInspection))
                        ? await _flow.GetWorkFlow(Enum.GetName(typeof(AppActions), AppActions.ApproveInspection), user.UserRoles.FirstOrDefault().RoleId, appointment.Application.Facility.FacilityTypeId)
                        : await _flow.GetWorkFlow(Enum.GetName(typeof(AppActions), AppActions.RejectInspection), user.UserRoles.FirstOrDefault().RoleId, appointment.Application.Facility.FacilityTypeId);
                    if (schFlow != null)
                    {
                        var nextUser = model.Act.Equals(Enum.GetName(typeof(AppActions), AppActions.ApproveInspection))
                        ? _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Id.Equals(app.UserId))
                            : _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Id.Equals(appointment.ScheduledBy));
                        await _flow.SaveHistory(model.Act, model.ApplicationId, schFlow, user, nextUser, model.ApprovalMessage);
                    }

                    appointment.ApprovalMessage = model.ApprovalMessage;
                    if (model.Act.Equals(Enum.GetName(typeof(AppActions), AppActions.ApproveInspection)))
                        appointment.IsApproved = true;

                    await _unitOfWork.Appointment.Update(appointment);
                    await _unitOfWork.SaveChangesAsync(user.Id);

                    _response = new ApiResponse
                    {
                        Message = model.Act.Equals(Enum.GetName(typeof(AppActions), AppActions.ApproveInspection))
                        ? "Inspection schedule updated successfully. awaiting Company's approval"
                        : $"Inspection schedule  rejected - {model.ApprovalMessage}",
                        StatusCode = HttpStatusCode.OK,
                        Success = true,
                    };
                }
                else
                    _response = new ApiResponse
                    {
                        Message = "Inspection Schedule is invalid",
                        StatusCode = HttpStatusCode.NotFound,
                        Success = false
                    };
            }
            catch (Exception ex)
            {
                _response = new ApiResponse
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
            return _response;
        }

        public async Task<ApiResponse> Schedules()
        {
            var user = await _userManager.FindByEmailAsync(User);
            var schedules = await _userManager.IsInRoleAsync(user, "Company") 
                ? await _unitOfWork.Appointment.Find(x => x.Application.UserId.Equals(user.Id) && x.IsApproved && !x.IsAccepted)
                : await _unitOfWork.Appointment.Find(x => x.ScheduledBy.Equals(user.Id) || x.ApprovedBy.Equals(user.Id));
            if (schedules.Any())
                _response = new ApiResponse
                {
                    Message = "Schedules for the user was found",
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Data = schedules.Select(s => new
                    {
                        s.ApprovedBy,
                        s.ScheduledBy,
                        s.IsApproved,
                        s.ApprovalMessage,
                        InspectionDate = s.AppointmentDate.ToString("MMM dd, yyyy HH:mm:ss"),
                        s.ClientMessage,
                        s.ContactName,
                        s.IsAccepted,
                        s.ScheduleMessage,
                        s.ScheduleType,
                        ExpiryDate = s.ExpiryDate.ToString("MMM dd, yyyy HH:mm:ss")
                    })
                };
            else
                _response = new ApiResponse
                {
                    Message = "Schedules not found",
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false
                };
            return _response;
        }

        public async Task<ApiResponse> AcceptSchedule(ScheduleViewModel model)
        {
            try
            {
                var appointment = await _unitOfWork.Appointment.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
                var user = _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Email.Equals(User));
                var app = await _unitOfWork.Application.FirstOrDefaultAsync(x => x.Id.Equals(model.ApplicationId), "User,Facility");
                if (appointment != null)
                {
                    var schFlow = await _flow.GetWorkFlow(Enum.GetName(typeof(AppActions), AppActions.AcceptInspection), user.UserRoles.FirstOrDefault().RoleId, app.Facility.FacilityTypeId);
                    if (schFlow != null)
                    {
                        var nextUser = _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Id.Equals(appointment.ScheduledBy));
                        await _flow.SaveHistory(model.Act, model.ApplicationId, schFlow, user, nextUser, model.ClientMessage);
                    }

                    appointment.ClientMessage = model.ClientMessage;
                    if (model.Act.Equals(Enum.GetName(typeof(AppActions), AppActions.AcceptInspection)))
                    {
                        appointment.IsAccepted = true;
                        appointment.ContactName = model.ContactName;
                        appointment.PhoneNo = model.PhoneNo;
                    }

                    await _unitOfWork.Appointment.Update(appointment);
                    await _unitOfWork.SaveChangesAsync(user.Id);

                    _response = new ApiResponse
                    {
                        Message = model.Act.Equals(Enum.GetName(typeof(AppActions), AppActions.AcceptInspection))
                        ? "Inspection schedule accepted successfully."
                        : $"Inspection schedule  rejected - {model.ClientMessage}",
                        StatusCode = HttpStatusCode.OK,
                        Success = true
                    };
                }
                else
                    _response = new ApiResponse
                    {
                        Success = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Inspection schedule not found"
                    };
            }
            catch (Exception ex) 
            {
                _response = new ApiResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
            return _response;
        }
    }
}
