using System;
using System.Collections.Generic;

namespace Application.Resources.Todo
{
    public class SetForTodoResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public ICollection<TagForTodoSetResource> Tags {get; set; }
    }
}