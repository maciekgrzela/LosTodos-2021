using System;

namespace Application.Resources.TaskSet
{
    public class TaskForTaskSetResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public bool Checked {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public DateTime? LastChecked {get; set;}
    }
}