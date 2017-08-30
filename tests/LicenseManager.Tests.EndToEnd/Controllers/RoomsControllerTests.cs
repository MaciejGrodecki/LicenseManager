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
using System;
using LicenseManager.Infrastructure.Commands.Room;
using System.Text;

namespace LicenseManager.Tests.EndToEnd.Controllers
{
    public class RoomsControllerTests : ControllerTestsBase
    {
 
        [Fact]
        public async Task Fetching_rooms_should_return_not_null_collection()
        {
            //Act
            var response = await Client.GetAsync("rooms");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<RoomDto>>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            rooms.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Fetching_room_with_name_should_return_roomDto_object()
        {
            //Act
            var response = await Client.GetAsync($"rooms/b-01");
            var content = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<RoomDto>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
            room.Should().NotBeNull();
            room.Should().BeOfType(typeof(RoomDto));
        }

        [Fact]
        public async Task Fetching_room_with_name_and_room_does_not_exist_should_return_NotFound()
        {
            //Act
            var response = await Client.GetAsync($"rooms/b-55");
            var content = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<RoomDto>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Fetching_room_with_roomId_should_return_room_object()
        {
            //Arrange
            var existingRoomResponse = await Client.GetAsync($"rooms/b-01");
            var existingRoomContent = await existingRoomResponse.Content.ReadAsStringAsync();
            var existingRoom = JsonConvert.DeserializeObject<RoomDto>(existingRoomContent);
            //Act
            var response = await Client.GetAsync($"rooms/{existingRoom.RoomId}");
            var content = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<RoomDto>(content);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Fetching_room_with_roomId_and_room_does_not_exist_should_return_NotFound()
        {
            //Act
            var response = await Client.GetAsync($"rooms/{Guid.NewGuid()}:Guid");
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Given_unique_room_name_should_be_created()
        {
            //Arrange
            var command = new AddRoom
            {
                Name = "B-02"
            };

            //Act
            var payload = GetPayload(command);
            var response = await Client.PostAsync("rooms", payload);

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Given_already_exist_room_name_should_not_be_created_and_throw_exception()
        {
            //Arrange
            var command = new AddRoom
            {
                Name = "B-01"
            };

            //Act
            var payload = GetPayload(command);
            HttpResponseMessage response = new HttpResponseMessage();

            //Assert
            await Assert.ThrowsAnyAsync<Exception>(
                async() => response =  await Client.PostAsync("rooms", payload));
        }

        [Fact]
        public async Task Given_unique_room_name_should_updated_room()
        {
            //Arrange
            var existingResponse = await Client.GetAsync($"rooms/b-01");
            var existingContent = await existingResponse.Content.ReadAsStringAsync();
            var existingRoom = JsonConvert.DeserializeObject<RoomDto>(existingContent);
            var command = new UpdateRoom
            {
                RoomId = existingRoom.RoomId,
                Name = "B-99"
            };

            //Act
            var payload = GetPayload(command);
            var response = await Client.PutAsync($"rooms/{command.RoomId}", payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Given_already_existing_room_name_should_not_updated_room()
        {
            //Arrange
            HttpResponseMessage response = new HttpResponseMessage();
            response = await Client.GetAsync($"rooms/b-01");
            var existingContent = await response.Content.ReadAsStringAsync();
            var existingRoom = JsonConvert.DeserializeObject<RoomDto>(existingContent);
            var command = new UpdateRoom
            {
                RoomId = existingRoom.RoomId,
                Name = "B-01"
            };
            var payload = GetPayload(command);

            //Act
            await Assert.ThrowsAnyAsync<Exception>(
                async() => response =  await Client.PutAsync($"rooms/{command.RoomId}", payload));
            

            //Assert
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Delete_room_who_exists_should_delete_it()
        {
            //Arrange
            var existingResponse = await Client.GetAsync($"rooms/b-01");
            var existingContent = await existingResponse.Content.ReadAsStringAsync();
            var existingRoom = JsonConvert.DeserializeObject<RoomDto>(existingContent);

            //Act
            var response = await Client.DeleteAsync($"rooms/{existingRoom.RoomId}");

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
        }

        
    }
}