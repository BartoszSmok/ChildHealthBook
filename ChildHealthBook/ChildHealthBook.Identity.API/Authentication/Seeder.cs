using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChildHealthBook.Identity.API.Authentication
{
    public static class Seeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AuthenticationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AuthenticationDbContext>>()))
            {
                context.Database.Migrate();
            }
        }
    }
}