using System;
using System.Text.RegularExpressions;

namespace LicenseManager.Core.Domain
{
    public class LicenseType
    {
        private static readonly string OnlyStringCheck = @"/^[A-z]+$/";
        public Guid LicenseTypeId { get; protected set; }
        public string Name { get; protected set; }


        protected LicenseType()
        {
            
        }

        public LicenseType(string name)
        {
            LicenseTypeId = Guid.NewGuid();
            SetName(name);
        }

        private void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new Exception("License type's name is incorrect");
            }
            if(Regex.IsMatch(name, OnlyStringCheck))
            {
                throw new Exception("License type's name is incorrect");
            }

            Name = name;
        }
    }
}