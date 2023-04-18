using Bunkering.Core.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bunkering.Core.ViewModels;
using System.Diagnostics;
using Bunkering.Access.IContracts;
using Bunkering.Access;

namespace Bunkering.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly AppConfiguration _appConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElps _elps;

        public AccountController(
            AppConfiguration appConfig, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> singInManager,
            IUnitOfWork unitOfWork,
            IElps elps)
        {
            _appConfig = appConfig;
            _userManager = userManager;
            _signInManager = singInManager;
            _unitOfWork = unitOfWork;
            _elps = elps;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> Auth(LoginViewModel model) 
        {
            var user = new ApplicationUser();
            var hash = Utils.GenerateSha512($"{_appConfig.Config().GetValue("publickey").ToUpper()}.{model.Email.ToUpper()}.{_appConfig.Config().GetValue("appid").ToUpper()}");
            if(Debugger.IsAttached && (!Debugger.IsAttached && model.Code.Equals(hash)))
            {
                //check if it's company login
                var company = _elps.GetCompanyDetailByEmail(model.Email);

                if (company.Count > 0)
                {
                    int compelspid = int.Parse(company.GetValue("id"));
                    user = _userManager.Users.Include(x => x.Company).FirstOrDefault(x => x.ElpsId == compelspid);

                    if (user == null)
                        user = await RegisterCompany(company);
                    else if (user != null && !user.Email.Equals(model.Email))
                    {
                        var addid = company.GetValue("registered_Address_Id");
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        user.Company.AddressId = string.IsNullOrEmpty(addid)
                            ? 0 : int.Parse(addid);
                        user.Company.Name = company.GetValue("name");
                        user.PhoneNumber = company.GetValue("contact_phone");

                        var result = await _userManager.UpdateAsync(user);
                    }
                }
                else
                {
                    //get staffn
                    var staff = _elps.GetStaff(model.Email);
                    if (staff != null)
                    {
                        user = _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefault(x => x.ElpsId.Equals(staff.Id));
                        if (user != null)
                        {
                            user.ElpsId = staff.Id;
                            user.FirstName = staff.FirstName;
                            user.LastName = staff.LastName;
                            user.IsActive = true;
                            user.ProfileComplete = true;
                            if (!user.Email.Equals(model.Email))
                            {
                                user.Email = model.Email;
                                user.UserName = model.Email;

                                var result = await _userManager.UpdateAsync(user);
                            }
                        }
                        else
                        {
                            user = new ApplicationUser
                            {
                                ElpsId = staff.Id,
                                FirstName = staff.FirstName,
                                LastName = staff.LastName,
                                Email = staff.Email.ToLower(),
                                UserName = staff.Email.ToLower(),
                                IsActive = true,
                            };
                            var result = await _userManager.CreateAsync(user);

                            if (result.Succeeded)
                                await _userManager.AddToRoleAsync(user, "Staff");

                        }

                        
                    }
                }
                if (user is { IsActive: true })
                {
                    await _signInManager.SignInAsync(user, false);

                    if (User.IsInRole("Staff"))
                    {
                        TempData["ErrorMessage"] = "Access to this portal is denied, please contact ICT/Support.";
                        return RedirectToAction("Index", "Home");
                    }
                    // await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    //     principal,
                    //     new AuthenticationProperties
                    //     {
                    //         IsPersistent = true,
                    //         ExpiresUtc = DateTime.Now.AddMinutes(60)
                    //     });

                    if (!user.ProfileComplete && _userManager.IsInRoleAsync(user, "Company").Result)
                        return RedirectToAction("Profile", "Company");
                    if (user.ProfileComplete && _userManager.IsInRoleAsync(user, "Company").Result)
                        return RedirectToAction("Index", "Company");


                    //if (user.UserRoles.FirstOrDefault().Role.Name.Equals("Support") || user.UserRoles.FirstOrDefault().Role.Name.Equals("HeadAdmin")
                    //    || user.UserRoles.FirstOrDefault().Role.Name.Equals("SuperAdmin"))
                        //return RedirectToAction("All", "Application");
                    //else if (user.UserRoles.FirstOrDefault().Role.Name.Equals("ACE_STA") || user.UserRoles.FirstOrDefault().Role.Name.Equals("ED_STA"))
                    //    return RedirectToAction("Index", "Admin");
                    //else
                        return RedirectToAction("Dashboard", "Staff");
                }
                else if (user is { IsActive: false })
                    TempData["ErrorMessage"] = "Access to this portal is denied, please contact ICT/Support.";
                else
                    TempData["ErrorMessage"] = "An error occured, please contact Support/ICT.";
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<ApplicationUser> RegisterCompany(Dictionary<string, string> dic)
        {
            var elpsaddid = dic.GetValue("registered_Address_Id");
            var company = new Company
            {
                Name = dic.GetValue("name"),
                //Nationality = dic.GetValue("nationality"),
                
                RcNumber = dic.GetValue("rC_Number"),
                TinNumber = dic.GetValue("tin_Number"),
                YearIncorporated = dic.GetValue("year_Incorporated"),
                AddressId = string.IsNullOrEmpty(elpsaddid) ? 0 : int.Parse(elpsaddid),

            };

            var user = new ApplicationUser
            {
                UserName = dic.GetValue("user_id"),
                Email = dic.GetValue("user_id"),
                EmailConfirmed = true,
                ElpsId = int.Parse(dic.GetValue("id")),
                PhoneNumber = dic.GetValue("contact_Phone"),
                ProfileComplete = false,
                FirstName = dic.GetValue("contact_firstname"),
                LastName = dic.GetValue("contact_lastname"),
                Company = company,
                IsActive = true
            };
            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Company");

            return user;
        }

        public async Task<IActionResult> ChangePassword(PasswordViewModel model)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                var response = _elps.ChangePassword(new
                {
                    model.OldPassword,
                    model.NewPassword,
                    ConfirmPassword = model.CPassword
                }, User.Identity.Name);

                if (response != null)
                {
                    if (response.GetValue("msg").Equals("ok", StringComparison.OrdinalIgnoreCase) &&
                        response.GetValue("code").Equals("1"))
                    {
                        TempData["Message"] = "Password changed successfully, please login again to continue";
                        return await LogOff();
                    }
                    TempData["Message"] = "Password change unsuccessful, please try again";

                }
            }
            return RedirectToAction("Index", "DashBoard");
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            var elpsLogOffUrl = $"{_appConfig.Config().GetValue("elpsurl")}/Account/RemoteLogOff";
            var returnUrl = $"{Request.Scheme}://{Request.Host}";
            var frm = "<form action='" + elpsLogOffUrl + "' id='frmTest' method='post'>" + "<input type='hidden' name='returnUrl' value='" + returnUrl + "' />" + "<input type='hidden' name='appId' value='" + _appConfig.Config().GetValue("publickey") + "' />" + "</form>" + "<script>document.getElementById('frmTest').submit();</script>";
            return Content(frm, "text/html");
        }
    }
}
