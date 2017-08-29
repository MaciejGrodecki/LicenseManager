using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoomDto>> BrowseAsync()
        {
            var rooms = await _roomRepository.BrowseAsync();

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetAsync(Guid roomId)
        {
            var room = await _roomRepository.GetAsync(roomId);

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> GetAsync(string name)
        {
            var room = await _roomRepository.GetAsync(name);
            
            return _mapper.Map<RoomDto>(room);
        }

        public async Task AddAsync(string name)
        {
            var room = await _roomRepository.GetAsync(name.ToLowerInvariant());
            if(room != null)
            {
                throw new Exception($"Room with name: {name} already exist");
            }
            room = new Room(Guid.NewGuid(), name);
            await _roomRepository.AddAsync(room);
        }

        public async Task RemoveAsync(Guid roomId)
        {
            var room = await _roomRepository.GetAsync(roomId);
            if(room == null)
            {
                throw new Exception($"Room with id: {roomId} doesn't exist");
            }

            await _roomRepository.RemoveAsync(room);
        }

        public async Task UpdateAsync(Guid roomId, string name)
        {
            var room = await _roomRepository.GetAsync(name.ToLowerInvariant());
            if(room != null)
            {
                throw new Exception($"Room with name: {name} already exist");
            }
            room = await _roomRepository.GetAsync(roomId);
            if(room == null)
            {
                throw new Exception($"Room with id: {roomId} cannot be updated becouse it doesn't exist");
            }
            room.SetName(name);
            await _roomRepository.UpdateAsync(room);
        }
    }
}