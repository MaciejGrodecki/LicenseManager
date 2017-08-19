using System;

namespace LicenseManager.Infrastructure.Commands.LicenseType
{
    public class UpdateLicenseType
    {
        public Guid LicenseTypeId { get; set; }
        public string Name { get; set; }
    }
}