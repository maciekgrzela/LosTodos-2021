using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Todo
{
    public class SaveTodoResource
    {
        [Required, StringLength(1000, MinimumLength = 3)]
        public string Name {get; set;}
        [Required]
        public bool Checked {get; set;}
        [Required]
        public Guid TodoSetId {get; set;}
    }
}