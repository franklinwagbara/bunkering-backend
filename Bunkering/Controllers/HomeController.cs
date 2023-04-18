using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bunkering.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public IActionResult Index()
        {
            return View();
        }

    }
}
