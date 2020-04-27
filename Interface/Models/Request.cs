using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Title"), BindProperty]
        public string Title { get; set; }
        [DisplayName("Year"), BindProperty]
        public string Year { get; set; }
        [DisplayName("Type"), BindProperty]
        public string Type { get; set; }
        public DateTime RequestedDate { get; set; }
        [DisplayName("Name"), BindProperty]
        public string RequestedBy { get; set; }
        [DisplayName("Complete"), BindProperty]
        public bool IsComplete { get; set; }
    }
}
