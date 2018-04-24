using System.Reflection;
using Abp.Modules;

namespace Abp.WebApi
{
    /// <summary>
    /// This module provides Abp features for ASP.NET Web API.
    /// </summary>
    public class AbpExtendWebApiModule : AbpModule
    {
        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}