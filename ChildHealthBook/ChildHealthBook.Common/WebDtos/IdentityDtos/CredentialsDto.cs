using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.WebDtos.IdentityDtos
{
    public class CredentialsDto
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Login { get; set; }
        [Required]
        [MinLength(8), MaxLength(50)]
        public string Password { get; set; }
    }
}
