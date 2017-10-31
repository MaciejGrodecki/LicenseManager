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

        public LicenseType(Guid licenseTypeId, string name)
        {
            LicenseTypeId = licenseTypeId;
            SetName(name);
        }

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                throw new LicenseManagerException("incorrect_licenseType", "License type's name is incorrect");
            }
            if(Regex.IsMatch(name, OnlyStringCheck))
            {
                throw new LicenseManagerException("incorrect_licenseType", "License type's name is incorrect");
            }

            Name = name;
        }
    }
}