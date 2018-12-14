// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Url.UrlChecker
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

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
