using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using LicenseManager.Api;
using Xunit;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using LicenseManager.Infrastructure.DTO;
using FluentAssertions;
using System.Net;

namespace LicenseManager.Tests.EndToEnd.Controllers
{
    public class RoomsControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public RoomsControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Fetching_rooms_should_return_not_null_collection()
        {
            //Act
            var response = await _client.GetAsync("rooms");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<RoomDto>>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            rooms.Should().NotBeNull();
            rooms.Should().BeOfType(typeof(List<RoomDto>));
        }

        [Fact]
        public async Task Fetching_room_with_roomId_should_return_room_object()
        {
            //Act
            var response = await _client.GetAsync("rooms");
            var content = await response.Content.ReadAsStringAsync();
        }
    }
}