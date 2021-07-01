using ChildHealthBook.Analytics.API.Analytics;
using ChildHealthBook.Analytics.API.Communication.Bridge;
using ChildHealthBook.Analytics.API.Communication.Strategy;
using ChildHealthBook.Analytics.API.Controllers;
using ChildHealthBook.Analytics.API.Repository;
using ChildHealthBook.Analytics.API.Repository.Setup;
using ChildHealthBook.Common.AnalyticsDtos;
using ChildHealthBook.Common.Identity.DTOs;
using ChildHealthBook.Common.WebDtos.ChildDtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ChildHealthBook.Analytics.API
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

            ConfigureDbConnection(services);
            AddAnalyticsToDI(services);
            AddRepositoriesToDI(services);
            AddBridgesSetupToDI(services);
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddSingleton<HistoryController>();
        }

        private void ConfigureDbConnection(IServiceCollection services)
        {
            services.Configure<HistoryDatabaseSettings>(Configuration.GetSection(nameof(HistoryDatabaseSettings)));
            services.AddSingleton<IHistoryDatabaseSettings>(sp => sp.GetRequiredService<IOptions<HistoryDatabaseSettings>>().Value);
        }

        private void AddBridgesSetupToDI(IServiceCollection services)
        {
            services.AddScoped<ICommunicationBridge<ChildWithEventsReadDto>, CommunicationBridge<ChildWithEventsReadDto>>();
            services.AddScoped<ICommunicationBridge<UserData>, CommunicationBridge<UserData>>();

            services.AddScoped<ICommunicationStrategy<ChildWithEventsReadDto>, HttpClientCommunicationStrategy<ChildWithEventsReadDto>>();
            services.AddScoped<ICommunicationStrategy<UserData>, HttpClientCommunicationStrategy<UserData>>();
        }

        private void AddRepositoriesToDI(IServiceCollection services)
        {
            services.AddTransient<IHistoryRecordRepository<ChildrenAverageAgeRecord>, HistoryRecordRepository<ChildrenAverageAgeRecord>>();
            services.AddTransient<IHistoryRecordRepository<ChildrenAverageCountPerParentRecord>, HistoryRecordRepository<ChildrenAverageCountPerParentRecord>>();
            services.AddTransient<IHistoryRecordRepository<VaccinationFactorRecord>, HistoryRecordRepository<VaccinationFactorRecord>>();
        }

        private void AddAnalyticsToDI(IServiceCollection services)
        {
            services.AddScoped<FactorCounter>();
            services.AddScoped<AverageCounter>();
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
