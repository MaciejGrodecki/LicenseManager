using System;

namespace LicenseManager.Core.Domain
{
    public class License
    {
        public Guid LicenseId { get; protected set; }
        public string Name { get; protected set; }
        public int Count { get; protected set; }
        public Guid LicenseTypeId { get; protected set; }
        public DateTime BuyDate { get; protected set; }

        protected License()
        {

        }

        public License(string name, int count, Guid licenseTypeId,
                DateTime buyDate)
        {
            LicenseId = Guid.NewGuid();
            SetName(name);
            SetCount(count);
            LicenseTypeId = licenseTypeId;
            SetBuyDate(buyDate);
        }

        public void SetLicenseType(Guid licenseTypeId)
        {
            if(licenseTypeId == null)
            {
                throw new Exception("License type is null");
            }
            LicenseTypeId = licenseTypeId;
        }

        public void SetBuyDate(DateTime buyDate)
        {
            if(buyDate > DateTime.Now)
            {
                throw new Exception("Buy date must be earlier");
            }
            BuyDate = buyDate.Date;
        }

        public void SetCount(int count)
        {
            if(count < 1)
            {
                throw new Exception("Count is lower than 1");
            }

            Count = count;
        }

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name is incorrect");
            }

            Name = name;
        }
    }
}