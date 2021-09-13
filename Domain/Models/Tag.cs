using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Tag : BaseEntity
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(30, MinimumLength = 2)]
        public string Name {get; set;}
        public string OwnerId {get; set;}
        public AppUser Owner {get; set;}
        public ICollection<TodoSetTags> TodoSetTags {get; set;}
    }
}