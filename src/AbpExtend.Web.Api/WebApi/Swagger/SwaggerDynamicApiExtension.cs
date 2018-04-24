using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Swashbuckle.Application;

namespace Abp.WebApi.Swagger
{
    /// <summary>
    /// 创建swagger 动态 api  扩展类
    /// </summary>
    public static class SwaggerDynamicApiExtension
    {
        /// <summary>
        /// 创建动态 Api，并且使用Swagger
        /// </summary>
        public static void SwaggerApi(this IAbpStartupConfiguration configuration, Dictionary<Type, string> applicationModuleList, bool isGroupBy = true)
        {
            configuration.DynamicApi(applicationModuleList);
            configuration.ConfigureSwaggerUi(applicationModuleList, isGroupBy);
        }

        /// <summary>
        /// 创建动态 Swagger Api
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="applicationModuleList"></param>
        private static void DynamicApi(this IAbpStartupConfiguration configuration, Dictionary<Type, string> applicationModuleList)
        {
            foreach (var item in applicationModuleList)
            {
                var name = item.Value;
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = item.Key.Name.Replace("ApplicationModule", "").Replace("Module", "");
                };
                configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                    .ForAll<IApplicationService>(item.Key.Assembly, name)
                    .WithConventionalVerbs()
                    .Build();
            }
        }
        /// <summary>
        /// 配置SwaggerUi 
        /// </summary>
        private static void ConfigureSwaggerUi(this IAbpStartupConfiguration configuration, Dictionary<Type, string> applicationModuleList, bool isGroupBy = false)
        {
            configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "AbpExtend.Web.Api");

                    if (isGroupBy)
                    {
                        c.GroupActionsBy(CustomGroupActionsBy);
                    }

                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.UseFullTypeNameInSchemaIds();
                    c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();

                    foreach (Type item in applicationModuleList.Keys)
                    {
                        var xml = GetXmlCommentsPath(item);
                        if (File.Exists(xml))
                        {
                            c.IncludeXmlComments(xml);
                        }
                    }
                })
                //.EnableSwaggerUi();
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(AbpExtendWebApiModule)), "Abp.WebApi.Swagger.Swagger-Custom.js");
                });


            //修改默认路由  访问变成：apidoc/index
            //.EnableSwaggerUi("apidoc/{*assetPath}");

            //修改 SwaggerUI界面
            //.EnableSwaggerUi(b =>
            //{
            //    b.InjectJavaScript(Assembly.GetExecutingAssembly(), "Boss.Scripts.swagger.js");
            //    //b.InjectStylesheet();
            //});
        }

        private static string CustomGroupActionsBy(ApiDescription apiDesc)
        {
            var path = apiDesc.RelativePath.ToLower();
            if (path.IndexOf("api/services") >= 0)
            {
                path = path.Replace("api/services/", "");
                var splits = path.Split('/');
                if (splits.Count() > 2)
                {
                    return $"services_{splits[0]}_{splits[1]}";
                }
                else
                {
                    return "none_matched";
                }
            }

            //abp默认接口
            if (path.IndexOf("api/abp") >= 0 || path.IndexOf("api/typescript") >= 0)
            {
                return "abp_default";
            }

            if (path.IndexOf("api/") >= 0)
            {
                var splits = path.Split('/');
                if (splits.Count() > 2)
                {
                    return splits[0] + "_" + splits[1];
                }
                else
                {
                    return "none_matched";
                }
            }

            return "none_matched";
        }

        private static string GetXmlCommentsPath(Type moduleType)
        {
            return string.Format(@"{0}\bin\{1}.XML", AppDomain.CurrentDomain.BaseDirectory, moduleType.Assembly.GetName().Name);
        }
    }
}
