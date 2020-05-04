using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class RequestUser
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }        
    }
}
