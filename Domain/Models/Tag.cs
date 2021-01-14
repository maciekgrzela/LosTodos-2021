using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Tag
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(30, MinimumLength = 2)]
        public string Name {get; set;}
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created {get; set;} = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModified {get; set;}
        public ICollection<TaskSetTags> TaskSetTags {get; set;}
    }
}