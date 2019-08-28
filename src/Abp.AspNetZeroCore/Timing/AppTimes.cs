using System;
using Abp.Dependency;
using Abp.Timing;

namespace Abp.AspNetZeroCore.Timing
{
    public class AppTimes : ISingletonDependency
    {
        public DateTime StartupTime { get; set; } = Clock.Now;
    }
}