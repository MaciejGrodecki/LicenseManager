using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public interface IComputerService
    {
         Task<ComputerDto> GetAsync(Guid computerId);
         Task<ComputerDto> GetAsync(string inventoryNumber);
         Task<IEnumerable<ComputerDto>> BrowseAsync();
         Task AddAsync(string inventoryNumber, string ipAddress, Guid roomId);
         Task UpdateAsync(Guid computerId, string inventoryNumber, string ipAddress, Guid roomId);
         Task RemoveAsync(Guid computerId);
    }
}