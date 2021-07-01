using Common.Identity.Setup;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity;

namespace ChildHealthBook.Identity.API.Authentication
{
    public class AuthenticationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("IdentityUser");
            base.OnModelCreating(builder);
        }
    }
}
