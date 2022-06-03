using System.ComponentModel.DataAnnotations;

namespace Crapper.DTOs.User
{
    public class UserRegistrationDto
    {
        [StringLength(20, ErrorMessage = "Username must be 3 to 20 characters long.")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Not an email address.")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Password must be 8 to 20 characters long.", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
