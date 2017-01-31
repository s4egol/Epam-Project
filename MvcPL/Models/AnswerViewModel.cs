using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class AnswerViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ContentAnswer { get; set; }

        [Required]
        public bool IsTrue { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}