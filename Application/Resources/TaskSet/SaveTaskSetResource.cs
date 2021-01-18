using System.ComponentModel.DataAnnotations;

namespace Application.Resources.TaskSet
{
    public class SaveTaskSetResource
    {
        [Required, StringLength(200, MinimumLength = 3)]
        public string Name {get; set;}
    }
}