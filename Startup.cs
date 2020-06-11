using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organization.API.Infrastructure.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Organization.API.Infrastructure.Repositories;
using Organization.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Organization.API.Models;

namespace Organization.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrganizationDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MyMDBContext")));

            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IChildEntityRepository, ChildEntityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUser_EntityRepository, User_EntityRepository>();
            //services.AddScoped<IAffectedPermissionRepository, AffectedPermissionRepository>();
            services.AddScoped<NotificationPermissionRepository>();
            services.AddScoped<IWallPermissionRepository, WallPermissionRepository>();
            services.AddScoped<IOrganizationPermissionRepository, OrganizationPermissionRepository>();
            services.AddScoped<INotificationPermissionRepository, NotificationPermissionRepository>();
            services.AddScoped<IOkrPermissionRepository, OkrPermissionRepository>();
            services.AddControllers().AddNewtonsoftJson(option =>
            option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
