using ECommerceAPI.DataAccess;
using ECommerceAPI.Entities;
using ECommerceAPI.HealthChecks;
using ECommerceAPI.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace ECommerceAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration);
            services.AddInjectionDependency();

            services.AddControllers();

            services.AddDbContext<ECommerceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
                options.LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging();
            });

            services.AddAutoMapperConfiguration();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerce API",
                    Version = "v1",
                    Description = "API para el curso FullStack"
                });
            });

            services.AddHealthChecks()
                .AddCheck("ECommerceAPI", _ => HealthCheckResult.Healthy(), new[] { "servicio" })
                .AddTypeActivatedCheck<PingHealthCheck>("Base de Datos", HealthStatus.Healthy,
                                                        new[] { "basedatos" },
                                                        new object[] { Configuration.GetValue<string>("IPServidor") })
                .AddTypeActivatedCheck<PingHealthCheck>("Google", HealthStatus.Healthy,
                                                        new[] { "internet" }, "google.com")
                .AddTypeActivatedCheck<PingHealthCheck>("Azure", HealthStatus.Healthy,
                                                        new[] { "nube" }, "portal.azure.com")
                .AddDbContextCheck<ECommerceDbContext>("EF Core", HealthStatus.Healthy,
                                                        new[] { "basedatos" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerceAPI v1"));
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "ECommerceAPI v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/health/externos", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                    Predicate = x => x.Tags.Contains("nube")
                });

                endpoints.MapHealthChecks("/health/internos", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                    Predicate = x => x.Tags.Contains("basedatos")
                });

                endpoints.MapControllers();
            });
        }
    }
}
