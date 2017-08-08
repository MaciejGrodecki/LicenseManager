using System;

namespace LicenseManager.Core.Domain
{
    public class License
    {
        public Guid LicenseId { get; protected set; }
        public string Name { get; protected set; }
        public int Count { get; protected set; }
        public LicenseType LicenseType { get; protected set; }
        public DateTime BuyDate { get; protected set; }

        protected License()
        {

        }

        public License(string name, int count, LicenseType licenseType,
                DateTime buyDate)
        {
            LicenseId = Guid.NewGuid();
            SetName(name);
            SetCount(count);
            LicenseType = licenseType;
            SetBuyDate(buyDate);
        }

        private void SetBuyDate(DateTime buyDate)
        {
            if(buyDate > DateTime.Now)
            {
                throw new Exception("Buy date must be earlier");
            }
            BuyDate = buyDate.Date;
        }

        private void SetCount(int count)
        {
            if(count < 1)
            {
                throw new Exception("Count is lower than 1");
            }

            Count = count;
        }

        private void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name is incorrect");
            }

            Name = name;
        }
    }
}