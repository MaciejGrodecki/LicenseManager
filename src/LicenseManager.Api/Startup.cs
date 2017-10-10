using LicenseManager.Core.Repositories;
using LicenseManager.Infrastructure.EntityFramework;
using LicenseManager.Infrastructure.Mappers;
using LicenseManager.Infrastructure.Repositories;
using LicenseManager.Infrastructure.Services;
using LicenseManager.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;

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
            services.AddMvc()
                    .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);
            services.Configure<SqlSettings>(Configuration.GetSection("sql"));
            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));
            services.AddScoped<IComputerRepository, InMemoryComputerRepository>();
            services.AddScoped<ILicenseRepository, InMemoryLicenseRepository>();
            services.AddScoped<ILicenseTypeRepository, InMemoryLicenseTypeRepository>();
            //services.AddScoped<IRoomRepository, InMemoryRoomRepository>();
            services.AddScoped<IRoomRepository, SqlRoomRepository>();
            services.AddScoped<IUserRepository, InMemoryUserRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<ILicenseTypeService, LicenseTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<IComputerService, ComputerService>();
            services.AddScoped<IDataInitializer,DataInitializer>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.Configure<AppSettings>(Configuration.GetSection("app"));
            services.AddEntityFrameworkSqlServer().AddDbContext<LicensesContext>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:5050").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            env.ConfigureNLog("nlog.config");
            SeedData(app);
            
            app.UseMvc();
            
        }

        private void SeedData(IApplicationBuilder app)
        {
            var settings = app.ApplicationServices.GetService<IOptions<AppSettings>>();
            if(settings.Value.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
        }
    }
}
