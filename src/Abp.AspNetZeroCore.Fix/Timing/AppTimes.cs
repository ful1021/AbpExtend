// Decompiled with JetBrains decompiler
// Type: Abp.AspNetZeroCore.Timing.AppTimes
// Assembly: Abp.AspNetZeroCore, Version=1.1.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 56933B5F-14E1-4014-ACD8-FB6D57FFE74B
// Assembly location: C:\Users\fuliang\.nuget\packages\abp.aspnetzerocore\1.1.8\lib\netcoreapp2.1\Abp.AspNetZeroCore.dll

using Abp.Dependency;
using Abp.Timing;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Abp.AspNetZeroCore.Timing
{
    public class AppTimes : ISingletonDependency
    {
        public DateTime StartupTime { get; set; } = Clock.Now;
    }
}
