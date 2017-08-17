using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;

namespace LicenseManager.Core.Repositories
{
    public interface ILicenseRepository
    {
        Task<License> GetAsync(Guid licenseId);
        Task<IEnumerable<License>>  BrowseAsync(string name);
        Task<IEnumerable<License>> BrowseAsync();
        Task AddAsync(License license);
        Task UpdateAsync(License license);
        Task RemoveAsync(License license);
    }
}