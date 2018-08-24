using Abp.AspNetCore.Mvc.Controllers;
using Abp.AspNetCore.Mvc.Proxying;
using Abp.Auditing;
using Abp.Web.Api.ProxyScripting;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Abp.Web.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [DontWrapResult]
    [DisableAuditing]
    public class SiteServiceProxiesController : AbpController
    {
        private readonly IApiProxyScriptManager _proxyScriptManager;

        public SiteServiceProxiesController(IApiProxyScriptManager proxyScriptManager)
        {
            _proxyScriptManager = proxyScriptManager;
        }

        [HttpGet]
        public string GetAll(ApiProxyGenerationModel model)
        {
            var script = _proxyScriptManager.GetScript(model.CreateOptions());
            //return Content(script, "application/x-javascript");
            return script;
        }
    }
}