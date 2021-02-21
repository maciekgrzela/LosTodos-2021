using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.User
{
    public class UpdateUserResource
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string OldEmail { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}