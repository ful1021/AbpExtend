// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Validation.ValidationHelper
// Assembly: Abp.AspNetZeroCore, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 5B98F706-C0CE-4CBB-BDF4-6B4AE542245F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.dll

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Abp.AspNetZeroCore.Validation
{
    public static class ValidationHelper
    {
        public const string EmailRegex = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return new Regex(EmailRegex).IsMatch(value);
        }
    }
}