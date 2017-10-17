using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public interface IComputerService
    {
         Task<ComputerDto> GetAsync(Guid computerId);
         Task<ComputerDto> GetAsync(string inventoryNumber);
         Task<IEnumerable<ComputerDto>> BrowseAsync();
         Task AddAsync(Guid computerId, string inventoryNumber, string ipAddress, Guid roomId);
         Task UpdateAsync(Guid computerId, string inventoryNumber, string ipAddress, Guid roomId, ISet<Guid> userIds);
         Task RemoveAsync(Guid computerId);
         Task AddUserToComputer(Guid computerId, ISet<Guid> userIds);
         Task AddLicenseToComputer(Guid computerId, Guid licenseId);
    }
}