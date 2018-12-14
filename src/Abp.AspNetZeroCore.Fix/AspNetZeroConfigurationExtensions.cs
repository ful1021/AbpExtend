// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.AspNetZeroConfigurationExtensions
// Assembly: Abp.AspNetZeroCore, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 56933B5F-14E1-4014-ACD8-FB6D57FFE74B
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.dll

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