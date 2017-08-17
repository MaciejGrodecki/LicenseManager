using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;

namespace LicenseManager.Infrastructure.Repositories
{
    public class InMemoryRoomRepository : IRoomRepository
    {
        private readonly ISet<Room> _rooms = new HashSet<Room>();
        public async Task<IEnumerable<Room>> BrowseAsync()
            => await Task.FromResult(_rooms);
        public async Task<Room> GetAsync(Guid roomId)
            => await Task.FromResult(_rooms.SingleOrDefault(x => x.RoomId == roomId));

        public async Task<Room> GetAsync(string name)
            => await Task.FromResult(_rooms.SingleOrDefault(x => x.Name == name));

        public async Task AddAsync(Room room)
        {
            _rooms.Add(room);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Room room)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Room room)
        {
            _rooms.Remove(room);
            await Task.CompletedTask;
        }
    }
}