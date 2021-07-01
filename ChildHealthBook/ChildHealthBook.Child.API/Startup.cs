using ChildHealthBook.Child.API.Clients;
using ChildHealthBook.Child.API.DAL;
using ChildHealthBook.Child.API.Models;
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

namespace ChildHealthBook.Child.API
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
            services.Configure<ChildApiSettings>(Configuration.GetSection(nameof(ChildApiSettings)));
            services.AddSingleton<IChildApiSettings>(sp => sp.GetRequiredService<IOptions<ChildApiSettings>>().Value);

            services.AddTransient<IChildRepository, ChildRepository>();
            services.AddTransient<IEventRepository, EventRepository>();

            services.AddHostedService<AddNewChildQueueClient>();
            services.AddHostedService<AddMedicalEventQueueClient>();
            services.AddHostedService<AddPersonalEventQueueClient>();
            services.AddHostedService<AddExaminationQueueClient>();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
