using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LicenseManager.Core.Domain
{
    public class Computer
    {
        private static readonly string IpAddressCheck = @"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
        public Guid ComputerId { get; protected set; }
        public string InventoryNumber { get; protected set; }
        public string IpAddress { get; protected set; }
        public Guid RoomId { get; protected set; }
        public IEnumerable<User> Users { get; set; }

        protected Computer()
        {

        }

        public Computer(string inventoryNumber, string ipAddress)
        {
            ComputerId = Guid.NewGuid();
            SetInventoryNumber(inventoryNumber);
            SetIpAddress(ipAddress);
        }

        public Computer(string inventoryNumber, string ipAddress,
                Guid roomId)
        {
            ComputerId = Guid.NewGuid();
            SetInventoryNumber(inventoryNumber);
            SetIpAddress(ipAddress);
            RoomId = roomId;
        }

        public Computer(string inventoryNumber, string ipAddress,
                Guid roomId, IEnumerable<User> users)
        {
            ComputerId = Guid.NewGuid();
            SetInventoryNumber(inventoryNumber);
            SetIpAddress(ipAddress);
            RoomId = roomId;
        }

        public void SetIpAddress(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress))
            {
                throw new Exception("Proszę podać adres IP!");
            }
            if (!Regex.IsMatch(ipAddress, IpAddressCheck))
            {
                throw new Exception("Adres IP jest niepoprawny!");
            }

            IpAddress = ipAddress;
        }

        public void SetInventoryNumber(string inventoryNumber)
        {
            if (String.IsNullOrWhiteSpace(inventoryNumber))
            {
                throw new Exception("Proszę podać numer inwentarzowy!");
            }

            InventoryNumber = inventoryNumber.ToUpperInvariant();
        }
    }
}