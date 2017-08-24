using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LicenseManager.Core.Domain;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.Repositories;
using Xunit;

namespace LicenseManager.Tests.Repositories
{
    public class InMemoryLicenseRepositoryTests
    {
        private readonly ILicenseRepository _repository;

        private static readonly LicenseType _licenseType = new LicenseType(Guid.NewGuid(), "OEM");
        private static readonly License _license = new License(
            "MS Office 2010", 10, _licenseType.LicenseTypeId, DateTime.Now);
        public InMemoryLicenseRepositoryTests()
        {
            //Arrange
            _repository = new InMemoryLicenseRepository();
            
        }


        [Fact]
        public async Task When_adding_new_license_it_should_be_added_correctly_to_the_collection()
        {

            //Act
            await _repository.AddAsync(_license);

            //Assert
            var existingLicense = await _repository.GetAsync(_license.LicenseId);
            Assert.Equal(_license, existingLicense);
        }

        [Fact]
        public async Task Invoking_BrowseAsync_should_return_collection_of_license_objects()
        {

            //Act
            var licenses = await _repository.BrowseAsync();

            //Assert
            Assert.IsType<HashSet<License>>(licenses);
        }
        
        [Fact]
        public async Task Invoking_GetAsync_with_licenseId_parameter_should_return_license_object()
        {
            //Arrange
            await _repository.AddAsync(_license);
            
            //Act
            var existingLicense = await _repository.GetAsync(_license.LicenseId);

            //Assert
            Assert.IsType(typeof(License), existingLicense);
            Assert.Equal(_license, existingLicense);
        } 
        

        [Fact]
        public async Task Invoking_RemoveAsync_should_remove_license_object_from_collection()
        {
            //Arrange
            await _repository.AddAsync(_license);

            //Act
            await _repository.RemoveAsync(_license);
            var license = await _repository.GetAsync(_license.LicenseId);
            
            //Assert
            Assert.Null(license);
        }
    }
}