using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Task : ModifiedAndCreatedEntity
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(1000, MinimumLength = 3)]
        public string Name {get; set;}
        [Required]
        public bool Checked {get; set;}
        public DateTime? LastChecked {get; set;}
        public TaskSet TaskSet {get; set;}
        public Guid TaskSetId {get; set;}
    }
}