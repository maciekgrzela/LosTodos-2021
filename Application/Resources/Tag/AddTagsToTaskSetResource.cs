using System;
using System.Collections.Generic;

namespace Application.Resources.Tag
{
    public class AddTagsToTaskSetResource
    {
        public Guid TaskSetId {get; set;}
        public List<string> Tags {get; set;}
    }
}