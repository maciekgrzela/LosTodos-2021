using System;
using System.Collections.Generic;

namespace Application.Resources.Task
{
    public class SetForTaskResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public ICollection<TagForTaskSetResource> Tags {get; set; }
    }
}