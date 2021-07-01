using System;
using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.Identity.DTOs
{
    public class ParentRegisterDTO
    {
        public UserRegisterDTO UserCredentials { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
