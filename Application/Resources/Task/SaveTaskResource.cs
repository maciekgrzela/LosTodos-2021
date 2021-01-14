using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Task
{
    public class SaveTaskResource
    {
        [Required, StringLength(1000, MinimumLength = 3)]
        public string Name {get; set;}
        [Required]
        public bool Checked {get; set;}
        [Required]
        public Guid TaskSetId {get; set;}
    }
}