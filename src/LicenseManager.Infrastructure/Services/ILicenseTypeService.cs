using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services {
    public interface ILicenseTypeService 
    {
        Task<IEnumerable<LicenseTypeDto>> BrowseAsync ();
        Task<LicenseTypeDto> GetAsync (Guid licenseTypeId);
        Task<LicenseTypeDto> GetAsync (string name);
        Task AddAsync (string name);
        Task RemoveAsync (Guid licenseTypeId);
        Task UpdateAsync (Guid licenseTypeId, string name);
    }
}