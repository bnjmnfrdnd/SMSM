using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}