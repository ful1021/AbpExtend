// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthProviderApiBase
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using Abp.Dependency;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
  {
    public ExternalLoginProviderInfo ProviderInfo { get; set; }

    public void Initialize(ExternalLoginProviderInfo providerInfo)
    {
      this.ProviderInfo = providerInfo;
    }

    public async Task<bool> IsValidUser(string userId, string accessCode)
    {
      return (await this.GetUserInfo(accessCode)).ProviderKey == userId;
    }

    public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
  }
}
