using Microsoft.AspNetCore.Identity;
using System;

namespace Common.Identity.Setup
{
    public class User : IdentityUser<Guid>
    {
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
    }
}
