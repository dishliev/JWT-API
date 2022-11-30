using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace JWT_API
{
    public class AuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo
              .GetCustomAttributes(true)
              .OfType<AuthorizeAttribute>()
              .Distinct();

            if (authAttributes.Any())
            {
                operation.Responses.TryAdd("401",
                    new OpenApiResponse
                    {
                        Description = "Unauthorized"
                    });
                operation.Responses.TryAdd("403",
                    new OpenApiResponse
                    {
                        Description = "Forbidden"
                    });

                OpenApiSecurityScheme jwtbearerScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ jwtbearerScheme ] = Array.Empty<string>()
                    }
                };
            }
        }
    }
}
