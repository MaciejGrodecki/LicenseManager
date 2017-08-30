using System;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.Commands.User;
using LicenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();
            return Json(users);
        }

        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var user = await _userService.GetAsync(userId);
            if(user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpGet("{name}/{surname}")]
        public async Task<IActionResult> Get(string name, string surname)
        {
            var user = await _userService.GetAsync(name, surname);
            if(user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddUser command)
        {
            await _userService.AddAsync(command.Name, command.Surname);

            return Created($"/users/{command.Surname}", null);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody]UpdateUser command)
        {
            await _userService.UpdateAsync(userId, command.Name, command.Surname);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _userService.RemoveAsync(userId);
            return NoContent();
        }
    }
}