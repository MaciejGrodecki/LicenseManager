using System;
using LicenseManager.Core.Domain;
using Machine.Specifications;

namespace LicenseManager.Tests.Domain
{
    public static class LicenseManagerExceptionCatch
    {
        public static LicenseManagerException Exception(Action throwingAction)
        {
            return Only<LicenseManagerException>(throwingAction);
        }

        public static LicenseManagerException Exception<T>(Func<T> throwingFunc)
        {
            try
            {
                throwingFunc();
            }
            catch (LicenseManagerException exception)
            {
                return exception;
            }

            return null;
        }

        public static TException Only<TException>(Action throwingAction)
          where TException : LicenseManagerException
        {
            try
            {
                throwingAction();
            }
            catch (TException exception)
            {
                return exception;
            }

            return null;
        }
    }
}