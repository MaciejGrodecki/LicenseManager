using System;

namespace LicenseManager.Infrastructure.Commands.User
{
    public class UpdateUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        
    }
}