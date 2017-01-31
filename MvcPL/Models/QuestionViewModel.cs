using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question title")]
        public string QuestionContent { get; set; }

        [Required]
        [Display(Name = "Quantity point")]
        public int QuantityPoint { get; set; }

        [Required]
        public int TestId { get; set; }
    }
}