using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spg.SpengerBanger.Domain.Interfaces;
using Spg.SpengerBanger.Domain.Model;
using Spg.SpengerBanger.Infrastructure;
using Spg.SpengerBanger.Services.AuthService;
using Spg.SpengerBanger.Services.CategoryService;
using Spg.SpengerBanger.Services.ProductService;
using Spg.SpengerBanger.Services.ShoppingCartService;
using Spg.SpengerBanger.Services.ShopService;
using Spg.SpengerBanger.Services.UserService;
using Spg.SpengerBanger.MvcFrontEnd.Services;

namespace Spg.SpengerBanger.MvcFrontEnd
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
            services.AddControllersWithViews();
            services.AddDbContext<SpengerBangerContext>(optionsBuilder =>
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlite(Configuration["AppSettings:Database"]);
                }
            });

            /// *** Authentication, Authorization hinzufügen
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("PolicyForCustomers", p => p.RequireRole(UserRoles.Customer.ToString(), UserRoles.Administrator.ToString()));
                o.AddPolicy("PolicyForAdmins", p => p.RequireRole(UserRoles.Administrator.ToString()));
            });
            /// ***

            services.AddTransient<IAuthService, HttpAuthService>();
            services.AddTransient<IAuthProvider, DbAuthProvider>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IShopService, ShopService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
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

            /// *** Authentication, Authorization aktivieren
            app.UseAuthentication();
            app.UseAuthorization();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            /// ***

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
