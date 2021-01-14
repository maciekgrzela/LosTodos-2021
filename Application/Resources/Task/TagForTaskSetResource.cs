using System;

namespace Application.Resources.Task
{
    public class TagForTaskSetResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
    }
}