using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class LoggedUser : AppUser
    {
        public string Token { get; set; }
    }
}