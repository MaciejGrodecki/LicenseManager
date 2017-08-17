using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;

namespace LicenseManager.Core.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetAsync(Guid roomId);
        Task<Room> GetAsync(string name);
        Task<IEnumerable<Room>> BrowseAsync();
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task RemoveAsync(Room room);
    }
}