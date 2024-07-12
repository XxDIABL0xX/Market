using System.ComponentModel.DataAnnotations;

namespace Market.ViewModel
{
    // ViewModel for user login
    public class LoginViewModel
    {
        [Required] // Email is required
        [EmailAddress] // Must be a valid email address
        [Display(Name = "Email")] // Display name for the email field
        public string Email { get; set; }

        [Required] // Password is required
        [DataType(DataType.Password)] // Password should be treated as a password field
        [Display(Name = "Password")] // Display name for the password field
        public string Password { get; set; }

        [Display(Name = "Remember me")] // Display name for the remember me checkbox
        public bool RememberMe { get; set; }
    }
}

