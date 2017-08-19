using System;

namespace LicenseManager.Infrastructure.Commands.LicenseType
{
    public class AddLicenseType
    {
        public Guid LicenseTypeId { get; set; }
        public string Name { get; set; }
    }
}