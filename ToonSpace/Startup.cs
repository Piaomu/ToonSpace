using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToonSpace.Data;
using ToonSpace.Models;
using ToonSpace.Services;
using ToonSpace.Services.Interfaces;

namespace ToonSpace
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                services.AddIdentity<ToonUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddScoped<IRelationService, RelationService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IImageService, BasicImageService>();
            services.AddScoped<DataService>();

            services.AddMvc();
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "939598031066-ki41sd7ijpshmkv6je5h1rv5qr5974oc.apps.googleusercontent.com";
                    options.ClientSecret = "GmEq1rtzoRsYPkVXrn_THI4H";
                });
                //.AddTwitter(options =>
                //{
                //    options.ConsumerKey = "Z0roH3IzOGs3t04k4cZOvoa9V";
                //    options.ConsumerSecret = "QOAavDgPyr1JoiLC6FovXr1eF1SuW8BMlkCZuDjQx7PrnhzlEg";
                //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Landing}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
