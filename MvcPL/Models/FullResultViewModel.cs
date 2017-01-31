using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class FullResultViewModel
    {
        public string TestName { get; set; }

        public IEnumerable<ResultQuestionViewModel> Questions { get; set; }
    }
}