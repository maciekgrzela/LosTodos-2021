using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Todo : BaseEntity
    {
        [Key]
        public Guid Id {get; set;}
        [Required, StringLength(1000, MinimumLength = 3)]
        public string Name {get; set;}
        [Required]
        public bool Checked {get; set;}
        public DateTime? LastChecked {get; set;}
        public TodoSet TodoSet {get; set;}
        public Guid TodoSetId {get; set;}
    }
}