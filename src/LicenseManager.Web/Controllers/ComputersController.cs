using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Web.Controllers
{
    public class ComputersController : Controller
    {
        [Route("/computers/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/computers/Add")]
        public IActionResult Add()
        {
            return View();
        }

        [Route("/computer/{computerId}")]
        public IActionResult Details(Guid? computerId)
        {
            if (computerId == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}