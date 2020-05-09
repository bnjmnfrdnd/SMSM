using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class RequestUser
    {
        [Key]
        public int ID { get; set; }
        [Required, DisplayName("Name"), BindProperty]
        public string Name { get; set; }      
        [Required, DisplayName("Active"), BindProperty]
        public bool Active { get; set; }
    }
}
