using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.Identity.DTOs
{
    public class UserLoginDTO
    {
        /// <summary>
        /// Username used for login
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        /// <summary>
        /// User password used for login
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
    }
}
