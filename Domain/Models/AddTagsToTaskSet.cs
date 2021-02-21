using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class AddTagsToTaskSet
    {
        public Guid TaskSetId {get; set;}
        public List<string> Tags {get; set;}   
    }
}