using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace PhotoSite.WebApi.Infrastructure
{
    internal static class SwaggerExts
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, Func<OpenApiInfo> openFunc, string xmlPath)
        {
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };

                c.DescribeAllEnumsAsStrings();
                c.SwaggerDoc("v1", openFunc());
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(securityRequirement);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
