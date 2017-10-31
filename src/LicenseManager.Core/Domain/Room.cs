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

        public Room(Guid roomId, string name)
        {
            RoomId = roomId;
            SetName(name);
        }

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new LicenseManagerException("blank_room_name", "Room's name is incorrect");
            }
            Name = name.ToUpperInvariant();
        }
    }
}