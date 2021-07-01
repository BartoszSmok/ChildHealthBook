using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.Identity.DTOs
{
    public class UserRegisterDTO
    {
        /// <summary>
        /// Username used for login in future
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string UserName { get; set; }

        /// <summary>
        /// User email used for marketing purposes in the future
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// Usre password used for login in the future
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string ConfirmPassword { get; set; }
    }
}
