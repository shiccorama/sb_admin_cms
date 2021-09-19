using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sb_light_admin.BL.Interfaces;
using sb_light_admin.BL.Repositories;
using sb_light_admin.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sb_light_admin.BL.Mapper;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Razor;
using sb_light_admin.view.Resources;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;

namespace sb_light_admin.view
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
                services.AddControllersWithViews()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization(options =>
                        {
                            options.DataAnnotationLocalizerProvider = (type, factory) =>
                            factory.Create(typeof(SharedResource));
                        })
                    .AddNewtonsoftJson(opt => {
                            opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                        });

            // add microsoft identity services to write IdentityUser and identityRole to database :

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<DbContainer>()
              .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
            


            // add connection string to open database sqlserver source :

            // note that you can add multiple connection string by adding the same line underneath but with changing the connection name :

            services.AddDbContextPool<DbContainer>(opts =>
                     opts.UseSqlServer(Configuration.GetConnectionString("sb_light_admin_Connection")));

            // add dependency injection (scoped type) into memory :

            #region Dependency Injection :

            services.AddScoped<IDepartmentMirror, DepartmentMirror>();

            services.AddScoped<IEmployeeMirror, EmployeeMirror>();

            services.AddScoped<IMailsRep, MailsRep>();

            services.AddScoped<ICountryMirror, CountryMirror>();

            services.AddScoped<ICityMirror, CityMirror>();

            services.AddScoped<IDistrictMirror, DistrictMirror>();



            #endregion


            // this is to add mapper configuration to map between database and mirrors :

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            // this is to add API configuration services :

            //services.AddControllers();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // this region is located for Globlization or Multi-language Configuration :

            #region
            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });

            #endregion


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // this end point is a reference to Areas (divided your controllers into areas) :

            #region

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            #endregion

            #region  api routing :


            //app.UseEndpoints(a => a.MapDefaultControllerRoute());

            #endregion



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
