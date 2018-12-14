// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthProviderApiBase
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

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
