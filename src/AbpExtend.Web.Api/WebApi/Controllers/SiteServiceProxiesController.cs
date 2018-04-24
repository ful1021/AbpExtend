using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Web.Models;
using Abp.Web.Security.AntiForgery;
using Abp.WebApi.Controllers.Dynamic.Formatters;
using Abp.WebApi.Scripts;

namespace Abp.WebApi.Controllers
{
    [DontWrapResult]
    [DisableAuditing]
    [DisableAbpAntiForgeryTokenValidation]
    public class SiteServiceProxiesController : AbpApiController
    {
        private readonly SiteScriptProxyManager _scriptProxyManager;

        public SiteServiceProxiesController(SiteScriptProxyManager scriptProxyManager)
        {
            _scriptProxyManager = scriptProxyManager;
        }

        /// <summary>
        /// Gets javascript proxy for given service name.
        /// </summary>
        /// <param name="name">Name of the service</param>
        /// <param name="type">Script type</param>
        public HttpResponseMessage Get(string name, ProxyType type = ProxyType.Customized)
        {
            var script = _scriptProxyManager.GetScript(name, type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }

        /// <summary>
        /// Gets javascript proxy for all services.
        /// </summary>
        /// <param name="type">Script type</param>
        [Route("api/SiteServiceProxies/GetAll")]
        [HttpGet]
        [AbpAllowAnonymous]
        public HttpResponseMessage GetAll(ProxyType type = ProxyType.Customized)
        {
            var script = _scriptProxyManager.GetAllScript(type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
    }
}
