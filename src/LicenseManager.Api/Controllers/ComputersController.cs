using System;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.Commands.Computer;
using LicenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Api.Controllers
{
    [Route("[controller]")]
    public class ComputersController : Controller
    {
        private readonly IComputerService _computerService;

        public ComputersController(IComputerService computerService)
        {
            _computerService = computerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var computers = await _computerService.BrowseAsync();

            return Json(computers);
        }

        [HttpGet("{computerId:Guid}")]
        public async Task<IActionResult> Get(Guid computerId)
        {
            var computer = await _computerService.GetAsync(computerId);
            if(computer == null)
            {
                return NotFound();
            }

            return Json(computer);
        }

        [HttpGet("{inventoryNumber}")]
        public async Task<IActionResult> Get(string inventoryNumber)
        {
            var computer = await _computerService.GetAsync(inventoryNumber);
            if(computer == null)
            {
                return NotFound();
            }

            return Json(computer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddComputer command)
        {
            command.ComputerId = Guid.NewGuid();
            
            await _computerService.AddAsync(command.ComputerId, command.InventoryNumber, command.IpAddress,
                command.RoomId);
            
            await _computerService.AddUserToComputer(command.ComputerId, command.UsersId);
            
            return Created($"/computers/{command.InventoryNumber}", null);
        }

        [HttpPut("{computerId}")]
        public async Task<IActionResult> Put(Guid computerId, [FromBody]UpdateComputer command)
        {
            await _computerService.UpdateAsync(computerId, command.InventoryNumber,
                command.IpAddress, command.RoomId);

            return NoContent();
        }

        [HttpDelete("{computerId}")]
        public async Task<IActionResult> Delete(Guid computerId)
        {
            await _computerService.RemoveAsync(computerId);

            return NoContent();
        }
    }
}