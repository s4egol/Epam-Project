using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class TestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Test name")]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string TestName { get; set; }

        [StringLength(2000, MinimumLength = 1)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Time (sec)")]
        public int TimeSec { get; set; }

        [Required]
        public bool Published { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}