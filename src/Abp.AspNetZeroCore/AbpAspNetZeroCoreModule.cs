// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.AbpAspNetZeroCoreModule
// Assembly: Abp.AspNetZeroCore, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 5B98F706-C0CE-4CBB-BDF4-6B4AE542245F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.dll

using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Abp.AspNetZeroCore
{
    public class AbpAspNetZeroCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<AspNetZeroConfiguration>(DependencyLifeStyle.Singleton);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpAspNetZeroCoreModule).GetAssembly());
        }
    }
}