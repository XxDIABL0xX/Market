using System.ComponentModel.DataAnnotations;

namespace Market.ViewModel
{
    // ViewModel for user registration
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")] // Display name for the email field
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")] // Display name for the password field
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")] // Display name for the confirm password field
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
