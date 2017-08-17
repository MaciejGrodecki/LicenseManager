using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.Mappers;
using LicenseManager.Infrastructure.Repositories;
using LicenseManager.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LicenseManager.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddScoped<IComputerRepository, InMemoryComputerRepository>();
            services.AddScoped<ILicenseRepository, InMemoryLicenseRepository>();
            services.AddScoped<ILicenseTypeRepository, InMemoryLicenseTypeRepository>();
            services.AddScoped<IRoomRepository, InMemoryRoomRepository>();
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ILicenseTypeService, LicenseTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton(AutoMapperConfig.Initialize());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
