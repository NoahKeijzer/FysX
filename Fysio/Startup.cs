using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fysio.Controllers;
using DomainServices.Interfaces;
using EFInfrastructure;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Fysio
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
            services.AddDbContext<FysioDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"), b => b.MigrationsAssembly("Fysio")));

            services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Security")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization(options => options.AddPolicy("RequireFysio", policy => policy.RequireClaim("Claim.Fysio")));

            services.AddAuthorization(options => options.AddPolicy("RequireStudent", policy => policy.RequireClaim("Claim.Student")));

            services.AddAuthorization(options => options.AddPolicy("RequireTreator", policy => policy.RequireClaim("Claim.Treator")));

            services.AddAuthorization(options => options.AddPolicy("RequirePatient", policy => policy.RequireClaim("Claim.Patient")));

            services.AddControllersWithViews();

            services.AddScoped<IPatientRepository, DBPatientRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                    name: "patient",
                    pattern: "patient/{Id:int}",
                    defaults: new { controller = "Home", action = "Patient" });

                endpoints.MapControllerRoute(
                    name: "patiensList",
                    pattern: "patient",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

        }

    }
}
