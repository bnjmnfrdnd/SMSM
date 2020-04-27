using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class AudioBook
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Title"), BindProperty, Required]
        public string Title { get; set; }
        [DisplayName("Year"), BindProperty]
        public string YYYY { get; set; }
        [DisplayName("Author"), BindProperty]
        public string Author { get; set; }
    }
}
