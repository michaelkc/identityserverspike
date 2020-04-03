using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace Mvc2
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient();
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "cookie";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("cookie", options =>
                {
                    options.Cookie.Name = "mvc2";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = SharedConfiguration.IdentityServer.BaseUrl;
                    options.ClientId = SharedConfiguration.Mvc.ClientId;
                    options.ClientSecret = SharedConfiguration.Mvc.ClientSecret;

                    options.ResponseType = "code";
                    //options.Scope.Add("customer.api");
                    //options.Scope.Add("offline_access");

                    options.UsePkce = true;

                    options.SaveTokens = true;

                    var handler = new JwtSecurityTokenHandler();
                    handler.MapInboundClaims = false;

                    options.SecurityTokenValidator = handler;
                });
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
