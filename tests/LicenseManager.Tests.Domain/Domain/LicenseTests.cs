using System;
using System.Collections.Generic;
using LicenseManager.Core.Domain;
using Machine.Specifications;

namespace LicenseManager.Tests.Domain.Domain
{
    public abstract class LicenseTests : DomainException
    {
        protected static string Name = "MS Office 2016";
        protected static int Count = 12;
        protected static Guid LicenseTypeId = Guid.NewGuid();
        protected static DateTime BuyDate = DateTime.Now;
        protected static Computer Computer = new Computer(Guid.NewGuid(), "US-IN/Z/100-W", "192.168.1.0");
        protected static ICollection<Computer> Computers = new List<Computer>();
        protected static string SerialNumber = "1234-567A-87OL";
        protected static License License;
        protected static void Initialize()
        {
            License = new License(Name, Count, LicenseTypeId, BuyDate, SerialNumber);
        }
        
    }

    [Subject("License initialize")]
    public class when_create_license : LicenseTests
    {
        Establish context = () => {};
        Because of = () => Initialize();

        It should_be_not_null = () => License.ShouldNotBeNull();
        It should_has_assigned_name = () => License.Name.ShouldEqual(Name);
        It should_has_assigned_licenseTypeId = () => License.LicenseTypeId.ShouldEqual(LicenseTypeId);
        It should_has_assigned_buyDate = () => License.BuyDate.ShouldEqual(BuyDate.Date);
        It should_has_assigned_serialNumber = () => License.SerialNumber.ShouldEqual(SerialNumber);
    }

    [Subject("License initialize without serial number")]
    public class when_create_license_without_serial_number : LicenseTests
    {
        Establish context = () => SerialNumber = string.Empty;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());
        
        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };
        It should_contain_error_message = () =>
        {
            Exception.Message.ShouldStartWith("Serial number cannot be null");
        };
        
    }

    [Subject("License initialize with buyDate later than today")]
    public class when_create_license_with_buyDate_later_than_today : LicenseTests
    {
        Establish context = () => BuyDate = DateTime.Now.AddDays(1);
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_message = () =>
        {
            Exception.Message.ShouldStartWith("Buy date must be earlier");
        };
    }

    [Subject("License initialize with count less than one")]
    public class when_create_license_with_count_less_than_one : LicenseTests
    {
        Establish context = () => Count = 0;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_message = () =>
        {
            Exception.Message.ShouldStartWith("Count is lower than 1");
        };
    }

    [Subject("License initialize without name")]
    public class when_create_license_without_name : LicenseTests
    {
        Establish context = () => Name = string.Empty;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_message = () =>
        {
            Exception.Message.ShouldStartWith("Name is incorrect");
        };
    }

    [Subject("Add Computer to license")]
    public class when_add_computer_to_license : LicenseTests
    {
        Establish context = () => {};
        Because of = () =>
        {
            License.AddComputer(Computer);
            
        };

        It should_add_computer_to_collection = () => License.Computers.ShouldContain(Computer);
    }

    [Subject("Add null computer object to license")]
    public class when_add_null_computer_to_license : LicenseTests
    {
        Establish context = () => Computer = null;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => License.AddComputer(Computer));

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_message = () =>
        {
            Exception.Message.ShouldStartWith("Computer cannot be null");
        };
    }
    

    
}