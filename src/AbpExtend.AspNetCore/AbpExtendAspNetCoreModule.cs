using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Abp
{
    /// <summary>
    /// This module is used to use ABP in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(AbpExtendWebCommonModule))]
    public class AbpExtendAspNetCoreModule : AbpModule
    {
        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpExtendAspNetCoreModule).GetAssembly());
        }
    }
}