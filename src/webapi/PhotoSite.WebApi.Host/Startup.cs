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
using PhotoSite.Data;
using PhotoSite.Data.Base;
using PhotoSite.WebApi.Infrastructure.Authorization;
using PhotoSite.WebApi.Infrastructure.Middlewares;

namespace PhotoSite.WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _currentEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<LoginOptions>(Configuration.GetSection(nameof(LoginOptions)));

            services.AddAuthentication(CustomTokenAuthOptions.DefaultSchemeName)
                .AddScheme<CustomTokenAuthOptions, CustomTokenAuthHandler>(
                    CustomTokenAuthOptions.DefaultSchemeName,
                    opts => {
                        //     opts.TokenHeaderName = "X-Custom-Token-Header";
                    }
                );

            //services.AddMvc(options =>
            //{
            //    options.Filters.Add<CustomExceptionFilterAttribute>();
            //});

            services.AddMemoryCache();
            services.AddRepositories();
            services.AddCaches();
            services.AddApiServices();
            ConfigureDependencies(services);
            AddDb(services);

            services.AddControllers();
            services.AddHealthChecks();
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen();
            services.ConfigureSwagger(() => new OpenApiInfo
            {
                Version = "v1",
                Title = "Photosite API",
                Description = "ASP.NET Core Web API"
            });
        }

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

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/error-development");
            }
            //else
            //{
            //    app.UseExceptionHandler("/error");
            //}

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Photosite API V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCustomExceptionHandler();
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
