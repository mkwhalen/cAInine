using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Interfaces.Repositories;
using CAInine.Core.Interfaces.Services;
using CAInine.Core.Models.Configuration;
using CAInine.Infrastructure.Business.Services;
using CAInine.Infrastructure.Data.Contexts;
using CAInine.Infrastructure.Data.Providers;
using CAInine.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CAInine.Clients.Api
{

    /// <summary>
    /// Startup class to configure services and IoC.
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MessagingService.Platform.Web.Startup"/> class.
        /// </summary>
        /// <param name="env">Env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Configures the services.
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            
            services.AddSwaggerGen(config =>
            {
                // gets xml comments directory to show in swagger
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                config.SwaggerDoc("v1", new Info { Title = "cAInine API", Version = "V1" });

                if (File.Exists(commentsFile))
                {
                    config.IncludeXmlComments(commentsFile);
                }
            });
            services.AddDbContext<CainineDataContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("DatabaseConnectionString"))
            );

            services.AddScoped<HttpClient>();

            services.AddOptions();
            services.Configure<Urls>(Configuration.GetSection("Urls"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            // add our implementations now
            // providers
            services.AddScoped<IBlobProvider, AzureBlobStorageProvider>();
            services.AddScoped<IBreedDetectionProvider, WhatDogBreedDetectionProvider>();

            // repositories
            services.AddScoped<ISubmittedDogRepository, SubmittedDogRepository>();

            // services
            services.AddScoped<IDogProcessingService, DogProcessingService>();
        

        }

        /// <summary>
        /// Configure the specified app and env.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <returns>The configure.</returns>
        /// <param name="app">App.</param>
        /// <param name="env">Env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "cAInine API v1"));
        }
    }
}
