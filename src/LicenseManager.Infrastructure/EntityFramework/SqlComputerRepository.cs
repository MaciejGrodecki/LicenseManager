using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.EntityFramework
{
    public class SqlComputerRepository : IComputerRepository
    {
        private LicensesContext _context;

        public SqlComputerRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Computer>> BrowseAsync()
            => await Task.FromResult(_context.Computers);

        public async Task<Computer> GetAsync(Guid computerId)
            => await Task.FromResult(_context.Computers.SingleOrDefault(x => x.ComputerId == computerId));

        public async Task<Computer> GetAsync(string inventoryNumber)
            => await Task.FromResult(_context.Computers.SingleOrDefault(x => x.InventoryNumber == inventoryNumber));

        public async Task AddAsync(Computer computer)
        {
            await _context.Computers.AddAsync(computer);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Computer computer)
        {
            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Computer computer)
        {
            _context.Computers.Update(computer);
            await _context.SaveChangesAsync();
        }
    }
}