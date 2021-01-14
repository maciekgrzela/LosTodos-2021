using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class TaskSet
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(200, MinimumLength = 3)]
        public string Name {get; set;}
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created {get; set;} = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModified {get; set;}
        public ICollection<Task> Tasks {get; set;}
        public ICollection<TaskSetTags> TaskSetTags {get; set; }
    }
}