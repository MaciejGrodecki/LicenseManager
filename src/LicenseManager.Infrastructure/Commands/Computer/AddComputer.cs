using System;
using System.Collections.Generic;
using LicenseManager.Infrastructure.DTO;

namespace LicenseManager.Infrastructure.Commands.Computer
{
    public class AddComputer
    {
        public Guid ComputerId { get; set; }
        public string InventoryNumber { get; set; }
        public string IpAddress { get; set; }
        public Guid RoomId { get; set; }
        public HashSet<Guid> UsersId { get; set; } 
    }
}