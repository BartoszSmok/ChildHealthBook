using System.ComponentModel.DataAnnotations;

namespace ChildHealthBook.Common.Identity.DTOs
{
    public class UserRegisterDTO
    {
        /// <summary>
        /// Username used for login in future
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(200)]
        public string UserName { get; set; }

        /// <summary>
        /// User email used for marketing purposes in the future
        /// </summary>
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// Usre password used for login in the future
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmpassword is required")]
        [MaxLength(200)]
        public string ConfirmPassword { get; set; }
    }
}
