using System;

namespace LicenseManager.Infrastructure.Commands.License
{
    public class UpdateLicense
    {
        public Guid LicenseId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public Guid LicenseTypeId { get; set; }
        public DateTime BuyDate { get; set; }
        
    }
}