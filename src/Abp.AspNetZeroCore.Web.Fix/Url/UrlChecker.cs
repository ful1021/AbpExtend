// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Url.UrlChecker
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using System.Text.RegularExpressions;

namespace Abp.AspNetZeroCore.Web.Url
{
    public static class UrlChecker
    {
        private static readonly Regex UrlWithProtocolRegex = new Regex("^.{1,10}://.*$");

        public static bool IsRooted(string url)
        {
            return url.StartsWith("/") || UrlChecker.UrlWithProtocolRegex.IsMatch(url);
        }
    }
}