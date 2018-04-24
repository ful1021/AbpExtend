using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Abp;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.WebApi.Controllers.Dynamic;
using Abp.WebApi.Controllers.Dynamic.Scripting;
using Abp.WebApi.Scripts.Customized;

namespace Abp.WebApi.Scripts
{
    public class SiteScriptProxyManager : ISingletonDependency
    {
        private readonly IDictionary<string, ScriptInfo> CachedScripts;
        private readonly DynamicApiControllerManager _dynamicApiControllerManager;
        private readonly ScriptProxyManager _scriptProxyManager;

        public SiteScriptProxyManager(
            DynamicApiControllerManager dynamicApiControllerManager,
            ScriptProxyManager scriptProxyManager)
        {
            _dynamicApiControllerManager = dynamicApiControllerManager;
            _scriptProxyManager = scriptProxyManager;
            CachedScripts = new Dictionary<string, ScriptInfo>();
        }

        public string GetScript(string name, ProxyType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is null or empty!", nameof(name));
            }

            if (type == ProxyType.JQuery)
            {
                return _scriptProxyManager.GetScript(name, ProxyScriptType.JQuery);
            }

            if (type == ProxyType.Angular)
            {
                return _scriptProxyManager.GetScript(name, ProxyScriptType.Angular);
            }

            var cacheKey = type + "_" + name;

            lock (CachedScripts)
            {
                var cachedScript = CachedScripts.GetOrDefault(cacheKey);
                if (cachedScript == null)
                {
                    var dynamicController = _dynamicApiControllerManager
                        .GetAll()
                        .FirstOrDefault(ci => ci.ServiceName == name && ci.IsProxyScriptingEnabled);

                    if (dynamicController == null)
                    {
                        throw new HttpException((int)HttpStatusCode.NotFound, "There is no such a service: " + cacheKey);
                    }

                    var script = CreateProxyGenerator(type, dynamicController, true).Generate();
                    CachedScripts[cacheKey] = cachedScript = new ScriptInfo(script);
                }

                return cachedScript.Script;
            }
        }

        public string GetAllScript(ProxyType type)
        {
            lock (CachedScripts)
            {
                var cacheKey = type + "_all";
                if (!CachedScripts.ContainsKey(cacheKey))
                {
                    if (type == ProxyType.JQuery)
                    {
                        return _scriptProxyManager.GetAllScript(ProxyScriptType.JQuery);
                    }

                    if (type == ProxyType.Angular)
                    {
                        return _scriptProxyManager.GetAllScript(ProxyScriptType.Angular);
                    }

                    var script = new StringBuilder();

                    var dynamicControllers = _dynamicApiControllerManager.GetAll().Where(ci => ci.IsProxyScriptingEnabled);
                    foreach (var dynamicController in dynamicControllers)
                    {
                        var proxyGenerator = CreateProxyGenerator(type, dynamicController, false);
                        script.AppendLine(proxyGenerator.Generate());
                        script.AppendLine();
                    }

                    CachedScripts[cacheKey] = new ScriptInfo(script.ToString());
                }

                return CachedScripts[cacheKey].Script;
            }
        }

        private static IScriptProxyGenerator CreateProxyGenerator(ProxyType type, DynamicApiControllerInfo controllerInfo, bool amdModule)
        {
            switch (type)
            {
                case ProxyType.Customized:
                    return new CustomizedProxyGenerator(controllerInfo, amdModule);
                default:
                    throw new AbpException("Unknown ProxyType: " + type);
            }
        }

        private class ScriptInfo
        {
            public string Script { get; private set; }

            public ScriptInfo(string script)
            {
                Script = script;
            }
        }
    }
}
