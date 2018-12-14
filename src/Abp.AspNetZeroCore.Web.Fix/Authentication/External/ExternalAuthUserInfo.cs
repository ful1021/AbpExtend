// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.Authentication.External.ExternalAuthUserInfo
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

namespace Abp.AspNetZeroCore.Web.Authentication.External
{
  public class ExternalAuthUserInfo
  {
    public string ProviderKey { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string Surname { get; set; }

    public string Provider { get; set; }
  }
}
