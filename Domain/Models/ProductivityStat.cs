using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class ProductivityStat
    {
        public int TaskCreated {get; set;}
        public int TaskChecked {get; set;}   
    }
}