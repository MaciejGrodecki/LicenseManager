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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IUserService _userService;
        private readonly User _user = new User("Jan", "Kowalski");
        private readonly ISet<User> _users = new HashSet<User>();
        private readonly UserDto _userDto;
        private readonly ISet<UserDto> _usersDto = new HashSet<UserDto>();

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
            _userDto = new UserDto{ UserId = _user.UserId, Name = _user.Name, Surname = _user.Surname };
            _users.Add(_user);
            _usersDto.Add(_userDto);
        }

        [Fact]
        public async Task When_invoking_browse_async_it_should_invoke_browse_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<IEnumerable<UserDto>>(_users)).Returns(_usersDto);
            _userRepositoryMock.Setup(x => x.BrowseAsync()).ReturnsAsync(_users);

            //Act
            var exististingUsersDto = await _userService.BrowseAsync();

            //Assert
            _userRepositoryMock.Verify( x => x.BrowseAsync(), Times.Once());
            _usersDto.Should().NotBeNull();
            _usersDto.Count.Should().Be(1);
        }

        [Fact]
        public async Task When_invoke_get_async_with_userId_parameter_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<UserDto>(_user)).Returns(_userDto);
            _userRepositoryMock.Setup(x => x.GetAsync(_user.UserId)).ReturnsAsync(_user);

            //Act
            var existingRoomDto = await _userService.GetAsync(_user.UserId);

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.UserId), Times.Once());
            _userDto.Should().NotBeNull();
            _userDto.Name.ShouldBeEquivalentTo(_user.Name);
        }

        [Fact]
        public async Task When_invoke_get_async_with_userId_parameter_and_user_do_not_exists_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            _userRepositoryMock.Setup(x => x.GetAsync(_user.UserId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _userService.GetAsync(_user.UserId));

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.UserId), Times.Once());
        }

        [Fact]
        public async Task When_invoke_get_async_with_name_and_surname_parameters_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<UserDto>(_user)).Returns(_userDto);
            _userRepositoryMock.Setup(x => x.GetAsync(_user.Name, _user.Surname)).ReturnsAsync(_user);

            //Act
            var existingUserDto = await _userService.GetAsync(_user.Name, _user.Surname);

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.Name, _user.Surname), Times.Once());
            _userDto.Should().NotBeNull();
            _userDto.Name.ShouldBeEquivalentTo(_user.Name);
            _userDto.Surname.ShouldBeEquivalentTo(_user.Surname);
        }

        [Fact]
        public async Task When_invoke_get_async_with_name_and_surname_parameters_and_user_do_not_exists_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            _userRepositoryMock.Setup(x => x.GetAsync(_user.Name, _user.Surname)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _userService.GetAsync(_user.Name, _user.Surname));

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.Name, _user.Surname), Times.Once());
        }

        [Fact]
        public async Task Add_user_async_should_invoke_add_user_async_on_user_repository()
        {
            //Act
            await _userService.AddAsync(_user.Name, _user.Surname);

            //Assert
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public async Task Add_user_async_and_user_with_name_and_surname_already_exists_should_not_invoke_add_user_async_on_user_repository()
        {
            //Arrange
            _userRepositoryMock.Setup(x => x.GetAsync(_user.Name, _user.Surname)).ReturnsAsync((_user));

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _userService.AddAsync(_user.Name, _user.Surname));

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.Name, _user.Surname), Times.Once());
            _userRepositoryMock.Verify(x => x.AddAsync(_user), Times.Never());
        }

        [Fact]
        public async Task Remove_user_async_should_invoke_remove_user_async_on_user_repository()
        {
            //Arrange
            _userRepositoryMock.Setup(x => x.GetAsync(_user.UserId)).ReturnsAsync(_user);

            //Act
            await _userService.RemoveAsync(_user.UserId);

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.UserId), Times.Once());
            _userRepositoryMock.Verify(x => x.RemoveAsync(_user), Times.Once());
        }

        [Fact]
        public async Task Remove_room_async_and_room_does_not_exists_should_not_invoke_remove_room_async_on_room_repository()
        {
            //Arrange
            _userRepositoryMock.Setup(x => x.GetAsync(_user.UserId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _userService.RemoveAsync(_user.UserId));
            

            //Assert
            _userRepositoryMock.Verify(x => x.GetAsync(_user.UserId), Times.Once());
            _userRepositoryMock.Verify(x => x.RemoveAsync(_user), Times.Never());         
        }
    }
}