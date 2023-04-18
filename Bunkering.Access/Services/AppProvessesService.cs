using Bunkering.Access.IContracts;
using Bunkering.Core.Data;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Bunkering.Access.Services
{
    public class AppProvessesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly RoleManager<ApplicationRole> _role;
        private readonly UserManager<ApplicationUser> _userManager;
        ApiResponse _response;
        private string User;

        public AppProvessesService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor contextAccessor,
            RoleManager<ApplicationRole> role,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _role = role;
            _contextAccessor = contextAccessor;
            User = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            _userManager = userManager;
        }

        public async Task<ApiResponse> Index()
        {
            var processes = (await _unitOfWork.Workflow.GetAll()).ToList();
            var roles = await _role.Roles.ToListAsync();
            if (processes != null)
            {
                processes.ForEach(r =>
                {
                    var trole = roles.FirstOrDefault(x => x.Id.Equals(r.TriggeredByRole));
                    var rrole = roles.FirstOrDefault(x => x.Id.Equals(r.TargetRole));

                    r.TriggeredByRole = trole.Name;
                    r.TargetRole = rrole.Name;
                });
                _response = new ApiResponse
                {
                    Message = "App processes fetched successfully",
                    StatusCode = HttpStatusCode.OK,
                    Success = true,
                    Data = processes
                };
            }
            else
                _response = new ApiResponse
                {
                    Message = "No processes found",
                    StatusCode = HttpStatusCode.NotFound,
                    Success = false,
                };
            return _response;
        }

        public async Task<ApiResponse> AddFlow(WorkFlow model)
        {
            var user = await _userManager.FindByEmailAsync(User);
            await _unitOfWork.Workflow.Add(model);
            await _unitOfWork.SaveChangesAsync(user.Id);
            
            return new ApiResponse
            {
                Message = "New workflow added successfully!",
                StatusCode = HttpStatusCode.OK,
                Success = true,
            };
        }

        public async Task<ApiResponse> EditFlow(WorkFlow model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User);
                var flow = await _unitOfWork.Workflow.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (flow != null)
                {
                    flow.Rate = model.Rate;
                    flow.Status = model.Status;
                    flow.TargetRole = model.TargetRole;
                    flow.Action = model.Action;
                    flow.TriggeredByRole = model.TriggeredByRole;
                    flow.FacilityTypeId = model.FacilityTypeId;

                    await _unitOfWork.Workflow.Update(flow);
                    await _unitOfWork.SaveChangesAsync(user.Id);

                    _response = new ApiResponse
                    {
                        Message = "Workflow updated successfully",
                        StatusCode = HttpStatusCode.OK,
                        Success = true
                    };
                }
                else
                    _response = new ApiResponse
                    {
                        Message = "Work flow not found",
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

        public async Task<ApiResponse> CloneFlow(int id)
        {
            if (id == 2)
            {
                var wk = new List<WorkFlow>();
                var user = await _userManager.FindByEmailAsync(User);
                var processes = (await _unitOfWork.Workflow.Find(x => x.FacilityTypeId == 1)).ToList();
                if (processes != null)
                {
                    processes.ForEach(x =>
                    {
                        wk.Add(new WorkFlow
                        {
                            Action = x.Action,
                            FacilityTypeId = id,
                            Rate = x.Rate,
                            Status = x.Status,
                            TargetRole = x.TargetRole,
                            TriggeredByRole = x.TriggeredByRole
                        });
                    });

                    await _unitOfWork.Workflow.AddRange(wk);
                    await _unitOfWork.SaveChangesAsync(user.Id);
                }
            }
            return new ApiResponse
            {
                Message = "Clone was successful",
                StatusCode = HttpStatusCode.OK,
                Success = true
            };
        }
    }
}
