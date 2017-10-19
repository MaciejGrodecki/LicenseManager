using System;

namespace LicenseManager.Infrastructure.Commands.License
{
    public class AddLicense
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public Guid LicenseTypeId { get; set; }
        public DateTime BuyDate { get; set; }
        public string SerialNumber { get; set; }

    }
}