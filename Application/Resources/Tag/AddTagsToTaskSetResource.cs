using System;
using System.Collections.Generic;

namespace Application.Resources.Tag
{
    public class AddTagsToTodoSetResource
    {
        public Guid TodoSetId {get; set;}
        public List<string> Tags {get; set;}
    }
}