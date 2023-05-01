
using Bunkering.Access;
using Bunkering.Access.Services;
using Bunkering.Controllers;
using Bunkering.Core.Data;
using Bunkering.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Buner.Controllers.API
{
    [AllowAnonymous]
    [Route("api/bunkering/[controller]")]
    public class AuthController : ResponseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _user;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppConfiguration _appConfig;
        private readonly AuthService _authService;

        public AuthController(
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> appSettings,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppConfiguration appConfig,
            AuthService authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings.Value;
            _user = userManager;
            _signInManager = signInManager;
            _appConfig = appConfig;
            _authService = authService;
        }

        [HttpPost]
        [Route("login-redirect")]
        public async Task<IActionResult> LoginRedirect([FromBody]LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var login = await _authService.UserAuth(model);
                if (login != null && login.Success)
                    return Redirect($"{Request.Scheme}://{Request.Host}/home?id={login.Data}");
            }
            return Redirect($"{Request.Scheme}://{Request.Host}/home");
        }

        [HttpGet]
        [Route("validate-user")]
        public async Task<IActionResult> ValidateUser(string id) => Response(await _authService.ValidateUser(id));

        [HttpGet]
        [Route("log-out")]
        public async Task<IActionResult> Logout() 
        {
            var elpsLogOffUrl = $"{_appConfig.Config().GetValue("elpsurl")}/Account/RemoteLogOff";
            var returnUrl = $"{Request.Scheme}://{Request.Host}";
            var frm = "<form action='" + elpsLogOffUrl + "' id='frmTest' method='post'>" + "<input type='hidden' name='returnUrl' value='" + returnUrl + "' />" + "<input type='hidden' name='appId' value='" + _appConfig.Config().GetValue("publickey") + "' />" + "</form>" + "<script>document.getElementById('frmTest').submit();</script>";
            return Content(frm, "text/html");
        }
    }
}
