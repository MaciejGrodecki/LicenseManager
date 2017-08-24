using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.Repositories;
using Xunit;

namespace LicenseManager.Tests.Repositories
{
    public class InMemoryLicenseTypeRepositoryTests
    {
        private readonly ILicenseTypeRepository _repository;
        private static readonly LicenseType _licenseType = new LicenseType(Guid.NewGuid(), "OEM");
        public InMemoryLicenseTypeRepositoryTests()
        {
            //Arrange
            _repository = new InMemoryLicenseTypeRepository();
            
        }


        [Fact]
        public async Task When_adding_new_license_type_it_should_be_added_correctly_to_the_collection()
        {

            //Act
            await _repository.AddAsync(_licenseType);

            //Assert
            var existingLicenseType = await _repository.GetAsync(_licenseType.Name);
            Assert.Equal(_licenseType, existingLicenseType);
        }

        [Fact]
        public async Task Invoking_BrowseAsync_should_return_collection_of_license_type_objects()
        {

            //Act
            var licenseTypes = await _repository.BrowseAsync();

            //Assert
            Assert.IsType<HashSet<LicenseType>>(licenseTypes);
        }
        
        [Fact]
        public async Task Invoking_GetAsync_with_licenseTypeId_parameter_should_return_licenseType_object()
        {
            //Arrange
            await _repository.AddAsync(_licenseType);
            
            //Act
            var existingLicenseType = await _repository.GetAsync(_licenseType.LicenseTypeId);

            //Assert
            Assert.IsType(typeof(LicenseType), existingLicenseType);
            Assert.Equal(_licenseType, existingLicenseType);
        } 
        
        [Fact]
        public async Task Invoking_GetAsync_with_name_parameter_should_return_licenseType_object()
        {
            //Arrange
            await _repository.AddAsync(_licenseType);

            //Act
            var existingLicenseType = await _repository.GetAsync(_licenseType.Name);

            //Assert
            Assert.Equal(_licenseType, existingLicenseType);
            Assert.IsType(typeof(LicenseType), existingLicenseType);
            
        }

        [Fact]
        public async Task Invoking_RemoveAsync_should_remove_room_object_from_collection()
        {
            //Arrange
            await _repository.AddAsync(_licenseType);

            //Act
            await _repository.RemoveAsync(_licenseType);
            var licenseType = await _repository.GetAsync(_licenseType.Name);
            
            //Assert
            Assert.Null(licenseType);
        }
    }
}