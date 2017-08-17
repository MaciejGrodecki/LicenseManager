using System;
using LicenseManager.Core.Domain;

namespace LicenseManager.Infrastructure.DTO
{
    public class LicenseDto
    {
        public Guid LicenseId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public Guid LicenseTypeId { get; set; }
        public DateTime BuyDate { get;  set; }
    }
}