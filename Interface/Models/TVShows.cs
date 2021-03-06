﻿using System.ComponentModel.DataAnnotations;

namespace Interface.Models
{
    public class TVShows
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Year { get; set; }
    }
}