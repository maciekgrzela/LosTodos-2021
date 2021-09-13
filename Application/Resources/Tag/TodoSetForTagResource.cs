using System;

namespace Application.Resources.Tag
{
    public class TodoSetForTagResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
    }
}