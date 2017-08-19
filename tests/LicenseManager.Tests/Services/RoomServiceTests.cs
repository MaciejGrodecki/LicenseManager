using System;
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
        [Fact]
        public async Task Add_room_async_should_invoke_add_room_async_on_room_repository()
        {
            //Arrange
            var roomRepositoryMock = new Mock<IRoomRepository>();
            var mapperMock = new Mock<IMapper>();
            var roomService = new RoomService(roomRepositoryMock.Object, mapperMock.Object);

            //Act
            await roomService.AddAsync("A-14");

            //Assert
            roomRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Room>()), Times.Once());
        }

        [Fact]
        public async Task When_invoke_get_async_it_should_invoke_get_async_on_room_repository()
        {
            //Arrange
            var room = new Room("A-15");
            var roomDto = new RoomDto
            {
                RoomId = room.RoomId,
                Name = room.Name
            };
            var roomRepositoryMock = new Mock<IRoomRepository>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<RoomDto>(room)).Returns(roomDto);
            var roomService = new RoomService(roomRepositoryMock.Object, mapperMock.Object);
            roomRepositoryMock.Setup(x => x.GetAsync(room.RoomId)).ReturnsAsync(room);

            //Act
            var existingRoomDto = await roomService.GetAsync(room.RoomId);

            //Assert
            roomRepositoryMock.Verify(x => x.GetAsync(room.RoomId), Times.Once());
            roomDto.Should().NotBeNull();
            roomDto.Name.ShouldBeEquivalentTo(room.Name);

        }
    }
}