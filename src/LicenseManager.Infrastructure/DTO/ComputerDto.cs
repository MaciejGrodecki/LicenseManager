using System;
using System.Collections.Generic;
using LicenseManager.Core.Domain;

namespace LicenseManager.Infrastructure.DTO
{
    public class ComputerDto
    {
        public Guid ComputerId { get; set; }
        public string InventoryNumber { get; set; }
        public string IpAddress { get; set; }
        public Guid RoomId { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}