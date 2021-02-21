using System.ComponentModel.DataAnnotations;

namespace Application.Resources.User
{
    public class UserCredentialsResource
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}