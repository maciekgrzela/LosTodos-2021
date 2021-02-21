using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class UserCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}