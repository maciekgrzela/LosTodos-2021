using System;
using System.Collections.Generic;
using Application.Resources.User;

namespace Application.Resources.Tag
{
    public class TagResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public OwnerResource Owner {get; set;}
        public ICollection<TodoSetForTagResource> TodoSets {get; set;}
    }
}