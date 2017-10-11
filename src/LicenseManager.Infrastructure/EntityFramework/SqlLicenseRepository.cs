using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.EntityFramework
{
    public class SqlLicenseRepository : ILicenseRepository
    {
        private LicensesContext _context;

        public SqlLicenseRepository(LicensesContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<License>> BrowseAsync()
            => await Task.FromResult(_context.Licenses);

        public async Task<IEnumerable<License>> BrowseAsync(string name)
        {
            var licenses = _context.Licenses.Where(x => x.Name == name);

            return await Task.FromResult(licenses.ToList());
        }

        public async Task<License> GetAsync(Guid licenseId)
            => await Task.FromResult(_context.Licenses.SingleOrDefault(x => x.LicenseId == licenseId));

        public async Task AddAsync(License license)
        {
            await _context.Licenses.AddAsync(license);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(License license)
        {
            _context.Licenses.Remove(license);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(License license)
        {
            _context.Licenses.Update(license);
            await _context.SaveChangesAsync();
        }
    }
}