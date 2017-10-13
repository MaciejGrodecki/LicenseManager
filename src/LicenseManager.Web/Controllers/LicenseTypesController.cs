using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicenseManager.Web.Controllers
{
    public class LicenseTypesController : Controller
    {
        [Route("licenseTypes/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("licenseType/add")]
        public IActionResult Add()
        {
            return View();
        }
    }
}
