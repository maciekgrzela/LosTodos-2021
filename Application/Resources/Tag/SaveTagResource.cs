using System.ComponentModel.DataAnnotations;

namespace Application.Resources.Tag
{
    public class SaveTagResource
    {
        [Required, StringLength(30, MinimumLength = 2)]
        public string Name {get; set;}
    }
}