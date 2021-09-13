using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TodoSetTags : BaseEntity
    {
        public Guid TodoSetId {get; set; }
        public TodoSet TodoSet {get; set; }
        public Guid TagId {get; set; }
        public Tag Tag {get; set; }
    }
}