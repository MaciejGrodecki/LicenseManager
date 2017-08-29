using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.Repositories
{
    public class InMemoryComputerRepository : IComputerRepository
    {
        private static readonly ISet<Computer> _computers = new HashSet<Computer>();
        public async Task<IEnumerable<Computer>> BrowseAsync()
            => await Task.FromResult(_computers);

        public async Task<Computer> GetAsync(Guid computerId)
            => await Task.FromResult(_computers.SingleOrDefault(x => x.ComputerId == computerId));

        public async Task<Computer> GetAsync(string inventoryNumber)
            => await Task.FromResult(_computers.SingleOrDefault(x => x.InventoryNumber == inventoryNumber));

        public async Task AddAsync(Computer computer)
        {
            _computers.Add(computer);
            await Task.CompletedTask;
        }
        public async Task RemoveAsync(Computer computer)
        {
            _computers.Remove(computer);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Computer computer)
        {
            await Task.CompletedTask;
        }
    }
}