using ChildHealthBook.Identity.API.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ChildHealthBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        //private static void SeedDataIfNotInitialized(IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var logger = services.GetRequiredService<ILogger<Program>>();
        //        try
        //        {
        //            Seeder.Initialize(services);
        //        }
        //        catch (Exception ex)
        //        {

        //            logger.LogError(ex, "An error occured while seeding the database for ChildHealthBook");
        //        }
        //    }
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //    .ConfigureAppConfiguration((context, config) =>
                //    {
                //        var builtConfiguration = config.Build();

                //        string kvURL = builtConfiguration["KeyVaultConfig:KVUrl"];
                //        string tenantId = builtConfiguration["KeyVaultConfig:TenantId"];
                //        string clientId = builtConfiguration["KeyVaultConfig:ClientId"];
                //        string clientSecret = builtConfiguration["KeyVaultConfig:ClientSecretId"];

                //        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

                //        var client = new SecretClient(new Uri(kvURL), credential);
                //        config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
                //    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
