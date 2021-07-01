using System;

namespace ChildHealthBook.Common.Identity.DTOs
{
    public class UserData
    {
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
    }
}
