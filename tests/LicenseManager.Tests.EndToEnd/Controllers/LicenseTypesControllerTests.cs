using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using LicenseManager.Infrastructure.Commands.LicenseType;
using LicenseManager.Infrastructure.DTO;
using Newtonsoft.Json;
using Xunit;

namespace LicenseManager.Tests.EndToEnd.Controllers
{
    public class LicenseTypesControllerTests : ControllerTestsBase
    {
         
        [Fact]
        public async Task Fetching_licenseTypes_should_return_not_null_collection()
        {
            //Act
            var response = await Client.GetAsync("licenseTypes");
            var content = await response.Content.ReadAsStringAsync();
            var licenseTypes = JsonConvert.DeserializeObject<IEnumerable<LicenseTypeDto>>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            licenseTypes.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Fetching_licenseTypes_with_name_should_return_licenseTypeDto_object()
        {
            //Act
            var response = await Client.GetAsync($"licenseTypes/OEM");
            var content = await response.Content.ReadAsStringAsync();
            var licenseType = JsonConvert.DeserializeObject<LicenseTypeDto>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            licenseType.Should().NotBeNull();
            licenseType.Should().BeOfType(typeof(LicenseTypeDto));
        }
/*
        [Fact]
        public async Task Fetching_licenseTypes_with_name_and_licenseTypes_does_not_exist_should_return_NotFound()
        {
            //Act
            var response = await Client.GetAsync($"licenseTypes/wrong");
            var content = await response.Content.ReadAsStringAsync();
            var licenseType = JsonConvert.DeserializeObject<LicenseTypeDto>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Fetching_licenseTypes_with_licenseTypeId_should_return_licenseType_object()
        {
            //Arrange
            var existingLicenseTypeResponse = await Client.GetAsync($"licenseTypes/OEM");
            var existingLicenseTypeContent = await existingLicenseTypeResponse.Content.ReadAsStringAsync();
            var existingLicenseType = JsonConvert.DeserializeObject<LicenseTypeDto>(existingLicenseTypeContent);
            //Act
            var response = await Client.GetAsync($"licenseTypes/{existingLicenseType.LicenseTypeId}");
            var content = await response.Content.ReadAsStringAsync();
            var licenseType = JsonConvert.DeserializeObject<LicenseTypeDto>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Fetching_licenseType_with_licenseTypeId_and_licenseType_does_not_exist_should_return_NotFound()
        {
            //Act
            var response = await Client.GetAsync($"licenseTypes/{Guid.NewGuid()}:Guid");
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_unique_licenseType_name_should_be_created()
        {
            //Arrange
            var command = new AddLicenseType
            {
                Name = "PLK"
            };

            //Act
            var payload = GetPayload(command);
            var response = await Client.PostAsync("licenseTypes", payload);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Given_already_exist_licenseType_name_should_not_be_created_and_throw_exception()
        {
            //Arrange
            var command = new AddLicenseType
            {
                Name = "OEM"
            };

            //Act
            var payload = GetPayload(command);
            HttpResponseMessage response = new HttpResponseMessage();

            //Assert
            await Assert.ThrowsAnyAsync<Exception>(
                async() => response =  await Client.PostAsync("licenseTypes", payload));
        }

        [Fact]
        public async Task Given_unique_licenseType_name_should_updated()
        {
            //Arrange
            var existingResponse = await Client.GetAsync($"licenseTypes/box");
            var existingContent = await existingResponse.Content.ReadAsStringAsync();
            var existingLicenseType = JsonConvert.DeserializeObject<LicenseTypeDto>(existingContent);
            var command = new UpdateLicenseType
            {
                LicenseTypeId = existingLicenseType.LicenseTypeId,
                Name = "New"
            };

            //Act
            var payload = GetPayload(command);
            var response = await Client.PutAsync($"licenseTypes/{command.LicenseTypeId}", payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }*/
    }
}