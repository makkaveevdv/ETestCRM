using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETestCRM.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ETestCRM
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:ETestCRMOrders:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:ETestCRMIdentity:ConnectionString"]));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IOrderRepository, EFOrderRepository>();
            //services.AddTransient<IOrderRepository, FakeOrderRepository>();
            services.AddTransient<IGetUserOnline, GetUserOnline>();

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            { 
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            //AppIdentityDbContext.CreateSuperAdminAccount(app.ApplicationServices, Configuration).Wait();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "MyOrders", action = "Index", sortTime = "all", orderPage = 1 });                
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            //SeedData.EnsurePopulated(app);
        }
    }
}
