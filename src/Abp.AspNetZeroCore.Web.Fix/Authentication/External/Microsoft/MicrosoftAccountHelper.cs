// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.Microsoft.MicrosoftAccountHelper
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

using Newtonsoft.Json.Linq;
using System;

namespace Abp.AspNetZeroCore.Web.Authentication.External.Microsoft
{
  public static class MicrosoftAccountHelper
  {
    public static string GetId(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "id");
    }

    public static string GetDisplayName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "displayName");
    }

    public static string GetGivenName(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "givenName");
    }

    public static string GetSurname(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "surname");
    }

    public static string GetEmail(JObject user)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      return (string) ((JToken) user).Value<string>((object) "mail") ?? (string) ((JToken) user).Value<string>((object) "userPrincipalName");
    }
  }
}
