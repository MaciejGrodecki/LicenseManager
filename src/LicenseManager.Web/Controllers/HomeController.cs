using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}