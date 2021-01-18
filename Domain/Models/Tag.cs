using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Tag : ModifiedAndCreatedEntity
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(30, MinimumLength = 2)]
        public string Name {get; set;}
        public ICollection<TaskSetTags> TaskSetTags {get; set;}
    }
}