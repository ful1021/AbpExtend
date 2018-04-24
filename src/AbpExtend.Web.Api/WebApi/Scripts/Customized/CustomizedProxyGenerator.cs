using System.Text;
using Abp.Extensions;
using Abp.WebApi.Controllers.Dynamic;

namespace Abp.WebApi.Scripts.Customized
{
    internal class CustomizedProxyGenerator : IScriptProxyGenerator
    {
        private readonly DynamicApiControllerInfo _controllerInfo;
        private readonly bool _defineAmdModule;

        public CustomizedProxyGenerator(DynamicApiControllerInfo controllerInfo, bool defineAmdModule = true)
        {
            _controllerInfo = controllerInfo;
            _defineAmdModule = defineAmdModule;
        }

        public string Generate()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();
            script.AppendLine("    var serviceNamespace = site.utils.createNamespace(site.api, 'services." + _controllerInfo.ServiceName.Replace("/", ".") + "');");
            script.AppendLine();

            //generate all methods
            foreach (var methodInfo in _controllerInfo.Actions.Values)
            {
                AppendMethod(script, _controllerInfo, methodInfo);
                script.AppendLine();
            }

            //generate amd module definition
            if (_defineAmdModule)
            {
                script.AppendLine("    if(typeof define === 'function' && define.amd){");
                script.AppendLine("        define(function (require, exports, module) {");
                script.AppendLine("            return {");

                var methodNo = 0;
                foreach (var methodInfo in _controllerInfo.Actions.Values)
                {
                    script.AppendLine("                " + methodInfo.ActionName.ToCamelCase() + ": serviceNamespace." + methodInfo.ActionName.ToCamelCase() + ((methodNo++) < (_controllerInfo.Actions.Count - 1) ? "," : ""));
                }

                script.AppendLine("            };");
                script.AppendLine("        });");
                script.AppendLine("    }");
            }

            script.AppendLine();
            script.AppendLine("})();");

            return script.ToString();
        }

        private static void AppendMethod(StringBuilder script, DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo methodInfo)
        {
            var generator = new CustomizedActionScriptGenerator(controllerInfo, methodInfo);
            script.AppendLine(generator.GenerateMethod());
        }
    }
}