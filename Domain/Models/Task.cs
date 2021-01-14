using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Task
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(1000, MinimumLength = 3)]
        public string Name {get; set;}
        [Required]
        public bool Checked {get; set;}
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created {get; set;} = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModified {get; set;}
        public DateTime? LastChecked {get; set;}
        public TaskSet TaskSet {get; set;}
        public Guid TaskSetId {get; set;}
    }
}