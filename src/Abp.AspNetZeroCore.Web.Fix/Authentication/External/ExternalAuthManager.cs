// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthManager
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using Abp.Dependency;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalAuthManager : IExternalAuthManager, ITransientDependency
  {
    private readonly IIocResolver _iocResolver;
    private readonly IExternalAuthConfiguration _externalAuthConfiguration;

    public ExternalAuthManager(
      IIocResolver iocResolver,
      IExternalAuthConfiguration externalAuthConfiguration)
    {
      this._iocResolver = iocResolver;
      this._externalAuthConfiguration = externalAuthConfiguration;
    }

    public Task<bool> IsValidUser(
      string provider,
      string providerKey,
      string providerAccessCode)
    {
      using (IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> providerApi = this.CreateProviderApi(provider))
        return providerApi.Object.IsValidUser(providerKey, providerAccessCode);
    }

    public Task<ExternalAuthUserInfo> GetUserInfo(
      string provider,
      string accessCode)
    {
      using (IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> providerApi = this.CreateProviderApi(provider))
        return providerApi.Object.GetUserInfo(accessCode);
    }

    public IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> CreateProviderApi(
      string provider)
    {
      ExternalLoginProviderInfo providerInfo = this._externalAuthConfiguration.Providers.FirstOrDefault<ExternalLoginProviderInfo>((Func<ExternalLoginProviderInfo, bool>) (p => p.Name == provider));
      if (providerInfo == null)
        throw new Exception("Unknown external auth provider: " + provider);
      IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> dependencyObjectWrapper = IocResolverExtensions.ResolveAsDisposable<IExternalAuthProviderApi>(this._iocResolver, providerInfo.ProviderApiType);
      dependencyObjectWrapper.Object.Initialize(providerInfo);
      return dependencyObjectWrapper;
    }
  }
}
