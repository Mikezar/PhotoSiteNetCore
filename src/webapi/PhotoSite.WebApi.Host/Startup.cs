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
using Microsoft.EntityFrameworkCore;
using PhotoSite.ApiService;
using PhotoSite.ApiService.Data.Admin;
using PhotoSite.Data.Base;
using PhotoSite.WebApi.Handlers;
using PhotoSite.WebApi.Helpers;
using PhotoSite.WebApi.Options;

namespace PhotoSite.WebApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="currentEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
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

            services.Configure<LoginOptions>(Configuration.GetSection(nameof(LoginOptions)));

            services.AddAuthentication(CustomTokenAuthOptions.DefaultSchemeName)
                .AddScheme<CustomTokenAuthOptions, CustomTokenAuthHandler>(
                    CustomTokenAuthOptions.DefaultSchemeName,
                    opts => {
                        //     opts.TokenHeaderName = "X-Custom-Token-Header";
                    }
                );

            services.AddMemoryCache();
            services.AddApiServices();
            ConfigureDependencies(services);
            AddDb(services);

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
        /// Configure dependencies
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureDependencies(IServiceCollection services)
        {

        }

        private void AddDb(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContext<MainDbContext>(options =>
                    options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                services.AddDbContext<MainDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            }
        }

        /// <summary>
        /// Configure
        /// </summary>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseIpFilter();

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
