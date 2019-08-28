using Abp.Dependency;
using Abp.Modules;

namespace Abp.AspNetZeroCore
{
    public class AbpAspNetZeroCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            this.IocManager.Register<AspNetZeroConfiguration>(DependencyLifeStyle.Singleton);
        }

        public override void Initialize()
        {
            this.IocManager.RegisterAssemblyByConvention(typeof(AbpAspNetZeroCoreModule).Assembly);
        }

        public override void PostInitialize()
        {
        }

        public AbpAspNetZeroCoreModule()
        {
        }
    }
}