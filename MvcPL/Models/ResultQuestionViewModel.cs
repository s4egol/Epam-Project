using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class ResultQuestionViewModel
    {
        public int Id { get; set; }

        public string QuestionContent { get; set; }

        public int QuantityPoint { get; set; }

        public bool IsTrueQuestion { get; set;}

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}