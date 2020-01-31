﻿using LetPortal.Core;
using LetPortal.Core.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace LetPortal.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevCors", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyOrigin()
                           .AllowCredentials()
                           .WithExposedHeaders("X-Token-Expired");
                });

                options.AddPolicy("DockerLocalCors", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowAnyOrigin()
                           .AllowCredentials()
                           .WithExposedHeaders("X-Token-Expired");
                });

                options.AddPolicy("ProdCors", builder =>
                {
                    builder.WithExposedHeaders("X-Token-Expired");
                });
            });
            services.AddOcelot();
            services.AddLetPortal(Configuration, options =>
            {
                options.EnableDatabaseConnection = false;
                options.EnableMicroservices = true;
                options.EnableSerilog = true;
                options.EnableServiceMonitor = true;
            }).AddGateway();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("DevCors");
            }
            else if (env.IsEnvironment("DockerLocal"))
            {
                app.UseCors("DockerLocalCors");
            }
            else
            {
                app.UseHsts();
                app.UseCors("ProdCors");
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMiddleware<GenerateTraceIdMiddleware>();
            app.UseLetPortal(appLifetime, options =>
            {
                options.EnableCheckUserSession = true;
                options.EnableWrapException = true;
                options.SkipCheckUrls = new string[] { "api/configurations" };
            });
            app.UseOcelot().Wait();
            app.UseMvc();

        }
    }
}
