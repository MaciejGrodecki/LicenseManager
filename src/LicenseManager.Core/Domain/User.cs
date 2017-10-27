using System;
using System.Text.RegularExpressions;

namespace LicenseManager.Core.Domain
{
    public class User
    {
        private static readonly string OnlyStringCheck = @"[A-ZĄĆĘŁŃÓŚŹŻ]";
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        
        protected User()
        {

        }

        public User(Guid userId, string name, string surname)
        {
            UserId = userId;
            SetName(name);
            SetSurname(surname);
        }

        public void SetSurname(string surname)
        {
            string exceptionMsg = "Surname is incorrect";

            if (String.IsNullOrWhiteSpace(surname))
            {
                throw new Exception(exceptionMsg);
            }
            if (!Regex.IsMatch(surname.ToUpperInvariant(), OnlyStringCheck))
            {
                throw new Exception(exceptionMsg);
            }

            Surname = surname;
        }

        public void SetName(string name)
        {
            string exceptionMsg = "Name is incorrect";

            if (String.IsNullOrWhiteSpace(name))
            {
                throw new Exception(exceptionMsg);
            }
            if (!Regex.IsMatch(name.ToUpperInvariant(), OnlyStringCheck))
            {
                throw new Exception(exceptionMsg);
            }

            Name = name;
        }
    }
}