using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>();

        public async Task<IEnumerable<User>> BrowseAsync()
            => await Task.FromResult(_users);

        public async Task<User> GetAsync(Guid userId)
            => await Task.FromResult(_users.SingleOrDefault(x => x.UserId == userId));

        public async Task<User> GetAsync(string name, string surname)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Name == name && x.Surname == surname));

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}