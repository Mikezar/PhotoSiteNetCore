using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhotoSite.WebApi.Infrastructure;
using System;
using System.IO;
using System.Reflection;
using PhotoSite.ApiService;
using PhotoSite.ApiService.Data.Admin;
using PhotoSite.Data;
using PhotoSite.WebApi.Handlers;
using PhotoSite.WebApi.Options;

namespace PhotoSite.WebApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure of Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var path = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}");

            services.Configure<DatabaseOptions>(Configuration.GetSection(nameof(DatabaseOptions)));
            services.Configure<LoginOptions>(Configuration.GetSection(nameof(LoginOptions)));

            services.AddAuthentication(CustomTokenAuthOptions.DefaultSchemeName)
                .AddScheme<CustomTokenAuthOptions, CustomTokenAuthHandler>(
                    CustomTokenAuthOptions.DefaultSchemeName,
                    opts => {
                        //     opts.TokenHeaderName = "X-Custom-Token-Header";
                    }
                );

            services.AddData();
            services.AddApiServices();

            services.AddControllers();
            services.AddHealthChecks();
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSwagger(() => new OpenApiInfo
            {
                Version = "v1",
                Title = "Photosite API",
                Description = "ASP.NET Core Web API"
            }, $"{path}.xml");
        }

        /// <summary>
        /// Configure
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Photosite API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/");
            });
        }
    }
}
