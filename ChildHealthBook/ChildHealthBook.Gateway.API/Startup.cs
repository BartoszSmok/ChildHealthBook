using ChildHealthBook.Common.AnalyticsDtos;
using ChildHealthBook.Gateway.API.Clients;
using ChildHealthBook.Gateway.API.Communication.Bridge;
using ChildHealthBook.Gateway.API.Communication.Strategy;
using ChildHealthBook.Gateway.API.Communication.Strategy.Identity;
using ChildHealthBook.Gateway.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// test comment for changes
namespace ChildHealthBook.Gateway.API
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
            services.Configure<GatewayApiSettings>(Configuration.GetSection(nameof(GatewayApiSettings)));
            services.AddSingleton<IGatewayApiSettings>(sp => sp.GetRequiredService<IOptions<GatewayApiSettings>>().Value);

            services.AddTransient<IGatewayService, GatewayService>();
            services.AddTransient<IAzureQueueClient, AzureQueueClient>();

            services.AddHttpClient<ChildClient>(options =>
            {
                options.BaseAddress = new Uri("http://childhealthbook.child.api/");
            });

            services.AddControllers();
            AddIdentityCommunicationBridge(services);
            AddAnalyticsCommunicationBridges(services);
            services.AddSwaggerGen();
        }

        private void AddAnalyticsCommunicationBridges(IServiceCollection services)
        {
            services.AddScoped<IdentityCommunicationBridge>();
            services.AddScoped<IIdentityCommunicationStrategy, HttpClientIdentityCommunication>();
        }

        private void AddIdentityCommunicationBridge(IServiceCollection services)
        {
            services.AddScoped<AnalyticsCommunicationBridge<VaccinationFactorRecord>>();
            services.AddScoped<AnalyticsCommunicationBridge<IEnumerable<VaccinationFactorRecord>>>();
            services.AddScoped<AnalyticsCommunicationBridge<ChildrenAverageAgeRecord>>();
            services.AddScoped<AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageAgeRecord>>>();
            services.AddScoped<AnalyticsCommunicationBridge<ChildrenAverageCountPerParentRecord>>();
            services.AddScoped<AnalyticsCommunicationBridge<IEnumerable<ChildrenAverageCountPerParentRecord>>>();

            services.AddScoped<IAnalyticsCommunicationStrategy<VaccinationFactorRecord>, HttpClientCommunicationStrategy<VaccinationFactorRecord>>();
            services.AddScoped<IAnalyticsCommunicationStrategy<IEnumerable<VaccinationFactorRecord>>, HttpClientCommunicationStrategy<IEnumerable<VaccinationFactorRecord>>>();
            services.AddScoped<IAnalyticsCommunicationStrategy<ChildrenAverageAgeRecord>, HttpClientCommunicationStrategy<ChildrenAverageAgeRecord>>();
            services.AddScoped<IAnalyticsCommunicationStrategy<IEnumerable<ChildrenAverageAgeRecord>>, HttpClientCommunicationStrategy<IEnumerable<ChildrenAverageAgeRecord>>>();
            services.AddScoped<IAnalyticsCommunicationStrategy<ChildrenAverageCountPerParentRecord>, HttpClientCommunicationStrategy<ChildrenAverageCountPerParentRecord>>();
            services.AddScoped<IAnalyticsCommunicationStrategy<IEnumerable<ChildrenAverageCountPerParentRecord>>, HttpClientCommunicationStrategy<IEnumerable<ChildrenAverageCountPerParentRecord>>>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
