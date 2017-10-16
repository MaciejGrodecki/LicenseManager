using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseManager.Web.Controllers
{
    public class UsersController : Controller
    {
        [Route("users/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("user/add")]
        public IActionResult Add()
        {
            return View();
        }

        [Route("user/{userId}")]
        public IActionResult Details(Guid userId)
        {
            if(userId == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}
