using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.EntityFramework
{
    public class SqlUserRepository : IUserRepository
    {
        private LicensesContext _context;

        public SqlUserRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> BrowseAsync()
            => await Task.FromResult(_context.Users);

        public async Task<User> GetAsync(Guid userId)
            => await Task.FromResult(_context.Users.SingleOrDefault(x => x.UserId == userId));

        public async Task<User> GetAsync(string name, string surname)
            => await Task.FromResult(_context.Users.SingleOrDefault(x => x.Name == name && x.Surname == surname));

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}