using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class TodoSet : BaseEntity
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(200, MinimumLength = 3)]
        public string Name {get; set;}
        public AppUser Owner {get; set;}
        public string OwnerId {get; set;}
        public ICollection<Todo> Todos {get; set;}
        public ICollection<TodoSetTags> TodoSetTags {get; set; }
    }
}