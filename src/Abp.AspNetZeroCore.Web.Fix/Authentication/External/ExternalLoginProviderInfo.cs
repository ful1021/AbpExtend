// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalLoginProviderInfo
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using System;
using System.Collections.Generic;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalLoginProviderInfo
  {
    public string Name { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public Type ProviderApiType { get; set; }

    public Dictionary<string, string> AdditionalParams { get; set; }

    public ExternalLoginProviderInfo(
      string name,
      string clientId,
      string clientSecret,
      Type providerApiType,
      Dictionary<string, string> additionalParams = null)
    {
      this.Name = name;
      this.ClientId = clientId;
      this.ClientSecret = clientSecret;
      this.ProviderApiType = providerApiType;
      this.AdditionalParams = additionalParams;
    }
  }
}
