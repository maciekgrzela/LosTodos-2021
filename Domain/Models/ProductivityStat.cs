using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [NotMapped]
    public class ProductivityStat
    {
        public int TodoCreated {get; set;}
        public int TodoChecked {get; set;}   
    }
}