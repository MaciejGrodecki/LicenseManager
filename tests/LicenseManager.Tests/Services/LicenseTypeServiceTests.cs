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
    public class LicenseTypeServiceTests
    {
        private readonly Mock<ILicenseTypeRepository> _licenseTypeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ILicenseTypeService _licenseTypeService;
        private readonly LicenseType _licenseType = new LicenseType(Guid.NewGuid(), "OEM");
        private readonly ISet<LicenseType> _licenseTypes = new HashSet<LicenseType>();
        private readonly LicenseTypeDto _licenseTypeDto;
        private readonly ISet<LicenseTypeDto> _licenseTypesDto = new HashSet<LicenseTypeDto>();

        public LicenseTypeServiceTests()
        {
            _licenseTypeRepositoryMock = new Mock<ILicenseTypeRepository>();
            _mapperMock = new Mock<IMapper>();
            _licenseTypeService = new LicenseTypeService(_licenseTypeRepositoryMock.Object, _mapperMock.Object);
            _licenseTypeDto = new LicenseTypeDto{ LicenseTypeId = _licenseType.LicenseTypeId, Name = _licenseType.Name };
            _licenseTypes.Add(_licenseType);
            _licenseTypesDto.Add(_licenseTypeDto);
        }

        [Fact]
        public async Task When_invoking_browse_async_it_should_invoke_browse_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<IEnumerable<LicenseTypeDto>>(_licenseTypes)).Returns(_licenseTypesDto);
            _licenseTypeRepositoryMock.Setup(x => x.BrowseAsync()).ReturnsAsync(_licenseTypes);

            //Act
            var exististingLicenseTypesDto = await _licenseTypeService.BrowseAsync();

            //Assert
            _licenseTypeRepositoryMock.Verify( x => x.BrowseAsync(), Times.Once());
            _licenseTypesDto.Should().NotBeNull();
            _licenseTypesDto.Count.Should().Be(1);
        }

        [Fact]
        public async Task When_invoke_get_async_with_licenseTypeId_parameter_it_should_invoke_get_async_on_licenseType_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<LicenseTypeDto>(_licenseType)).Returns(_licenseTypeDto);
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.LicenseTypeId)).ReturnsAsync(_licenseType);

            //Act
            var existingLicenseTypeDto = await _licenseTypeService.GetAsync(_licenseType.LicenseTypeId);

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.LicenseTypeId), Times.Once());
            _licenseTypeDto.Should().NotBeNull();
            _licenseTypeDto.Name.ShouldAllBeEquivalentTo(_licenseType.Name);
        }

        [Fact]
        public async Task When_invoke_get_async_with_licenseTypeId_parameter_and_license_type_do_not_exists_it_should_invoke_get_async_on_licenseType_repository()
        {
            //Arrange
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.LicenseTypeId)).ReturnsAsync(() => null);

            //Act
            await _licenseTypeService.GetAsync(_licenseType.LicenseTypeId);

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.LicenseTypeId), Times.Once());
        }

        [Fact]
        public async Task When_invoke_get_async_with_name_parameter_it_should_invoke_get_async_on_room_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<LicenseTypeDto>(_licenseType)).Returns(_licenseTypeDto);
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.Name)).ReturnsAsync(_licenseType);

            //Act
            var existingLicenseTypeDto = await _licenseTypeService.GetAsync(_licenseType.Name);

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.Name), Times.Once());
            _licenseTypeDto.Should().NotBeNull();
            _licenseTypeDto.Name.ShouldBeEquivalentTo(_licenseType.Name);
        }

        [Fact]
        public async Task When_invoke_get_async_with_name_parameter_and_license_type_do_not_exists_it_should_invoke_get_async_on_licenseType_repository()
        {
            //Arrange
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.Name)).ReturnsAsync(() => null);

            //Act
            await _licenseTypeService.GetAsync(_licenseType.Name);

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.Name), Times.Once());
        }

        [Fact]
        public async Task Add_licenseType_async_should_invoke_add_licenseType_async_on_licenseType_repository()
        {
            //Act
            await _licenseTypeService.AddAsync("OEM");

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<LicenseType>()), Times.Once());
        }

        [Fact]
        public async Task Add_licenseType_async_and_licenseType_with_name_already_exists_should_not_invoke_add_licenseType_async_on_licenseType_repository()
        {
            //Arrange
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.Name)).ReturnsAsync((_licenseType));

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _licenseTypeService.AddAsync(_licenseType.Name));

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.Name), Times.Once());
            _licenseTypeRepositoryMock.Verify(x => x.AddAsync(_licenseType), Times.Never());
        }

        [Fact]
        public async Task Remove_licenseType_async_should_invoke_remove_licenseType_async_on_licenseType_repository()
        {
            //Arrange
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.LicenseTypeId)).ReturnsAsync(_licenseType);

            //Act
            await _licenseTypeService.RemoveAsync(_licenseType.LicenseTypeId);

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.LicenseTypeId), Times.Once());
            _licenseTypeRepositoryMock.Verify(x => x.RemoveAsync(_licenseType), Times.Once());
        }

        [Fact]
        public async Task Remove_licenseType_async_and_licenseType_does_not_exists_should_not_invoke_remove_licenseType_async_on_licenseType_repository()
        {
            //Arrange
            _licenseTypeRepositoryMock.Setup(x => x.GetAsync(_licenseType.LicenseTypeId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<Exception>(async () => await _licenseTypeService.RemoveAsync(_licenseType.LicenseTypeId));
            

            //Assert
            _licenseTypeRepositoryMock.Verify(x => x.GetAsync(_licenseType.LicenseTypeId), Times.Once());
            _licenseTypeRepositoryMock.Verify(x => x.RemoveAsync(_licenseType), Times.Never());         
        }
    }
}