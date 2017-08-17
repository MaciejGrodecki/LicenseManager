using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.Repositories
{
    public class InMemoryLicenseRepository : ILicenseRepository
    {
        private static readonly ISet<License> _licenses = new HashSet<License>();
        public async Task<IEnumerable<License>> BrowseAsync()
            => await Task.FromResult(_licenses);

        public async Task<IEnumerable<License>> BrowseAsync(string name)
        {
            var licenses = _licenses.Where(x => x.Name == name);

            return await Task.FromResult(licenses);
        }

        public async Task<License> GetAsync(Guid licenseId)
            => await Task.FromResult(_licenses.SingleOrDefault(x => x.LicenseId == licenseId));
        public async Task AddAsync(License license)
        {
            _licenses.Add(license);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(License license)
        {
            _licenses.Remove(license);
            await Task.CompletedTask;
        }

        public Task UpdateAsync(License license)
        {
            throw new NotImplementedException();
        }
    }
}