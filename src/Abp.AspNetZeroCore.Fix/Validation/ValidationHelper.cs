// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Validation.ValidationHelper
// Assembly: Abp.AspNetZeroCore, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 56933B5F-14E1-4014-ACD8-FB6D57FFE74B
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.dll

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