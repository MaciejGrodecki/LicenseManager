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
    public class LicenseServiceTests
    {
        private static readonly LicenseType _licenseType = new LicenseType(Guid.NewGuid(), "OEM");
        private readonly Mock<ILicenseRepository> _licenseRepositoryMock;
        private readonly Mock<IComputerRepository> _computerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private ILicenseService _licenseService;

        private readonly License _license = new License("MS Office 2017", 10, _licenseType.LicenseTypeId,
            DateTime.UtcNow, "123A-345B-678C");
        private readonly LicenseDto _licenseDto;
        private readonly ISet<License> _licenses = new HashSet<License>();
        private readonly ISet<LicenseDto> _licensesDto = new HashSet<LicenseDto>();

        public LicenseServiceTests()
        {
            _licenseRepositoryMock = new Mock<ILicenseRepository>();
            _computerRepositoryMock = new Mock<IComputerRepository>();
            _mapperMock = new Mock<IMapper>();
            _licenseService = new LicenseService(_licenseRepositoryMock.Object, _computerRepositoryMock.Object, _mapperMock.Object);
            _licenseDto = new LicenseDto
            {
                LicenseId = _license.LicenseId,
                Name = _license.Name,
                Count = _license.Count,
                LicenseTypeId = _license.LicenseTypeId,
                BuyDate = _license.BuyDate
            };
            _licenses.Add(_license);
            _licensesDto.Add(_licenseDto);
        }

        [Fact]
        public async Task When_invoking_browse_async_it_should_invoke_browse_async_on_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<IEnumerable<LicenseDto>>(_license)).Returns(_licensesDto);
            _licenseRepositoryMock.Setup(x => x.BrowseAsync()).ReturnsAsync(_licenses);

            //Act
            var exististingLicenseDto = await _licenseService.BrowseAsync();

            //Assert
            _licenseRepositoryMock.Verify( x => x.BrowseAsync(), Times.Once());
            _licenseDto.Should().NotBeNull();
            _licenseDto.Name.ShouldBeEquivalentTo(_license.Name);
            _licenseDto.Count.ShouldBeEquivalentTo(_license.Count);
            _licenseDto.LicenseTypeId.ShouldBeEquivalentTo(_license.LicenseTypeId);
            _licenseDto.BuyDate.ShouldBeEquivalentTo(_license.BuyDate);
        }
        
        [Fact]
        public async Task When_invoke_get_async_with_licenseId_parameter_it_should_invoke_get_async_on_license_repository()
        {
            //Arrange
            _mapperMock.Setup(x => x.Map<LicenseDto>(_license)).Returns(_licenseDto);
            _licenseRepositoryMock.Setup(x => x.GetAsync(_license.LicenseId)).ReturnsAsync(_license);

            //Act
            var existingLicenseDto = await _licenseService.GetAsync(_license.LicenseId);

            //Assert
            _licenseRepositoryMock.Verify(x => x.GetAsync(_license.LicenseId), Times.Once());
            _licenseDto.Should().NotBeNull();
            _licenseDto.Should().NotBeNull();
            _licenseDto.Name.ShouldBeEquivalentTo(_license.Name);
            _licenseDto.Count.ShouldBeEquivalentTo(_license.Count);
            _licenseDto.LicenseTypeId.ShouldBeEquivalentTo(_license.LicenseTypeId);
            _licenseDto.BuyDate.ShouldBeEquivalentTo(_license.BuyDate);
        }

        [Fact]
        public async Task When_invoke_get_async_with_licenseId_parameter_and_license_do_not_exists_it_should_invoke_get_async_on_license_repository()
        {
            //Arrange
            _licenseRepositoryMock.Setup(x => x.GetAsync(_license.LicenseId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<LicenseManagerException>(async () => await _licenseService.GetAsync(_license.LicenseId));

            //Assert
            _licenseRepositoryMock.Verify(x => x.GetAsync(_license.LicenseId), Times.Once());
        }

        [Fact]
        public async Task Add_license_async_should_invoke_add_license_async_on_license_repository()
        {
            //Act
            await _licenseService.AddAsync(_license.Name, _license.Count, _license.LicenseTypeId, _license.BuyDate, _license.SerialNumber);

            //Assert
            _licenseRepositoryMock.Verify(x => x.AddAsync(It.IsAny<License>()), Times.Once());
        }

        [Fact]
        public async Task Remove_license_async_should_invoke_license_room_async_on_license_repository()
        {
            //Arrange
            _licenseRepositoryMock.Setup(x => x.GetAsync(_license.LicenseId)).ReturnsAsync(_license);

            //Act
            await _licenseService.RemoveAsync(_license.LicenseId);

            //Assert
            _licenseRepositoryMock.Verify(x => x.GetAsync(_license.LicenseId), Times.Once());
            _licenseRepositoryMock.Verify(x => x.RemoveAsync(_license), Times.Once());
        }

        [Fact]
        public async Task Remove_license_async_and_license_does_not_exists_should_not_invoke_remove_license_async_on_license_repository()
        {
            //Arrange
            _licenseRepositoryMock.Setup(x => x.GetAsync(_license.LicenseId)).ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<LicenseManagerException>(async () => await _licenseService.RemoveAsync(_license.LicenseId));
            

            //Assert
            _licenseRepositoryMock.Verify(x => x.GetAsync(_license.LicenseId), Times.Once());
            _licenseRepositoryMock.Verify(x => x.RemoveAsync(_license), Times.Never());         
        }


    }
}