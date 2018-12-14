// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.IExternalAuthProviderApi
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public interface IExternalAuthProviderApi
  {
    ExternalLoginProviderInfo ProviderInfo { get; }

    Task<bool> IsValidUser(string userId, string accessCode);

    Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);

    void Initialize(ExternalLoginProviderInfo providerInfo);
  }
}
