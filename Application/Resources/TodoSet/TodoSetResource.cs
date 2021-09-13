using System;
using System.Collections.Generic;
using Application.Resources.Todo;
using Application.Resources.User;

namespace Application.Resources.TodoSet
{
    public class TodoSetResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public OwnerResource Owner {get; set;}
        public ICollection<TodoForTodoSetResource> Todos {get; set;}
        public ICollection<TagForTodoSetResource> Tags {get; set; }
    }
}