using System;
using LicenseManager.Core.Domain;

namespace LicenseManager.Infrastructure.Services
{
    public static class NullCheck
    {
        //Method to check if object is null. If object is null exception will be call from NullException method
        public static void IsNull<T>(T obj)
        {
            if(obj == null)
            {
                NullException(obj);
            }
        }

        //Method to check if object is not null. If object is not null exception will be call from NotNullException method
        public static void IsNotNull<T>(T obj)
        {
            if(obj != null)
            {
                NotNullException(obj);
            }
        }

        //Throw LicenseManagerException if object is null
        private static void NullException<T>(T obj)
        {
            var type = typeof(T).Name;
            throw new LicenseManagerException($"{type}_null", $"{type} doesn't exist");
        }

        //Throw LicenseManagerException if object is not null
        private static void NotNullException<T>(T obj)
        {
            var type = typeof(T).Name;
            throw new LicenseManagerException($"{type}_already_exists", $"{type} already exists");
        }
    }
}