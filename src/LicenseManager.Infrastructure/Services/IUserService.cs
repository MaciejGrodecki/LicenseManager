using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public interface IUserService
    {
         Task<IEnumerable<UserDto>> BrowseAsync();
         Task<UserDto> GetAsync(Guid userId);
         Task<UserDto> GetAsync(string name, string surname);
         Task AddAsync(string name, string surname);
         Task RemoveAsync(Guid userId);
         Task UpdateAsync(Guid userId, string name, string surname);
    }
}