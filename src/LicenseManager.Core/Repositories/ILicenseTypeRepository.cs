using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;

namespace LicenseManager.Core.Repositories
{
    public interface ILicenseTypeRepository
    {
        Task<LicenseType> GetAsync(Guid licenseTypeId);
        Task<LicenseType> GetAsync(string name);
        Task<IEnumerable<LicenseType>> BrowseAsync();
        Task AddAsync(LicenseType licenseType);
        Task UpdateAsync(LicenseType licenseType);
        Task RemoveAsync(LicenseType licenseType);
    }
}