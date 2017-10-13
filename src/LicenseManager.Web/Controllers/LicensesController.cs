using System;
using Microsoft.AspNetCore.Mvc;


namespace LicenseManager.Web.Controllers
{
    
    public class LicensesController : Controller
    {
        [Route("")]
        [Route("licenses/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("license/add")]
        public IActionResult Add()
        {
            return View();
        }

        [Route("license/{licenseId}")]
        public IActionResult Details(Guid? licenseId)
        {
            if(licenseId == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}