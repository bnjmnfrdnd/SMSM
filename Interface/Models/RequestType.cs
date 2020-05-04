using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class RequestType
    {
        [Key]
        public int ID { get; set; }
        [Required, DisplayName("Type"), BindProperty]
        public string Type { get; set; }
        [Required, DisplayName("Enabled"), BindProperty]
        public bool Enabled { get; set; }
    }
}