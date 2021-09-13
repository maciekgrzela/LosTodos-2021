using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class AddTagsToTodoSet
    {
        public Guid TodoSetId {get; set;}
        public List<string> Tags {get; set;}   
    }
}