using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;

namespace LicenseManager.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid userId);
        Task<User> GetAsync(string name, string surname);
        Task<IEnumerable<User>> BrowseAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(User user);
    }
}