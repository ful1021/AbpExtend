using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.Configuration.Startup;
using Swashbuckle.Application;
using System.Linq;
using System;
using Abp.WebApi.Configuration;
using Abp.WebApi.Authorization;
using Boss;
using Abp.Web.Configuration;
using Abp.Web.Models;
using Abp.Localization;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using System.Security.Cryptography;
using System.Collections.Generic;
using Abp.Collections.Extensions;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Abp.Json;
using Newtonsoft.Json;

namespace Boss
{
    /// <summary>
    /// 公用Web API 接口模块
    /// </summary>
    [DependsOn(
        typeof(AbpWebApiModule)
        )]
    public class BossWebApiBaseModule : AbpModule
    {
        /// <summary>
        /// 初始化此模块前 执行
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //注入自定义异常处理
            IocManager.Resolve<IEventBus>().Register<AbpHandledExceptionData>(MyErrorHandle.HandError);
        }

        public override void PostInitialize()
        {
            base.PostInitialize();

            var httpConfiguration = IocManager.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;

            InitializeFilters(httpConfiguration);
            InitializeFormatters(httpConfiguration);
        }

        private void InitializeFilters(System.Web.Http.HttpConfiguration httpConfiguration)
        {
            //添加过滤器
            //httpConfiguration.Filters.Add(IocManager.Resolve<LoginValidateAttribute>());
        }

        private void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();

            //var Settings = new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Error,
            //    //忽略为NULL的值
            //    NullValueHandling = NullValueHandling.Ignore,

            //    //Converters = new List<JsonConverter> { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm" } },
            //};
            //httpConfiguration.Formatters.JsonFormatter.SerializerSettings = Settings;
        }
    }
}
