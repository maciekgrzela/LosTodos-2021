using System;

namespace Domain.Models
{
    public class ModifiedAndCreatedEntity
    {
        public DateTime Created {get; set;}
        public DateTime? LastModified {get; set;}
    }
}