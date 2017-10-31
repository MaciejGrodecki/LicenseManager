using System;
using LicenseManager.Core.Domain;
using Machine.Specifications;

namespace LicenseManager.Tests.Domain.Domain
{
    public abstract class RoomTests : DomainException
    {
        protected static Guid RoomId = Guid.NewGuid();
        protected static string Name = "C-89";
        protected static Room Room;

        protected static void Initialize()
        {
            Room = new Room(RoomId, Name);
        }
    }
    

    [Subject("Room initialize")]
    public class when_creating_new_room : RoomTests
    {
        Establish context = () => {};
        Because of = () => Initialize();

        It should_not_be_null = () => Room.ShouldNotBeNull();
        It should_have_assigned_roomId = () => Room.RoomId.ShouldEqual(RoomId);
        It should_have_assigned_name = () => Room.Name.ShouldEqual(Name.ToUpperInvariant());
    }

    [Subject("Room initialize with whiteSpace name")]
    public class when_creating_new_room_with_whiteSpace_name : RoomTests
    {
        Establish context = () => Name = String.Empty;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("blank_room_name");
        };
    }

    [Subject("Room initialize without name")]
    public class when_creating_new_room_without_name : RoomTests
    {
        Establish context = () => Name = null;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("blank_room_name");
        };
    }
}