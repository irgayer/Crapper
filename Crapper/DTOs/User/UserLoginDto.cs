using System.ComponentModel.DataAnnotations;

namespace Crapper.DTOs.User
{
    public class UserLoginDto
    {
        [EmailAddress(ErrorMessage = "Not an email address.")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
