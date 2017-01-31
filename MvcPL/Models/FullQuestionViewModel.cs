using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class FullQuestionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        [Display(Name = "Question")]
        public string QuestionContent { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public int QuantityPoint { get; set; }

        [Required]
        public int TestId { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}