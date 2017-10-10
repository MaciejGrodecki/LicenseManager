using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManager.Infrastructure.EntityFramework
{
    public class SqlRoomRepository : IRoomRepository
    {
        private LicensesContext _context;

        public SqlRoomRepository(LicensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> BrowseAsync()
            => await Task.FromResult(_context.Rooms);

        public async Task<Room> GetAsync(Guid roomId)
            => await Task.FromResult(_context.Rooms.SingleOrDefault(x => x.RoomId == roomId));

        public async Task<Room> GetAsync(string name)
            => await Task.FromResult(_context.Rooms.SingleOrDefault(x => x.Name == name));

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}
