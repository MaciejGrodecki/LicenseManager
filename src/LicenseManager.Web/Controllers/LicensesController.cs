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

        [Route("licenses/Edit")]
        public IActionResult Edit(Guid? licenseTypeId)
        {
            if(licenseTypeId == null)
            {
                return NotFound();
            }
            ViewData["licenseTypeId"] = licenseTypeId;
            return View();
        }
    }
}