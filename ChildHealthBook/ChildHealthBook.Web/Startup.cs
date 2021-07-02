using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Web.Communication;
using ChildHealthBook.Web.CookieServices;
using ChildHealthBook.Web.CookieServices.Serializers;
using ChildHealthBook.Web.CookieServices.Token;
using ChildHealthBook.Web.CookieServices.Validator;
using ChildHealthBook.Web.Models.Session;
using ChildHealthBook.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Options;
using System;

namespace ChildHealthBook.Web
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
            services.Configure<WebSettings>(Configuration.GetSection(nameof(WebSettings)));
            services.AddSingleton<IWebSettings>(sp => sp.GetRequiredService<IOptions<WebSettings>>().Value);
            //services.AddHttpClient<ParentService>(options =>
            //{
            //    options.BaseAddress = new Uri("http://childhealthbook.gateway.api/");
            //});
            services.AddControllersWithViews();
            services.AddScoped<ParentService>();
            services.AddScoped<AnalyticsService>();
            AddCommunicationToDI(services);
            AddCookieServicesToDI(services);
        }

        private void AddCookieServicesToDI(IServiceCollection services)
        {
            services.AddScoped<TokenHelper>();
            services.AddScoped<AuthenticatedUserSessionBuilder>();
            services.AddScoped<UserSessionCookieValidator>();
            services.AddScoped<ICookieDeserializer<AuthUserSession>, UserCookieDeserializer>();
        }

        private void AddCommunicationToDI(IServiceCollection services)
        {
            services.AddSingleton<IIdentityRegisterCommunication<ParentRegisterDTO>, IdentityRegisterCommunication<ParentRegisterDTO>>();
            services.AddSingleton<IIdentityRegisterCommunication<UserRegisterDTO>, IdentityRegisterCommunication<UserRegisterDTO>>();
            services.AddSingleton<IdentityTokenCommunication>();
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{val?}");
            });
        }
    }
}
