﻿using IoC;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Text.Json.Serialization;
using WebAPI.Configurations;
using WebAPI.StartupExtensions;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("/", "http://localhost:3000");
            }));

            // ----- Database -----
            services.AddCustomizedDatabase(Configuration, _env);

            // ----- Auth -----
            services.AddCustomizedAuth(Configuration);

            // ----- Http -----
            services.AddCustomizedHttp(Configuration);

            // ----- AutoMapper -----
            services.AddAutoMapperSetup();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // ----- Hash -----
            services.AddCustomizedHash(Configuration);

            // .NET Native DI Abstraction
            RegisterServices(services);

            services.AddControllers()
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                    // x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            // Nuget package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            // .AddNewtonsoftJson(
            //     options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            // );

            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            });

            // Add ApiExplorer to discover versions
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddEndpointsApiExplorer();

            // ----- Swagger UI -----
            services.AddCustomizedSwagger(_env);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // ----- Error Handling -----
            app.UseCustomizedErrorHandling(_env);

            app.UseRouting();

            // ----- CORS -----
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader());
            app.UseCors(x => x
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .SetIsOriginAllowed(origin => true) // allow any origin
                 .AllowCredentials()); // allow credentials

            // ----- Auth -----
            app.UseCustomizedAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ----- Swagger UI -----
            app.UseCustomizedSwagger(_env);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
