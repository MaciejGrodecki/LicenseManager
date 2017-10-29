using System;
using LicenseManager.Core.Domain;
using Machine.Specifications;

namespace LicenseManager.Tests.Domain.Domain
{
    public abstract class ComputerTests : DomainException
    {
        protected static Guid ComputerId = Guid.NewGuid();
        protected static string InventoryNumber = "US-IN/Z/50-W";
        protected static string IpAddress = "192.168.1.1";
        protected static Guid RoomId = Guid.NewGuid();
        protected static User User = new User(Guid.NewGuid(), "Anna", "Kowalska");
        protected static Computer Computer;

        protected static void Initialize()
        {
            Computer = new Computer(ComputerId, InventoryNumber, 
                IpAddress, RoomId);
        }

        protected static void InitializeWithoutRoom()
        {
            Computer = new Computer(ComputerId, InventoryNumber, 
                IpAddress);
        }
    }

    [Subject("Computer initialize")]
    public class when_create_computer : ComputerTests
    {
        Establish context = () => {};
        Because of = () => Initialize();

        It should_not_be_null = () => Computer.ShouldNotBeNull();
        It should_has_assigned_inventoryNumber = () => Computer.InventoryNumber.ShouldEqual(InventoryNumber);
        It should_has_assigned_ipAddress = () => Computer.IpAddress.ShouldEqual(IpAddress);
        It should_has_assinged_roomId = () => Computer.RoomId.ShouldEqual(RoomId);
    }

    [Subject("Computer initialize without roomId")]
    public class when_create_computer_without_roomId : ComputerTests
    {
        Establish context = () => InitializeWithoutRoom();
        Because of = () => Exception = Catch.Exception(() => Initialize());

        It should_not_be_null = () => Computer.ShouldNotBeNull();
        It should_has_assigned_inventoryNumber = () => Computer.InventoryNumber.ShouldEqual(InventoryNumber);
        It should_has_assigned_ipAddress = () => Computer.IpAddress.ShouldEqual(IpAddress);
    }

    [Subject("Add room to computer")]
    public class when_add_room_to_computer : ComputerTests
    {
        Establish context = () => {};
        Because of = () => Computer.SetRoom(RoomId);

        It should_add_roomId_to_computer = () => Computer.RoomId.ShouldEqual(RoomId);
    }

    [Subject("Add user to computer")]
    public class when_add_user_to_computer : ComputerTests
    {
        Establish context = () => {};
        Because of = () => Computer.AddUser(User);

        It should_add_user_to_collection = () => Computer.Users.ShouldContain(User);
    }

    [Subject("Add null User to Computer")]
    public class when_add_null_user_to_computer : ComputerTests
    {
        Establish context = () => User = null;
        Because of = () => Exception = Catch.Exception(() => Computer.AddUser(User));

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<System.Exception>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("User cannot be null");
        };
    }

    
    [Subject("Computer initialize without ipAddress")]
    public class when_create_computer_without_ipAddress : ComputerTests
    {
        Establish context = () => IpAddress = string.Empty;
        Because of = () => Exception = Catch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<System.Exception>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("IP address is empty!");
        };
    }

    [Subject("Computer initialize without inventoryNumber")]
    public class when_create_computer_without_inventoryNumber : ComputerTests
    {
        Establish context = () => InventoryNumber = string.Empty;
        Because of = () => Exception = Catch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<System.Exception>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("Inventory number is empty!");
        };
    }
}