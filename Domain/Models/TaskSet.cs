using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class TaskSet : ModifiedAndCreatedEntity
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(200, MinimumLength = 3)]
        public string Name {get; set;}
        public ICollection<Task> Tasks {get; set;}
        public ICollection<TaskSetTags> TaskSetTags {get; set; }
    }
}