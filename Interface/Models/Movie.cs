using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Title"), BindProperty]
        public string Title { get; set; }
        [DisplayName("Year"), BindProperty]
        public string YYYY { get; set; }
        [DisplayName("Extension"), BindProperty]
        public string Extension { get; set; }
    }
}
