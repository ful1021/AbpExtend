// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Web.AbpAspNetZeroCoreWebModule
// Assembly: Abp.AspNetZeroCore.Web, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: E82D7A41-87A0-49FB-853C-F00596815594
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore.web\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.Web.dll

using System;
using Abp.AspNetCore;
using Abp.Modules;

namespace Abp.AspNetZeroCore.Web
{
    [DependsOn(new Type[] { typeof(AbpAspNetZeroCoreModule) })]
    [DependsOn(new Type[] { typeof(AbpAspNetCoreModule) })]
    public class AbpAspNetZeroCoreWebModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            this.IocManager.RegisterAssemblyByConvention(typeof(AbpAspNetZeroCoreWebModule).Assembly);
        }
    }
}