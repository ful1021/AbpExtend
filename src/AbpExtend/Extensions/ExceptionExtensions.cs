using System;

namespace Abp.Extensions
{
    public static class ExceptionExtensions
    {
        public static T GetInnerException<T>(this Exception ex) where T : Exception
        {
            var tmpEx = ex.InnerException as T;
            if (tmpEx != null)
            {
                return tmpEx;
            }
            if (ex.InnerException != null)
            {
                return GetInnerException<T>(ex.InnerException);
            }
            return null;
        }
    }
}