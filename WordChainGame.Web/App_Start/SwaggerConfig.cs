using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using WordChainGame.Web.App_Start;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WordChainGame.Web.App_Start
{
    public class SwaggerConfig

    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "CentralRegistrars.Web.Api");



                    c.ApiKey("token")
                        .Description("API Key Authentication")
                        .Name("Bearer")
                        .In("header");

                    c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();

                    c.DocumentFilter<AuthTokenOperation>();
                })
                .EnableSwaggerUi(config => {
                    //config.DocExpansion(DocExpansion.Full);
                });
        }

        public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
                var isAuthorized = filterPipeline
                                                 .Select(filterInfo => filterInfo.Instance)
                                                 .Any(filter => filter is IAuthorizationFilter);

                var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

                if (isAuthorized && !allowAnonymous)
                {
                    if (operation.parameters == null)
                    {
                        operation.parameters = new List<Parameter>();
                    }
                    operation.parameters.Add(new Parameter
                    {
                        name = "Authorization",
                        @in = "header",
                        description = "access token",
                        required = true,
                        type = "string"
                    });
                }
            }
        }

        class AuthTokenOperation : IDocumentFilter
        {
            public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
            {
                swaggerDoc.paths.Add("/Token", new PathItem
                {
                    post = new Operation
                    {
                        tags = new List<string> { "Auth" },
                        consumes = new List<string>
                {
                    "application/x-www-form-urlencoded"
                },
                        parameters = new List<Parameter> {
                    new Parameter
                    {
                        type = "string",
                        name = "grant_type",
                        required = true,
                        @in = "formData"
                    },
                    new Parameter
                    {
                        type = "string",
                        name = "username",
                        required = false,
                        @in = "formData"
                    },
                    new Parameter
                    {
                        type = "string",
                        name = "password",
                        required = false,
                        @in = "formData"
                    }
                }
                    }
                });
            }
        }
    }
}