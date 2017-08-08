using System;

namespace LicenseManager.Core.Domain
{
    public class Room
    {
        public Guid RoomId { get; protected set; }
        public string Name { get; protected set; }

        protected Room()
        { 

        }

        public Room(string name)
        {
            RoomId = Guid.NewGuid();
            SetName(name);
        }

        private void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Room's name is incorrect");
            }
            Name = name.ToLowerInvariant();
        }
    }
}