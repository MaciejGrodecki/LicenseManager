using System;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.Commands.License;
using LicenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Api.Controllers
{
    [Route("[controller]")]
    public class LicensesController : Controller
    {
        private readonly ILicenseService _licenseService;

        public LicensesController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var licenses = await _licenseService.BrowseAsync();

            return Json(licenses);
        }

        [HttpGet("{licenseId:Guid}")]
        public async Task<IActionResult> Get(Guid licenseId)
        {
            var license = await _licenseService.GetAsync(licenseId);
            if(license == null)
            {
                return NotFound();
            }

            return Json(license);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddLicense command)
        {
            await _licenseService.AddAsync(command.Name, command.count,
                command.licenseTypeId, command.buyDate);

            return Created($"/licenseTypes/{command.Name}", null);
        }

        [HttpPut("{licenseId}")]
        public async Task<IActionResult> Put(Guid licenseId, [FromBody]UpdateLicense command)
        {
            await _licenseService.UpdateAsync(licenseId, command.Name, command.Count,
                command.LicenseTypeId, command.BuyDate);

            return NoContent();
        }

        [HttpDelete("{licenseId}")]
        public async Task<IActionResult> Delete(Guid licenseId)
        {
            await _licenseService.RemoveAsync(licenseId);
            return NoContent();
        }
    }
}