// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthConfiguration
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using Abp.Dependency;
using System.Collections.Generic;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
  {
    public List<ExternalLoginProviderInfo> Providers { get; }

    public ExternalAuthConfiguration()
    {
      this.Providers = new List<ExternalLoginProviderInfo>();
    }
  }
}
