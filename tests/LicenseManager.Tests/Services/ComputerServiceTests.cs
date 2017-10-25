using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.DTO;
using LicenseManager.Infrastructure.Services;
using Moq;
using Xunit;

namespace LicenseManager.Tests.Services
{
    public class ComputerServiceTests
    {
        private static readonly Room _room = new Room(Guid.NewGuid(), "A-11");
        private static readonly ISet<User> _users = new HashSet<User>
        {
            new User("Jan", "Kowalski")
        };
        private readonly Mock<IComputerRepository> _computerRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IComputerService _computerService;
        private readonly Computer _computer = new Computer(Guid.NewGuid(), "US-IN/Z/1-W", "10.11.2.1",
             _room.RoomId);
        private readonly ComputerDto _computerDto;
        private readonly ISet<Computer> _computers = new HashSet<Computer>();
        private readonly ISet<ComputerDto> _computersDto = new HashSet<ComputerDto>();

        public ComputerServiceTests()
        {
            _computerRepositoryMock = new Mock<IComputerRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _licenseRepositoryMock = new Mock<ILicenseRepository>();
            _mapperMock = new Mock<IMapper>();
            _computerService = new ComputerService(_computerRepositoryMock.Object, _userRepositoryMock.Object, _licenseRepositoryMock.Object, _mapperMock.Object);
            _computerDto = new ComputerDto
            {
                ComputerId = _computer.ComputerId,
                InventoryNumber = _computer.InventoryNumber,
                IpAddress = _computer.IpAddress,
                RoomId = _computer.RoomId,
                Users = _computer.Users
            };
            _computers.Add(_computer);
            _computersDto.Add(_computerDto);
        }

        [Fact]
        public async Task When_invoking_browse_async_it_should_invoke_browse_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<IEnumerable<ComputerDto>>(_computer)).Returns(_computersDto);
            _computerRepositoryMock.Setup(x => x.BrowseAsync()).ReturnsAsync(_computers);

            //Act
            var existingComputerDto = await _computerService.BrowseAsync();

            //Assert
            _computerRepositoryMock.Verify( x => x.BrowseAsync(), Times.Once());
            _computersDto.Should().NotBeNull();
            _computersDto.Count.Should().Be(1);
        }

        [Fact]
        public async Task When_invoke_get_async_with_computerId_parameter_it_should_invoke_get_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<ComputerDto>(_computer)).Returns(_computerDto);
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.ComputerId)).ReturnsAsync(_computer);

            //Act
            var existingComputerDto = await _computerService.GetAsync(_computer.ComputerId);

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.ComputerId), Times.Once());
            _computerDto.Should().NotBeNull();
            _computerDto.InventoryNumber.ShouldBeEquivalentTo(_computer.InventoryNumber);
            _computerDto.IpAddress.ShouldBeEquivalentTo(_computer.IpAddress);
            _computerDto.RoomId.ShouldBeEquivalentTo(_computer.RoomId);
            _computerDto.Users.ShouldBeEquivalentTo(_computer.Users);
        }

        [Fact]
        public async Task When_invoke_get_async_with_computerId_parameter_and_computer_do_not_exists_it_should_invoke_get_async_on_repository()
        {
            //Arrange
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.ComputerId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _computerService.GetAsync(_computer.ComputerId));

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.ComputerId), Times.Once());
        }

        [Fact]
        public async Task When_invoke_get_async_with_inventoryNumber_parameter_it_should_invoke_get_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<ComputerDto>(_computer)).Returns(_computerDto);
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.InventoryNumber)).ReturnsAsync(_computer);

            //Act
            var existingComputerDto = await _computerService.GetAsync(_computer.InventoryNumber);

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.InventoryNumber), Times.Once());
            _computerDto.Should().NotBeNull();
            _computerDto.InventoryNumber.ShouldBeEquivalentTo(_computer.InventoryNumber);
        }

        [Fact]
        public async Task When_invoke_get_async_with_inventoryNumber_parameter_and_computer_do_not_exists_it_should_invoke_get_async_on_repository()
        {
            //Arrange
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.InventoryNumber)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _computerService.GetAsync(_computer.InventoryNumber));

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.InventoryNumber), Times.Once());
        }

        [Fact]
        public async Task Add_computer_async_should_invoke_add_computer_async_on_repository()
        {
            //Act
            await _computerService.AddAsync(Guid.NewGuid(), _computer.InventoryNumber, _computer.IpAddress, _computer.RoomId);

            //Assert
            _computerRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Computer>()), Times.Once());
        }

        [Fact]
        public async Task Add_computer_async_and_computer_already_exists_should_not_invoke_add_computer_async_on_repository()
        {
            //Arrange
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.InventoryNumber)).ReturnsAsync((_computer));

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _computerService.AddAsync(_computer.ComputerId, _computer.InventoryNumber, 
                _computer.IpAddress, _computer.RoomId));

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.InventoryNumber), Times.Once());
            _computerRepositoryMock.Verify(x => x.AddAsync(_computer), Times.Never());
        }

        [Fact]
        public async Task Remove_computer_async_should_invoke_remove_computer_async_on_repository()
        {
            //Arrange
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.ComputerId)).ReturnsAsync(_computer);

            //Act
            await _computerService.RemoveAsync(_computer.ComputerId);

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.ComputerId), Times.Once());
            _computerRepositoryMock.Verify(x => x.RemoveAsync(_computer), Times.Once());
        }

        [Fact]
        public async Task Remove_computer_async_and_computer_does_not_exists_should_not_invoke_remove_async_on_repository()
        {
            //Arrange
            _computerRepositoryMock.Setup(x => x.GetAsync(_computer.ComputerId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _computerService.RemoveAsync(_computer.ComputerId));
            

            //Assert
            _computerRepositoryMock.Verify(x => x.GetAsync(_computer.ComputerId), Times.Once());
            _computerRepositoryMock.Verify(x => x.RemoveAsync(_computer), Times.Never());         
        }
        
    }
}