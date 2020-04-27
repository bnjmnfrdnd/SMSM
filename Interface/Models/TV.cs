using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class TV
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Year { get; set; }
    }
}