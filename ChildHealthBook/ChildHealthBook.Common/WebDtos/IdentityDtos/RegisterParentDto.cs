using System;
using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.WebDtos.IdentityDtos
{
    class RegisterParentDto
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MinLength(8), MaxLength(50)]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        [MinLength(8), MaxLength(50)]
        public string confirmPassword { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
