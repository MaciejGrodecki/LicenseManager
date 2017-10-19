using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public interface ILicenseService
    {
         Task<IEnumerable<LicenseDto>> BrowseAsync();
         Task<IEnumerable<LicenseDto>> BrowseAsync(string name);
         Task<LicenseDto> GetAsync(Guid licenseId);
         Task AddAsync(string name, int count, Guid licenseTypeId, DateTime buyDate, string serialNumber);
         Task RemoveAsync(Guid licenseId);
         Task UpdateAsync(Guid licenseId, string name, int count, Guid licenseTypeId,
             DateTime buyDate, string serialNumber);
         Task AddComputer(Guid licenseId, ISet<Guid> computerIds);
    }
}