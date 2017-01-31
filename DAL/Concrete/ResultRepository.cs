using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class ResultRepository : IResultRepository
    {
        private readonly EntityModel context;

        public ResultRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalResult result)
        {
            if (context.Results.FirstOrDefault(x => x.PassedUserId == result.UserId && x.QuestionId == result.QuestionId) == null)
                context.Results.Add(result.ToOrmResult());
        }

        public void DeleteResultsByIdTest(int idTest)
        {
            var questions = context.Questions.Where(x => x.TestId == idTest).ToList();
            if (questions != null)
            {
                foreach (var question in questions)
                {
                    context.Results.Where(x => x.QuestionId == question.Id).ToList().ForEach(x => context.Results.Remove(x));
                }
            }
        }

        public IEnumerable<DalResult> GetResultOfTest(int userId, int testId)
        {
            var resultUser = context.Results.Where(x => x.PassedUserId == userId)?.ToList();
            var allQuestionTest = context.Questions.Where(x => x.TestId == testId)?.ToList();
            if (resultUser == null || allQuestionTest == null)
                return null;

            List<DalResult> returnListResult = new List<DalResult>();
            foreach(var result in resultUser)
            {
                foreach(var question in allQuestionTest)
                {
                    if (result.QuestionId == question.Id)
                        returnListResult.Add(result.ToDalResult());
                }
            }
            return returnListResult;
        }

        public DalResult GetRusultByIdQuestion(int idQuestion, string nickname)
        {
            return context.Results.FirstOrDefault(x => x.QuestionId == idQuestion && x.PassedUserId == context.Users.FirstOrDefault(y => y.Nickname == nickname).Id)?.ToDalResult();           
        }
    }
}
