﻿using Bunkering.Core.Data;
using Bunkering.Access;
using Bunkering.Core.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text;
using Bunkering.Access.IContracts;
using Bunkering.Access;
using Bunkering.Core.ViewModels;

namespace Bunkering.Access.Services
{
    public class WorkFlowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _env;
        private readonly MailSettings _mailSetting;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSetting _appSetting;
        //private readonly string directory = "WorkFlow";
        //private readonly GeneralLogger _generalLogger;

        public WorkFlowService(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment env,
            IOptions<MailSettings> mailSettings,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSetting> appSetting)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _env = env;
            _mailSetting = mailSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _appSetting = appSetting.Value;
        }

        public async Task<(bool, string)> AppWorkFlow(int appid, string action, string comment, string currUserId = null, string delUserId = null)
        {
            var res = false;
            ApplicationUser nextprocessingofficer = null;
            var wkflow = new WorkFlow();
            try
            {
                var app = await _unitOfWork.Application.FirstOrDefaultAsync(x => x.Id == appid, "User,Facility.FacilityType,Payments");
                currUserId = string.IsNullOrEmpty(currUserId) ? app.CurrentDeskId : currUserId;
                var currentUser = _userManager.Users
                    .Include(x => x.Company)
                    .Include(ur => ur.UserRoles)
                    .ThenInclude(r => r.Role)
                    .FirstOrDefault(x => x.Id.Equals(currUserId));
                var currentuserRoles = currentUser.UserRoles.Where(x => !x.Role.Name.Equals("Staff")).FirstOrDefault().Role.Id;

                if (currentUser != null)
                {
                    wkflow = await GetWorkFlow(action, currentuserRoles, app.Facility.FacilityTypeId);
                    if(wkflow != null) //get next processing staff
                    {
                        if (action.ToLower().Equals("reject") && currentUser.UserRoles.FirstOrDefault().Role.Name.ToLower().Equals("fad"))
                            nextprocessingofficer = app.User;
                        else if (action.ToLower().Equals("approve") && currentUser.UserRoles.FirstOrDefault().Role.Name.ToLower().Equals("fad"))
                            nextprocessingofficer = _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role).FirstOrDefault(x => x.Id.Equals(app.CurrentDeskId));
                        else
                            nextprocessingofficer = await GetNextStaff(appid, action, wkflow, currentUser, delUserId);
                    }
                    if (nextprocessingofficer != null)
                    {
                        //update application
                        app.CurrentDeskId = nextprocessingofficer.Id;
                        app.Status = wkflow.Status;
                        app.FlowId = wkflow.Id;
                        app.ModifiedDate = DateTime.Now.AddHours(1);

                        if (action.Equals(Enum.GetName(typeof(AppActions), AppActions.Submit)))
                        {
                            app.SubmittedDate = DateTime.Now.AddHours(1);
                            var fadusers = await _userManager.GetUsersInRoleAsync("FAD");
                            if(fadusers.Count > 0)
                            {
                                var fadStaff = fadusers.Where(x => x.IsActive).OrderBy(x => x.LastJobDate).FirstOrDefault();
                                if (fadStaff != null)
                                {
                                    app.FADStaffId = fadStaff.Id;
                                    fadStaff.LastJobDate = DateTime.UtcNow.AddHours(1);
                                    await _userManager.UpdateAsync(fadStaff);
                                }
                            }
                        }
                        //else if(action.ToLower().Equals("resubmit") && !app.FADApproved)
                        //{
                        //    var prevFadSTaff = await _unitOfWork.ApplicationHistory.Find(x => x.Action.ToLower().Equals())
                        //}

                        //on approval by FAD or Bunkering Reviewer
                        if (action.Equals(Enum.GetName(typeof(AppActions), AppActions.Approve)))
                        {
                            if (currentUser.UserRoles.FirstOrDefault().Role.Name.ToLower().Equals("fad"))
                            {
                                var payment = await _unitOfWork.Payment.FirstOrDefaultAsync(x => x.ApplicationId == appid);
                                if (payment != null)
                                {
                                    payment.Status = Enum.GetName(typeof(AppStatus), AppStatus.PaymentCompleted);
                                    payment.TransactionDate = DateTime.UtcNow.AddHours(1);
                                }
                                app.FADApproved = true;
                            }
                            if(currentUser.UserRoles.FirstOrDefault().Role.Name.ToLower().Equals("reviewer") && !app.FADApproved)
                                return (false, string.Empty);
                        }

                        //await _unitOfWork.Application.Update(app);
                        await _unitOfWork.SaveChangesAsync(currentUser.Id);
                        nextprocessingofficer.LastJobDate = DateTime.UtcNow.AddHours(1);
                        await _userManager.UpdateAsync(nextprocessingofficer);
                        //save action to history
                        await SaveHistory(action, appid, wkflow, currentUser, nextprocessingofficer, comment);
                        res = true;

                        //Generate permit number on final approval
                        if (wkflow.Status.Equals(Enum.GetName(typeof(AppStatus), AppStatus.Completed)))
                        {
                            var permit = await GeneratePermit(appid, currentUser.Id);
                            if (permit.Item1)
                                comment = $"Application with reference {app.Reference} has been approved and permit {permit.Item2} has been generated successfully";
                        }
                        //send and save notification
                        await SendNotification(app, action, nextprocessingofficer, comment);
                    }
                }
            }
            catch (Exception ex)
            {
                //_generalLogger.LogRequest($"{"Internal server error occurred while trying to fetch staff dashboard"}{"-"}{DateTime.Now}", true, directory);
            }
            return (res, nextprocessingofficer.Id);
        }

        public async Task<WorkFlow> GetWorkFlow(string action, string currentuserrole, int factypeid) => await _unitOfWork.Workflow.FirstOrDefaultAsync(x
                    => x.Action.ToLower().Trim().Equals(action.ToLower().Trim())
                    && currentuserrole.Equals(x.TriggeredByRole)
                    && x.FacilityTypeId == factypeid);

        public async Task<bool> SaveHistory(string action, int appid, WorkFlow flow, ApplicationUser user, ApplicationUser nextUser, string comment)
        {
            await _unitOfWork.ApplicationHistory.Add(new ApplicationHistory
            {
                Action = action,
                Date = DateTime.Now.AddHours(1),
                ApplicationId = appid,
                TargetedTo = nextUser.Id,
                TargetRole = nextUser.UserRoles.Where(x => !x.Role.Id.Equals("Staff")).FirstOrDefault().Role.Id,
                TriggeredBy = user.Id,
                TriggeredByRole = user.UserRoles.Where(x => !x.Role.Name.Equals("Staff")).FirstOrDefault().Role.Id,
                Comment = comment
            });
            var res = await _unitOfWork.SaveChangesAsync(user.Id);
            return res > 0;
        }

        public async Task<ApplicationUser> GetNextStaff(int appid, string action, WorkFlow wkflow, ApplicationUser currentUser, string delUserId = null)
        {
            ApplicationUser nextprocessingofficer = null;
            if(!string.IsNullOrEmpty(delUserId))
                return _userManager.Users.Include(x => x.Company)
                    .Include(ur => ur.UserRoles).ThenInclude(r => r.Role)
                    .FirstOrDefault(x => x.Id.Equals(delUserId) && x.IsActive);
            else
            {
                if (action.Equals(Enum.GetName(typeof(AppActions), AppActions.Reject)) || action.Equals(Enum.GetName(typeof(AppActions), AppActions.Resubmit)) && wkflow != null)
                {
                    var historylist = await _unitOfWork.ApplicationHistory.Find(x => x.ApplicationId == appid
                                            && currentUser.UserRoles.FirstOrDefault().Role.Id.Equals(x.TargetRole)
                                            && x.TargetedTo.Equals(currentUser.Id)
                                            && x.TriggeredByRole.Equals(wkflow.TargetRole));
                    if(action.Equals(Enum.GetName(typeof(AppActions), AppActions.Resubmit)))
                         historylist = await _unitOfWork.ApplicationHistory.Find(x => x.ApplicationId == appid
                                            && currentUser.UserRoles.FirstOrDefault().Role.Id.Equals(x.TriggeredByRole)
                                            && x.Action.Equals(Enum.GetName(typeof(AppActions), AppActions.Submit))
                                            && x.TriggeredByRole.Equals(wkflow.TriggeredByRole));

                    var history = historylist.OrderByDescending(x => x.Id).FirstOrDefault();
                    if (history != null)
                    {
                        nextprocessingofficer = _userManager.Users.Include(ur => ur.UserRoles).ThenInclude(r => r.Role)
                                                    .FirstOrDefault(x => x.Id.Equals(history.TargetedTo));
                        if (nextprocessingofficer != null && !nextprocessingofficer.IsActive)
                        {
                            var users = _userManager.Users
                                            .Include(x => x.Company).Include(ur => ur.UserRoles).ThenInclude(r => r.Role)
                                            .Where(x => x.UserRoles.Where(y => y.Role.Id.Equals(wkflow.TargetRole)) != null
                                            && x.IsActive).ToList();
                            nextprocessingofficer = users.OrderBy(x => x.LastJobDate).FirstOrDefault();
                        }
                    }
                }
                if (wkflow != null && nextprocessingofficer == null)
                {
                    if (wkflow.TargetRole.Equals(currentUser.UserRoles.FirstOrDefault().Role.Id))
                        nextprocessingofficer = currentUser;
                    else
                    {
                        var users = _userManager.Users.Include(x => x.Company).Include(f => f.Company)
                                    .Include(ur => ur.UserRoles).ThenInclude(r => r.Role)
                                    .Where(x => x.UserRoles.Any(y => y.Role.Id.Equals(wkflow.TargetRole))
                                    && x.IsActive).ToList();
                        nextprocessingofficer = users.Count == 1 ? users.FirstOrDefault() : users.OrderBy(x => x.LastJobDate).FirstOrDefault();
                    }
                }
                return nextprocessingofficer;
            }
        }

        internal async Task<(bool, string)> GeneratePermit(int id, string userid)
        {
            var app = await _unitOfWork.Application.FirstOrDefaultAsync(x => x.Id == id, "Facility.FacilityType,ApplicationType");
            if(app != null)
            {
                var year = DateTime.Now.Year.ToString();
                var pno = $"NMDPRA/BUNK/{app.Facility.FacilityType.Code}/{app.ApplicationType.Name.Substring(0, 1).ToUpper()}/{year.Substring(0)}/{app.Id}";
                var qrcode = Utils.GenerateQrCode($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/License/ValidateQrCode/{id}");
                //license.QRCode = Convert.ToBase64String(qrcode, 0, qrcode.Length);
                //save permit to elps and portal
                var permit = new Permit
                {
                    ApplicationId = id,
                    ExpireDate = DateTime.Now,
                    IssuedDate= DateTime.Now,
                    PermitNo = pno,
                    Signature = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/wwwroot/assets/fa.png",
                    QRCode = Convert.ToBase64String(qrcode, 0, qrcode.Length)
                };

                var req = await Utils.Send(_appSetting.ElpsUrl, new HttpRequestMessage(HttpMethod.Post, $"api/Permits/{app.User.ElpsId}/{_appSetting.AppEmail}/{Utils.GenerateSha512($"{_appSetting.AppEmail}{_appSetting.AppId}")}")
                {
                    Content = new StringContent(new 
                    { 
                        Permit_No = pno,
                        OrderId = app.Reference,
                        Company_Id = app.User.ElpsId,
                        Date_Issued = permit.IssuedDate.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                        Date_Expire = permit.ExpireDate.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                        CategoryName = $"Bunkering ({app.Facility.FacilityType.Name})",
                        Is_Renewed = app.ApplicationType.Name,
                        LicenseId = id,
                        Expired = false
                    }.Stringify(), Encoding.UTF8, "application/json")
                });

                if(req.IsSuccessStatusCode)
                {
                    var content = await req.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var dic = content.Parse<Dictionary<string, string>>();
                        permit.ElpsId = int.Parse(dic.GetValue("id"));
                        await _unitOfWork.Permit.Add(permit);
                        await _unitOfWork.SaveChangesAsync(userid);

                        return (true, pno);
                    }
                }

            }
            return (false, null);
        }

        public async Task SendNotification(Application app, string action, ApplicationUser user, string comment)
        {
            string content = $"Application with reference {app.Reference} has been submitted to your desk for further processing";
            string subject = $"Application with reference {app.Reference} Submitted";
            switch (action)
            {
                case "Reject":
                    content = $"Application with reference {app.Reference} has been rejected and <br/>returned to your desk for further processing. Below is for your information - <br/>{comment}";
                    break;
                case "Approve":
                    content = $"Application with reference {app.Reference} has been endorsed and <br/> move to your desk for further processing";
                    subject = $"Application with reference {app.Reference} pushed for processing";
                    break;
                default:
                    break;
            }
            //send and save notification
            
            var body = Utils.ReadTextFile(_env.WebRootPath, "GeneralTemplate.cshtml");
            var url = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/assets/nmdpraLogo.png";
            body = string.Format(body, content, DateTime.Now.Year, url);
            Utils.SendMail(_mailSetting.Stringify().Parse<Dictionary<string, string>>(), user.Email, subject, body);

            await _unitOfWork.Message.Add(new Message
            {
                ApplicationId = app.Id,
                Content = body,
                Date = DateTime.Now.AddHours(1),
                Subject = subject,
                UserId = user.Id
            });
            await _unitOfWork.SaveChangesAsync(user.Id);
        }
    }
}
