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
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IRoomService _roomService;
        private readonly Room _room = new Room(Guid.NewGuid(), "A-11");
        private readonly ISet<Room> _rooms =  new HashSet<Room>();
        private readonly RoomDto _roomDto;
        private readonly ISet<RoomDto> _roomsDto = new HashSet<RoomDto>();
        public RoomServiceTests()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _mapperMock = new Mock<IMapper>();
            _roomService = new RoomService(_roomRepositoryMock.Object, _mapperMock.Object);
            _roomDto = new RoomDto { RoomId = _room.RoomId, Name = _room.Name};
            _rooms.Add(_room);
            _roomsDto.Add(_roomDto);
        }

        [Fact]
        public async Task When_invoking_browse_async_it_should_invoke_browse_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<IEnumerable<RoomDto>>(_rooms)).Returns(_roomsDto);
            _roomRepositoryMock.Setup(x => x.BrowseAsync()).ReturnsAsync(_rooms);

            //Act
            var exististingRoomsDto = await _roomService.BrowseAsync();

            //Assert
            _roomRepositoryMock.Verify( x => x.BrowseAsync(), Times.Once());
            _roomsDto.Should().NotBeNull();
            _roomsDto.Count.Should().Be(1);
        }

        [Fact]
        public async Task When_invoke_get_async_with_roomId_parameter_it_should_invoke_get_async_on_room_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<RoomDto>(_room)).Returns(_roomDto);
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.RoomId)).ReturnsAsync(_room);

            //Act
            var existingRoomDto = await _roomService.GetAsync(_room.RoomId);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.RoomId), Times.Once());
            _roomDto.Should().NotBeNull();
            _roomDto.Name.ShouldBeEquivalentTo(_room.Name);
        }

        [Fact]
        public async Task When_invoke_get_async_with_roomId_parameter_and_room_do_not_exists_it_should_invoke_get_async_on_room_repository()
        {
            //Arrange
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.RoomId)).ReturnsAsync(() => null);

            //Act
            var room = await _roomService.GetAsync(_room.RoomId);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.RoomId), Times.Once());
        }

        [Fact]
        public async Task When_invoke_get_async_with_name_parameter_it_should_invoke_get_async_on_room_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<RoomDto>(_room)).Returns(_roomDto);
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.Name)).ReturnsAsync(_room);

            //Act
            var existingRoomDto = await _roomService.GetAsync(_room.Name);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.Name), Times.Once());
            _roomDto.Should().NotBeNull();
            _roomDto.Name.ShouldBeEquivalentTo(_room.Name);
        }

        [Fact]
        public async Task When_invoke_get_async_with_name_parameter_and_room_do_not_exists_it_should_invoke_get_async_on_room_repository()
        {
            //Arrange
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.Name)).ReturnsAsync(() => null);

            //Act
            var room = await _roomService.GetAsync(_room.Name);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.Name), Times.Once());
        }

        [Fact]
        public async Task Add_room_async_should_invoke_add_room_async_on_room_repository()
        {
            //Act
            await _roomService.AddAsync("A-14");

            //Assert
            _roomRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Room>()), Times.Once());
        }

        [Fact]
        public async Task Add_room_async_and_room_with_name_already_exists_should_not_invoke_add_room_async_on_room_repository()
        {
            //Arrange
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.Name)).ReturnsAsync((_room));

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _roomService.AddAsync(_room.Name));

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.Name), Times.Once());
            _roomRepositoryMock.Verify(x => x.AddAsync(_room), Times.Never());
        }

        [Fact]
        public async Task Remove_room_async_should_invoke_remove_room_async_on_room_repository()
        {
            //Arrange
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.RoomId)).ReturnsAsync(_room);

            //Act
            await _roomService.RemoveAsync(_room.RoomId);

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.RoomId), Times.Once());
            _roomRepositoryMock.Verify(x => x.RemoveAsync(_room), Times.Once());
        }

        [Fact]
        public async Task Remove_room_async_and_room_does_not_exists_should_not_invoke_remove_room_async_on_room_repository()
        {
            //Arrange
            _roomRepositoryMock.Setup(x => x.GetAsync(_room.RoomId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _roomService.RemoveAsync(_room.RoomId));
            

            //Assert
            _roomRepositoryMock.Verify(x => x.GetAsync(_room.RoomId), Times.Once());
            _roomRepositoryMock.Verify(x => x.RemoveAsync(_room), Times.Never());         
        }
    }
}