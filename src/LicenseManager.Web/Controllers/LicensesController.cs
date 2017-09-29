using System;
using Microsoft.AspNetCore.Mvc;


namespace LicenseManager.Web.Controllers
{
    [Route("")]
    public class LicensesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("licenses/Add")]
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