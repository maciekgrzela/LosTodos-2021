using System;

namespace Application.Resources.Todo
{
    public class TagForTodoSetResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
    }
}