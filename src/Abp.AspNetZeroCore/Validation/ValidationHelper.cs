using System.Text.RegularExpressions;
using Abp.Extensions;

namespace Abp.AspNetZeroCore.Validation
{
    public static class ValidationHelper
    {
        public const string EmailRegex = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

        public static bool IsEmail(string value)
        {
            if (value.IsNullOrEmpty())
                return false;
            return new Regex(EmailRegex).IsMatch(value);
        }
    }
}