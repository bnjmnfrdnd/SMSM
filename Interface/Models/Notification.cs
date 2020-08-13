using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string RequestID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public bool Acknowledged { get; set; }
    }
}