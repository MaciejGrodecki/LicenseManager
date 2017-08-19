using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.Repositories;
using Xunit;

namespace LicenseManager.Tests.Repositories
{
    public class InMemoryUserRepositoryTests
    {
        private readonly IUserRepository _repository;
        private static readonly User _user = new User("Jan", "Kowalski");

        public InMemoryUserRepositoryTests()
        {
            _repository = new InMemoryUserRepository();
        }

        [Fact]
        public async Task When_adding_new_user_it_should_be_added_correctly_to_the_collection()
        {
            //Act
            await _repository.AddAsync(_user);

            //Assert
            var existingUser = await _repository.GetAsync(_user.UserId);
            Assert.Equal(_user, existingUser);
        }

        [Fact]
        public async Task Invoking_BrowseAsync_should_return_collection_of_user_objects()
        {
            //Act
            var users = await _repository.BrowseAsync();

            //Assert
            Assert.IsType<HashSet<User>>(users);
        }

        [Fact]
        public async Task Invoking_GetAsync_with_userId_parameter_should_return_user_object()
        {
            //Arrange
            await _repository.AddAsync(_user);

            //Act
            var existingUser = await _repository.GetAsync(_user.UserId);

            //Assert
            Assert.IsType(typeof(User), existingUser);
            Assert.Equal(_user, existingUser);
        }

        [Fact]
        public async Task Invoking_GetAsync_with_name_and_surname_parameters_should_return_user_object()
        {
            //Arrange
            await _repository.AddAsync(_user);

            //Act
            var existingUser = await _repository.GetAsync(_user.Name, _user.Surname);

            //Assert
            Assert.IsType(typeof(User), existingUser);
            Assert.Equal(_user, existingUser);
        }

        [Fact]
        public async Task Invoking_RemoveAsync_should_remove_user_object_from_collection()
        {
            //Arrange
            await _repository.AddAsync(_user);

            //Act
            await _repository.RemoveAsync(_user);
            var user = await _repository.GetAsync(_user.UserId);

            //Assert
            Assert.Null(user);
        }

    }
}