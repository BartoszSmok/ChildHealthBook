using ChildHealthBook.Common.Identity.Attribs;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.Identity.DTOs
{
    public class ParentRegisterDTO
    {
        public UserRegisterDTO UserCredentials { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(30)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
    }
}
