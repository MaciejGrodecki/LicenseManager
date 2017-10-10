using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Infrastructure.EntityFramework
{
    public class SqlLicenseTypeRepository : ILicenseTypeRepository
    {
        private LicensesContext _context;

        public SqlLicenseTypeRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LicenseType>> BrowseAsync()
            => await Task.FromResult(_context.LicenseTypes);

        public async Task<LicenseType> GetAsync(Guid licenseTypeId)
            => await Task.FromResult(_context.LicenseTypes.SingleOrDefault(x => x.LicenseTypeId == licenseTypeId));

        public async Task<LicenseType> GetAsync(string name)
            => await Task.FromResult(_context.LicenseTypes.SingleOrDefault(x => x.Name == name));

        public async Task AddAsync(LicenseType licenseType)
        {
            await _context.LicenseTypes.AddAsync(licenseType);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAsync(LicenseType licenseType)
        {
            _context.LicenseTypes.Remove(licenseType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LicenseType licenseType)
        {
            _context.LicenseTypes.Remove(licenseType);
            await _context.SaveChangesAsync();
        }
    }

        
}
