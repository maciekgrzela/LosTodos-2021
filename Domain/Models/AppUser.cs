using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<TaskSet> TaskSets { get; set; }
    }
}