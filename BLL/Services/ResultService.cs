using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities.FullModel;
using DAL.Interface.Repository;
using BLL.Interface.Entities;
using BLL.Mappers;
using DAL.Interface.DTO;

namespace BLL.Services
{
    public class ResultService : IResultService
    {
        private readonly IUnitOfWork uow;

        public ResultService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public FullResultEntity GetResultByIdTest(int idTest, string nicknameUser)
        {
            var test = uow.TestRepository.GetById(idTest)?.ToBllTest();
            if (test == null)
                return null;

            if (uow.QuestionRepository.GetQuestionsByIdTest(idTest).Count() ==
                uow.ResultRepository.GetResultOfTest(uow.UserRepository.GetUserByNickname(nicknameUser).Id, idTest).Count())
            {
                var allQuestion = uow.QuestionRepository.GetQuestionsByIdTest(idTest)?
                    .OrderBy(x => x.Id).Select(x => x.ToBllQuestion()).ToList();
                var listResultQuestion = new List<ResultQuestionEntity>();
                foreach(var question in allQuestion)
                {
                    listResultQuestion.Add(new ResultQuestionEntity
                    {
                        Id = question.Id,
                        QuestionContent = question.QuestionContent,
                        QuantityPoint = question.QuantityPoint,
                        IsTrueQuestion = uow.ResultRepository.GetRusultByIdQuestion(question.Id, nicknameUser).IsTrueAnswer,
                        Answers = uow.AnswerRepository.GetAll().Where(x => x.QuestionId == question.Id).Select(x => x.ToBllAnswer())
                    });
                }

                return new FullResultEntity
                {
                    TestName = test.TestName,
                    Questions = listResultQuestion
                };
            }
            return null;
        }

        public void SaveResult(FullQuestionEntity question, string nickname)
        {
            var user = uow.UserRepository.GetUserByNickname(nickname)?.ToBllUser();
            var answers = uow.AnswerRepository.GetAllAnswersByIdQuestion(question.Id)?.ToList().Select(x => x.ToBllAnswer()).ToList();

            bool trueAnswer = true;
            int i = 0;
            foreach (var requestAnswer in question.Answers)
            {
                if (requestAnswer.IsTrue != answers[i].IsTrue)
                    trueAnswer = false;
                i++;
            }

            var result = new ResultEntity
            {
                QuestionId = question.Id,
                UserId = user.Id,
                PassedTime = DateTime.Now,
                IsTrueAnswer = trueAnswer
            };

            uow.ResultRepository.Create(result.ToDalResult());
            uow.Commit();
        }
    }
}
