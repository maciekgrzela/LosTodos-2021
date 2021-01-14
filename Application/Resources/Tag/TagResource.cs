using System;
using System.Collections.Generic;

namespace Application.Resources.Tag
{
    public class TagResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public ICollection<TaskSetForTagResource> TaskSets {get; set;}
    }
}