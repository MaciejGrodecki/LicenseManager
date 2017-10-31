using System;
using System.Collections.Generic;

namespace LicenseManager.Core.Domain
{
    public class License
    {
        private ISet<Computer> _computers = new HashSet<Computer>();
        public Guid LicenseId { get; protected set; }
        public string Name { get; protected set; }
        public int Count { get; protected set; }
        public Guid LicenseTypeId { get; protected set; }
        public DateTime BuyDate { get; protected set; }
        public ICollection<Computer> Computers
        {
            get => _computers;
            protected set => _computers = new HashSet<Computer>(value);
        }
        public string SerialNumber { get; protected set; }

        protected License()
        {

        }

        public License(string name, int count, Guid licenseTypeId,
                DateTime buyDate, string serialNumber)
        {
            LicenseId = Guid.NewGuid();
            SetName(name);
            SetCount(count);
            LicenseTypeId = licenseTypeId;
            SetBuyDate(buyDate);
            SetSerialNumber(serialNumber);
        }

        public void SetLicenseType(Guid licenseTypeId)
        {
            if(licenseTypeId == null)
            {
                throw new LicenseManagerException("licenseType_is_null", "License type is null");
            }
            LicenseTypeId = licenseTypeId;
        }

        public void SetBuyDate(DateTime buyDate)
        {

            if(buyDate > DateTime.Now)
            {
                throw new LicenseManagerException("buy_date_earlier", "Buy date must be earlier");
            }
            BuyDate = buyDate.Date;
        }

        public void SetCount(int count)
        {
            if(count < 1)
            {
                throw new LicenseManagerException("count_lower_than_1","Count is lower than 1");
            }

            Count = count;
        }

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new LicenseManagerException("incorrect_name", "Name is incorrect");
            }

            Name = name;
        }

        public void AddComputer(Computer computer)
        {
            if(computer == null)
            {
                throw new LicenseManagerException("computer_is_null", "Computer cannot be null");
            }
            if(Computers.Contains(computer))
            {
                throw new LicenseManagerException("computer_already_in_collection", $"Computer with {computer.InventoryNumber} already exists");
            }

            _computers.Add(computer);
        }

        public void SetSerialNumber(string serialNumber)
        {
            if(String.IsNullOrWhiteSpace(serialNumber))
            {
                throw new LicenseManagerException("serial_cannot_be_null", "Serial number cannot be null");
            }

            SerialNumber = serialNumber;
        }

    }
}