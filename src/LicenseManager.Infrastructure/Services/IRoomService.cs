using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public interface IRoomService
    {
         Task<IEnumerable<RoomDto>> BrowseAsync();
         Task<RoomDto> GetAsync(Guid roomId);
         Task<RoomDto> GetAsync(string name);
         Task AddAsync(string name);
         Task RemoveAsync(Guid roomId);
         Task UpdateAsync(Guid roomId, string name);
    }
}