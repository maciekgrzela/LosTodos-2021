using System;

namespace Domain.Models
{
    public class TaskSetTags
    {
        public Guid TaskSetId {get; set; }
        public TaskSet TaskSet {get; set; }
        public Guid TagId {get; set; }
        public Tag Tag {get; set; }
    }
}