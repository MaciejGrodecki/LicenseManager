using System;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.Commands.Room;
using LicenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Api.Controllers
{
    [Route("[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rooms = await _roomService.BrowseAsync();
            return Json(rooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get(Guid roomId)
        {
            var room = await _roomService.GetAsync(roomId);
            if(room == null)
            {
                return NotFound();
            }

            return Json(room);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var room = await _roomService.GetAsync(name);
            if(room == null)
            {
                return NotFound();
            }

            return Json(room);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddRoom command)
        {
            await _roomService.AddAsync(command.Name);

            return Created($"/rooms/{command.Name}", null);
        }

        [HttpPut("{roomId}")]
        public async Task<IActionResult> Put(Guid roomId, [FromBody]UpdateRoom command)
        {
            await _roomService.UpdateAsync(roomId, command.Name);

            return NoContent();
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> Delete(Guid roomId)
        {
            await _roomService.RemoveAsync(roomId);
            return NoContent();
        }
    }
}