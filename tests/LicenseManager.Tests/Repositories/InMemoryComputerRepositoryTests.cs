using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.Repositories;
using Xunit;

namespace LicenseManager.Tests.Repositories
{
    public class InMemoryComputerRepositoryTests
    {
        private readonly IComputerRepository _repository;

        private static readonly Room _room = new Room(Guid.NewGuid(), "A-11");
        private static readonly ISet<User> _users = new HashSet<User>{
            new User("Jan", "Kowalski")
        };
        private static readonly Computer _computer = new Computer(
            Guid.NewGuid(), "US-IN/Z/1-W", "192.168.0.1", _room.RoomId
        );
        

        public InMemoryComputerRepositoryTests()
        {
            //Arrange
            _repository = new InMemoryComputerRepository();
            
        }


        [Fact]
        public async Task When_adding_new_computer_it_should_be_added_correctly_to_the_collection()
        {

            //Act
            await _repository.AddAsync(_computer);

            //Assert
            var existingComputer = await _repository.GetAsync(_computer.ComputerId);
            Assert.Equal(_computer, existingComputer);
        }

        [Fact]
        public async Task Invoking_BrowseAsync_should_return_collection_of_computer_objects()
        {

            //Act
            var computers = await _repository.BrowseAsync();

            //Assert
            Assert.IsType<HashSet<Computer>>(computers);
        }
        
        [Fact]
        public async Task Invoking_GetAsync_with_computerId_parameter_should_return_computer_object()
        {
            //Arrange
            await _repository.AddAsync(_computer);
            
            //Act
            var existingComputer = await _repository.GetAsync(_computer.ComputerId);

            //Assert
            Assert.IsType(typeof(Computer), existingComputer);
            Assert.Equal(_computer, existingComputer);
        }

        [Fact]
        public async Task Invoking_GetAsync_with_inventoryNumber_parameter_should_return_computer_object()
        {
            //Arrange
            await _repository.AddAsync(_computer);
            
            //Act
            var existingComputer = await _repository.GetAsync(_computer.InventoryNumber);

            //Assert
            Assert.IsType(typeof(Computer), existingComputer);
            Assert.Equal(_computer, existingComputer);
        }  
        

        [Fact]
        public async Task Invoking_RemoveAsync_should_remove_computer_object_from_collection()
        {
            //Arrange
            await _repository.AddAsync(_computer);

            //Act
            await _repository.RemoveAsync(_computer);
            var computer = await _repository.GetAsync(_computer.ComputerId);
            
            //Assert
            Assert.Null(computer);
        }
    }
}