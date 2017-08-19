using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.Repositories;
using Xunit;

namespace LicenseManager.Tests.Repositories
{
    public class InMemoryRoomRepositoryTests
    {
        private readonly IRoomRepository _repository;
        private static readonly Room _room = new Room("A-13");
        public InMemoryRoomRepositoryTests()
        {
            //Arrange
            _repository = new InMemoryRoomRepository();
            
        }


        [Fact]
        public async Task When_adding_new_room_it_should_be_added_correctly_to_the_collection()
        {

            //Act
            await _repository.AddAsync(_room);

            //Assert
            var existingRoom = await _repository.GetAsync(_room.Name);
            Assert.Equal(_room, existingRoom);
        }

        [Fact]
        public async Task Invoking_BrowseAsync_should_return_collection_of_room_objects()
        {

            //Act
            var rooms = await _repository.BrowseAsync();

            //Assert
            Assert.IsType<HashSet<Room>>(rooms);
        }
        
        [Fact]
        public async Task Invoking_GetAsync_with_roomId_parameter_should_return_room_object()
        {
            //Arrange
            await _repository.AddAsync(_room);
            
            //Act
            var existingRoom = await _repository.GetAsync(_room.RoomId);

            //Assert
            Assert.IsType(typeof(Room), existingRoom);
            Assert.Equal(_room, existingRoom);
        } 
        
        [Fact]
        public async Task Invoking_GetAsync_with_name_parameter_should_return_room_object()
        {
            //Arrange
            await _repository.AddAsync(_room);

            //Act
            var existingRoom = await _repository.GetAsync(_room.Name);

            //Assert
            Assert.Equal(_room, existingRoom);
            Assert.IsType(typeof(Room), existingRoom);
            
        }

        [Fact]
        public async Task Invoking_RemoveAsync_should_remove_room_object_from_collection()
        {
            //Arrange
            await _repository.AddAsync(_room);

            //Act
            await _repository.RemoveAsync(_room);
            var room = await _repository.GetAsync(_room.Name);
            
            //Assert
            Assert.Null(room);
        }
    }
}