using System;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.Commands.LicenseType;
using LicenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManager.Api.Controllers
{
    [Route("[controller]")]
    public class LicenseTypesController : Controller
    {
        private readonly ILicenseTypeService _licenseTypeService;

        public LicenseTypesController(ILicenseTypeService licenseTypeService)
        {
            _licenseTypeService = licenseTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var licenseTypes = await _licenseTypeService.BrowseAsync();

            return Json(licenseTypes);
        }

        [HttpGet("{licenseTypeId}")]
        public async Task<IActionResult> Get(Guid licenseTypeId)
        {
            var licenseType = await _licenseTypeService.GetAsync(licenseTypeId);

            return Json(licenseType);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var licenseType = await _licenseTypeService.GetAsync(name);

            return Json(licenseType);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddLicenseType command)
        {
            await _licenseTypeService.AddAsync(command.Name);

            return Created($"/licenseTypes/{command.Name}", null);
        }

        [HttpPut("{licenseTypeId}")]
        public async Task<IActionResult> Put(Guid licenseTypeId, [FromBody]UpdateLicenseType command)
        {
            await _licenseTypeService.UpdateAsync(licenseTypeId, command.Name);

            return NoContent();
        }

        [HttpDelete("{licenseTypeId}")]
        public async Task<IActionResult> Delete(Guid licenseTypeId)
        {
            await _licenseTypeService.RemoveAsync(licenseTypeId);
            return NoContent();
        }
    }
}