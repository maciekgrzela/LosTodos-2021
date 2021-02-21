using System;
using System.Collections.Generic;
using Application.Resources.Task;
using Application.Resources.User;

namespace Application.Resources.TaskSet
{
    public class TaskSetResource
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
        public OwnerResource Owner {get; set;}
        public ICollection<TaskForTaskSetResource> Tasks {get; set;}
        public ICollection<TagForTaskSetResource> Tags {get; set; }
    }
}