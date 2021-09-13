using System.ComponentModel.DataAnnotations;

namespace Application.Resources.TodoSet
{
    public class SaveTodoSetResource
    {
        [Required, StringLength(200, MinimumLength = 3)]
        public string Name {get; set;}
    }
}