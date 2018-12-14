// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.AbpAspNetZeroCoreWebModule
// Assembly: Abp.AspNetZeroCore.Web, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 80F05ACE-4DE3-4EA6-96CC-9307E608EE8F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.Web.dll

using Abp.AspNetCore;
using Abp.Modules;
using System;

namespace Abp.AspNetZeroCore.Web
{
  [DependsOn(new Type[] {typeof (AbpAspNetZeroCoreModule)})]
  [DependsOn(new Type[] {typeof (AbpAspNetCoreModule)})]
  public class AbpAspNetZeroCoreWebModule : AbpModule
  {
    public override void PreInitialize()
    {
    }

    public override void Initialize()
    {
      this.IocManager.RegisterAssemblyByConvention(typeof (AbpAspNetZeroCoreWebModule).Assembly);
    }
  }
}
