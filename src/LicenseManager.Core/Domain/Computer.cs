using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LicenseManager.Core.Domain
{
    public class Computer
    {
        private ISet<User> _users = new HashSet<User>();
        private static readonly string IpAddressCheck = @"((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
        public Guid ComputerId { get; protected set; }
        public string InventoryNumber { get; protected set; }
        public string IpAddress { get; protected set; }
        public Guid RoomId { get; protected set; }
        public ICollection<User> Users => _users;

        protected Computer()
        {

        }

        public Computer(Guid computerId, string inventoryNumber, string ipAddress)
        {
            ComputerId = computerId;
            SetInventoryNumber(inventoryNumber);
            SetIpAddress(ipAddress);
        }

        public Computer(Guid computerId, string inventoryNumber, string ipAddress,
                Guid roomId)
        {
            ComputerId = computerId;
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

        public void AssignRoomToComputer(Guid roomId)
        {
            if(roomId == null)
            {
                throw new Exception("Room id cannot be null");
            }
            RoomId = roomId;
        }

        public void AddUser(User user)
        {
            if(user == null)
            {
                throw new Exception("User cannot be null");
            }
            if (!Users.Contains(user))
            {
                _users.Add(user);
            }
        }
    }
}