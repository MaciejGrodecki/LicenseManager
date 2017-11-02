using System;
using LicenseManager.Core.Domain;
using Machine.Specifications;

namespace LicenseManager.Tests.Domain.Domain
{
    public abstract class UserTests : DomainException
    {
        protected static string Name = "Jan";
        protected static string Surname = "Maciejewski";
        protected static User User;

        protected static void Initialize()
        {
            User = new User(Guid.NewGuid(), Name, Surname);
        }
        
    }

    [Subject("User initialize")]
    public class when_creating_user : UserTests
    {
        Establish context = () => {};
        Because of = () => Initialize();

        It should_not_be_null = () => User.ShouldNotBeNull();
        It should_have_assigned_userId = () => User.UserId.ShouldNotEqual(Guid.Empty);
        It should_have_assigned_name = () => User.Name.ShouldEqual(Name);
        It should_have_assigned_surname = () => User.Surname.ShouldEqual(Surname);
    }

    [Subject("User initialize without surname")]
    public class when_creating_user_without_surname : UserTests
    {
        Establish context = () => Surname = string.Empty;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("Surname is incorrect");
        };
    }

    [Subject("User initialize without name")]
    public class when_creating_user_without_name : UserTests
    {
        Establish context = () => Name = string.Empty;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_msg = () =>
        {
            Exception.Message.ShouldStartWith("Name is incorrect");
        };
    }

    [Subject("User initialize with name and surname which contain polish letters")]
    public class when_creating_user_and_surname_with_polish_name : UserTests
    {
        Establish context = () => 
        {
            Name = "Móąźćń";
            Surname = "Mączyński";
        };
        Because of = () => Initialize();

        It should_not_be_null = () => User.ShouldNotBeNull();
        It should_have_assigned_name = () => User.Name.ShouldEqual(Name);
    }

    
    
}