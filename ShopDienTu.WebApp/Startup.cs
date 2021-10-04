using System;
using System.Globalization;
using ShopDienTu.ApiIntegration;
using ShopDienTu.WebApp.LocalizationResources;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using ShopDienTu.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentValidation;

namespace ShopDienTu.WebApp
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
            services.AddHttpClient();
            var cultures = new[]
          {
                new CultureInfo("en"),
                new CultureInfo("vi"),
            };

            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>())
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
            {
                ops.UseAllCultureProviders = false;
                ops.ResourcesPath = "LocalizationResources";
                ops.RequestLocalizationOptions = o =>
                {
                    o.SupportedCultures = cultures;
                    o.SupportedUICultures = cultures;
                    o.DefaultRequestCulture = new RequestCulture("vi");
                };
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options =>
             {
                 options.LoginPath = $"/{CultureInfo.CurrentCulture.Name}/Account/Login";
                 options.AccessDeniedPath = "/User/Forbidden/";
             });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<ISliderApiClient, SliderApiClient>();
            services.AddTransient<IProductApiClient, ProductApiClient>();
            services.AddTransient<ICategoryApiClient, CategoryApiClient>();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IOrderApiClient, OrderApiClient>();
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
            app.UseAuthentication();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.UseRequestLocalization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "Product Category En",
                  pattern: "{culture}/categories/{id}/{ceoalias}", new
                  {
                      controller = "Product",
                      action = "Category"
                  });

                endpoints.MapControllerRoute(
                  name: "Product Category Vn",
                  pattern: "{culture}/danh-muc/{id}/{ceoalias}", new
                  {
                      controller = "Product",
                      action = "Category"
                  });

                endpoints.MapControllerRoute(
                   name: "Product Detail En",
                   pattern: "{culture}/products/{id}/{ceoalias}", new
                   {
                       controller = "Product",
                       action = "Detail"
                   });

                endpoints.MapControllerRoute(
                  name: "Product Detail Vn",
                  pattern: "{culture}/san-pham/{id}/{ceoalias}", new
                  {
                      controller = "Product",
                      action = "Detail"
                  });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{culture=vi}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
