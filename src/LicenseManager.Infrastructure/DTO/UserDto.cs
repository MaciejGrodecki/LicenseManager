using System;

namespace LicenseManager.Infrastructure.DTO
{
    public class UserDto
    {
        public Guid UserId { get;  set; }
        public string Name { get;  set; }
        public string Surname { get;  set; }
    }
}