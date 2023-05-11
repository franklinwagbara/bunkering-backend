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
using Microsoft.AspNetCore.Authorization;

namespace Bunkering.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/bunkering/[controller]")]
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
        
    }
}
