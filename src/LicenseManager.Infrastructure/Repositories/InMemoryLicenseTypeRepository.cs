using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.Repositories
{
    public class InMemoryLicenseTypeRepository : ILicenseTypeRepository
    {
        private ISet<LicenseType> _licenseTypes = new HashSet<LicenseType>();

        public async Task<IEnumerable<LicenseType>> BrowseAsync()
            => await Task.FromResult(_licenseTypes);

        public async Task<LicenseType> GetAsync(Guid licenseTypeId)
            => await Task.FromResult(_licenseTypes.SingleOrDefault(x => x.LicenseTypeId == licenseTypeId));

        public async Task<LicenseType> GetAsync(string name)
            => await Task.FromResult(_licenseTypes.SingleOrDefault(x => x.Name == name));

        public async Task AddAsync(LicenseType licenseType)
        {
            _licenseTypes.Add(licenseType);
            await Task.CompletedTask;
        }


        public async Task RemoveAsync(LicenseType licenseType)
        {
            _licenseTypes.Remove(licenseType);
            await Task.CompletedTask;
        }

        public Task UpdateAsync(LicenseType licenseType)
        {
            throw new NotImplementedException();
        }
    }
}