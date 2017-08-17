using System;

namespace LicenseManager.Infrastructure.Commands.Room
{
    public class UpdateRoom
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
    }
}