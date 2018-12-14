// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalLoginProviderInfo
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

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

    public ExternalLoginProviderInfo(string name, string clientId, string clientSecret, Type providerApiType, Dictionary<string, string> additionalParams = null)
    {
      this.Name = name;
      this.ClientId = clientId;
      this.ClientSecret = clientSecret;
      this.ProviderApiType = providerApiType;
      this.AdditionalParams = additionalParams;
    }
  }
}
