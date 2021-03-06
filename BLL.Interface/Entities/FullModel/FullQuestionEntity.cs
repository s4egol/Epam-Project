﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities.FullModel
{
    public class FullQuestionEntity
    {
        public int Id { get; set; }

        public string QuestionContent { get; set; }

        public int QuantityPoint { get; set; }

        public int TestId { get; set; }

        public IEnumerable<AnswerEntity> Answers { get; set; }
    }
}
