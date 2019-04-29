// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.AspNetZeroConfigurationExtensions
// Assembly: Abp.AspNetZeroCore, Version=1.2.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 5B98F706-C0CE-4CBB-BDF4-6B4AE542245F
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore\1.2.2\lib\netcoreapp2.2\Abp.AspNetZeroCore.dll

using Abp.Configuration.Startup;

namespace Abp.AspNetZeroCore
{
    public static class AspNetZeroConfigurationExtensions
    {
        public static AspNetZeroConfiguration AspNetZero(this IModuleConfigurations modules)
        {
            return modules.AbpConfiguration.Get<AspNetZeroConfiguration>();
        }
    }
}