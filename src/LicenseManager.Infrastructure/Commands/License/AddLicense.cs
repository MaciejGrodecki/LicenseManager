using System;

namespace LicenseManager.Infrastructure.Commands.License
{
    public class AddLicense
    {
        public string Name { get; set; }
        public int count { get; set; }
        public Guid licenseTypeId { get; set; }
        public DateTime buyDate { get; set; }

    }
}