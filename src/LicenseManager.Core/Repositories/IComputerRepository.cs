using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;

namespace LicenseManager.Core.Repositories
{
    public interface IComputerRepository
    {
         Task<Computer> GetAsync(Guid computerId);
         Task<Computer> GetAsync(string inventoryNumber);
         Task<IEnumerable<Computer>> BrowseAsync();
         Task AddAsync(Computer computer);
         Task UpdateAsync(Computer computer);
         Task RemoveAsync(Computer computer);
    }
}