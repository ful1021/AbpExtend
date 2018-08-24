using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Web.Api.ProxyScripting.Generators;

namespace Abp
{
    /// <summary>
    /// This module is used to use ABP in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpExtendWebCommonModule : AbpModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().ApiProxyScripting.Generators[CustomizedProxyScriptGenerator.Name] = typeof(CustomizedProxyScriptGenerator);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpExtendWebCommonModule).GetAssembly());
        }
    }
}