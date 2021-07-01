using Common.Identity.Setup;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChildHealthBook.Identity.API.Authentication
{
    public class AuthenticationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {      
        }
    }
}
