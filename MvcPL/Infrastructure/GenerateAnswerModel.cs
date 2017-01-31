using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Infrastructure
{
    public static class GenerateAnswerModel
    {
        public static void ToAnswerModel(this List<AnswerViewModel> answer, int[] isTrueAnswer, string[] answers)
        {
            if (isTrueAnswer == null || answer == null)
                throw new NullReferenceException();

            for (int i = 0; i < answers.Length; i++)
            {
                if (isTrueAnswer.Contains(i))
                    answer.Add(new AnswerViewModel { ContentAnswer = answers[i], IsTrue = true });
                else
                    answer.Add(new AnswerViewModel { ContentAnswer = answers[i], IsTrue = false });
            }
        }

        public static void ToAnswersModel(this List<AnswerViewModel> answer, int[] isTrueAnswer, string[] answers)
        {
            if (isTrueAnswer == null || answer == null)
                throw new NullReferenceException();

            for (int i = 0; i < answers.Length; i++)
            {
                if (isTrueAnswer[i] != 0)
                    answer.Add(new AnswerViewModel { ContentAnswer = answers[i], IsTrue = true });
                else
                    answer.Add(new AnswerViewModel { ContentAnswer = answers[i], IsTrue = false });
            }
        }
    }
}