using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICollection<User> Users
        {
            get => _users;
            protected set => _users = new HashSet<User>(value);
        }

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
            SetRoom(roomId);
        }

        public void SetIpAddress(string ipAddress)
        {
            if (String.IsNullOrWhiteSpace(ipAddress))
            {
                throw new Exception("IP address is empty!");
            }
            if (!Regex.IsMatch(ipAddress, IpAddressCheck))
            {
                throw new Exception("IP address is incorrect!");
            }

            IpAddress = ipAddress;
        }

        public void SetInventoryNumber(string inventoryNumber)
        {
            if (String.IsNullOrWhiteSpace(inventoryNumber))
            {
                throw new Exception("Inventory number is empty!");
            }

            InventoryNumber = inventoryNumber.ToUpperInvariant();
        }

        public void SetRoom(Guid roomId)
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
            if(Users.Contains(user))
            {
                throw new Exception($"User with name {user.Name} and {user.Surname} already assigned to computer");
            }

            _users.Add(user);
        }
    }
}