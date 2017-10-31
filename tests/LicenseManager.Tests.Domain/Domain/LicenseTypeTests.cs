using System;
using LicenseManager.Core.Domain;
using Machine.Specifications;

namespace LicenseManager.Tests.Domain.Domain
{
    public abstract class LicenseTypeTests : DomainException
    {
        protected static Guid LicenseTypeId = Guid.NewGuid();
        protected static string Name = "PKL";

        protected static LicenseType LicenseType;

        protected static void Initialize()
        {
            LicenseType = new LicenseType(LicenseTypeId, Name);
        }
    }

    [Subject("License type initialize")]
    public class when_licenseType_initialize : LicenseTypeTests
    {
        Establish context = () => {};
        Because of = () => Initialize();

        It should_not_be_null = () => LicenseType.ShouldNotBeNull();
        It should_have_assigne_licenseTypeId = () => LicenseType.LicenseTypeId.ShouldEqual(LicenseTypeId);                                            
        It should_have_assigne_name = () => LicenseType.Name.ShouldEqual(Name);
    }

    [Subject("License type initialize without name")]
    public class when_licensetype_initialize_without_name : LicenseTypeTests
    {
        Establish context = () => Name = String.Empty;
        Because of = () => Exception = LicenseManagerExceptionCatch.Exception(() => Initialize());

        It should_throw_exception = () =>
        {
            Exception.ShouldBeOfExactType<LicenseManagerException>();
        };

        It should_contain_error_message = () =>
        {
            Exception.Message.ShouldStartWith("incorrect_licenseType");
        };
    }
}