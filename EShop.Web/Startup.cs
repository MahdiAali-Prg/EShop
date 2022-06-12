using System.IO;
using EShop.Common.Services.RouteConstraint;
using EShop.Data.Context;
using EShop.Data.Repositories.GenericRepository;
using EShop.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace EShop.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<EShopContext>(options =>
            {
                options.UseSqlServer(_configuration["ConnectionStrings:MainConnection"],
                    o => o.MigrationsAssembly("EShop.Web"));
            });

            #region Congiguration

            services.Configure<RouteOptions>(r => r.ConstraintMap.Add("positive", typeof(PositivePageId)));

            #endregion


            #region Dependency Life Time

            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));

            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EShopContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DataBaseConfiguration.PopulateMigrate(context);
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images"
            });


            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
