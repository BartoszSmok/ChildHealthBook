using System;

namespace ChildHealthBook.Web.Models.Session
{
    public class AuthUserSession
    {
        public Guid Id { get; set; }
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
