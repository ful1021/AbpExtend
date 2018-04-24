using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace Abp.WebApi.Swagger
{
    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            //portal 动态映射的api无法识别，默认全部加上，但是非必填
            operation.parameters = operation.parameters ?? new List<Parameter>();
            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                description = "access token",
                required = false,
                type = "string"
            });
        }
    }
}