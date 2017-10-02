using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseManager.Web.Controllers
{
    public class RoomsController : Controller
    {
        [Route("rooms/index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("rooms/add")]
        public IActionResult Add()
        {
            return View();
        }
    }
}
