using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildHealthBook.Notification.Service.Data;
using ChildHealthBook.Notification.Service.Settings;
using Microsoft.Extensions.Configuration;

namespace ChildHealthBook.Notification.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    AzureQueueConfigData options = configuration.GetSection("AzureQueues").Get<AzureQueueConfigData>();
                    MongoSettings dbSettings = configuration.GetSection("DatabaseSettings").Get<MongoSettings>();
                    SmtpSettings smtpSetting = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

                    services.AddSingleton(options);
                    services.AddSingleton(dbSettings);
                    services.AddSingleton(smtpSetting);

                    services.AddSingleton<IMsgContext, MsgContext>();

                    services.AddHostedService<MessagesReciver>();
                    services.AddHostedService<MessagesSender>();
                });
    }
}
