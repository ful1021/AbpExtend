using System.Text;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Web.Api.Modeling;

namespace Abp.Web.Api.ProxyScripting.Generators
{
    public class CustomizedProxyScriptGenerator : IProxyScriptGenerator, ITransientDependency
    {
        /// <summary>
        /// "vue".
        /// </summary>
        public const string Name = "2";

        public string CreateScript(ApplicationApiDescriptionModel model)
        {
            var script = new StringBuilder();
            foreach (var module in model.Modules.Values)
            {
                var moduleName = module.Name.ToCamelCase();

                foreach (var controller in module.Controllers.Values)
                {
                    var controllerName = controller.Name.ToCamelCase();

                    script.AppendLine("(function(){");
                    script.AppendLine();

                    script.AppendLine($"    var serviceNamespace = site.utils.createNamespace(site.api, 'services.{moduleName}.{controllerName}');");
                    script.AppendLine();

                    foreach (var action in controller.Actions.Values)
                    {
                        AddActionScript(script, controller, action);
                        script.AppendLine();
                    }

                    script.AppendLine();
                    script.AppendLine("})();");
                    script.AppendLine();
                }
            }

            return script.ToString();
        }

        private static void AddActionScript(StringBuilder script, ControllerApiDescriptionModel controller, ActionApiDescriptionModel action)
        {
            var actionName = ProxyScriptingJsFuncHelper.WrapWithBracketsOrWithDotPrefix(action.Name.ToCamelCase());
            var parameterList = ProxyScriptingJsFuncHelper.GenerateJsFuncParameterList(action, "ajaxParams");

            script.AppendLine($"    serviceNamespace{actionName} = function({parameterList}) {{");
            script.AppendLine("        return site.ajax(site.utils.extend({");

            AddAjaxCallParameters(script, controller, action);

            script.AppendLine("        }, ajaxParams));");
            script.AppendLine("    };");
        }

        private static void AddAjaxCallParameters(StringBuilder script, ControllerApiDescriptionModel controller, ActionApiDescriptionModel action)
        {
            var httpMethod = action.HttpMethod?.ToUpperInvariant() ?? "POST";

            script.AppendLine("        url: site.Config.webapiDomain + '" + ProxyScriptingHelper.GenerateUrlWithParameters(action) + "',");
            script.Append("        type: '" + httpMethod + "'");

            if (action.ReturnValue.Type == typeof(void))
            {
                script.AppendLine(",");
                script.Append("        dataType: null");
            }

            var headers = ProxyScriptingHelper.GenerateHeaders(action, 8);
            if (headers != null)
            {
                script.AppendLine(",");
                script.Append("        headers: " + headers);
            }

            var body = ProxyScriptingHelper.GenerateBody(action);
            if (!body.IsNullOrEmpty())
            {
                script.AppendLine(",");
                script.Append("        data: JSON.stringify(input)");
            }
            else
            {
                var formData = ProxyScriptingHelper.GenerateFormPostData(action, 8);
                if (!formData.IsNullOrEmpty())
                {
                    script.AppendLine(",");
                    script.Append("        data: " + formData);
                }
            }

            script.AppendLine();
        }
    }
}