using System;

namespace LicenseManager.Core.Domain
{
    public class LicenseManagerException : Exception
    {
        public string ErrorCode { get; }

        public LicenseManagerException()
        {
            
        }

        public LicenseManagerException(string errorCode) : this(errorCode, null, null, null)
        {
            
        }

        public LicenseManagerException(string message, params object[] args)
            : this(null, null, message, args)
        {
            
        }

        public LicenseManagerException(string errorCode, string message, params object[] args)
            :this(errorCode, null, message, args)
        {
            
        }

        public LicenseManagerException(Exception innerException, string message, params object[] args)
            :this(string.Empty, innerException, message, args)
        {
            
        }

        public LicenseManagerException(string errorCode, Exception innerException, string message,
            params object[] args) : base(string.Format(message, args), innerException)
        {
            ErrorCode = errorCode;
        }
    }
}