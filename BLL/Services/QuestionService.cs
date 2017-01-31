using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using DAL.Interface.Repository;
using BLL.Mappers;
using BLL.Interface.Entities.FullModel;

namespace BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork uow;

        public QuestionService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateQuestion(FullQuestionEntity questionEntity)
        {
            var simpleQuestion = new QuestionEntity
            {
                Id = questionEntity.Id,
                QuestionContent = questionEntity.QuestionContent,
                QuantityPoint = questionEntity.QuantityPoint,
                TestId = questionEntity.TestId
            };
            uow.QuestionRepository.Create(simpleQuestion.ToDalQuestion());
            uow.Commit();
            var listAnswers = questionEntity.Answers.ToList();
            var currentQuestion = uow.QuestionRepository.GetQuestionByParams(questionEntity.TestId, questionEntity.QuestionContent)?.ToBllQuestion();
            if (currentQuestion != null)
                listAnswers.ForEach(x => { x.QuestionId = currentQuestion.Id; uow.AnswerRepository.Create(x.ToDalAnswer()); });
            uow.Commit();
        }

        public void DeleteQuestion(FullQuestionEntity questionEntity)
        {
            questionEntity.Answers.ToList().ForEach(x => uow.AnswerRepository.Delete(x.ToDalAnswer()));
            var question = new QuestionEntity
            {
                Id = questionEntity.Id,
                QuestionContent = questionEntity.QuestionContent,
                QuantityPoint = questionEntity.QuantityPoint,
                TestId = questionEntity.TestId
            };
            uow.QuestionRepository.Delete(question.ToDalQuestion());
            uow.Commit();
        }

        public FullQuestionEntity GetFullQuestion(QuestionEntity questionEntity)
        {
            if (questionEntity == null)
                return null;

            return new FullQuestionEntity
            {
                Id = questionEntity.Id,
                QuestionContent = questionEntity.QuestionContent,
                QuantityPoint = questionEntity.QuantityPoint,
                TestId = questionEntity.TestId,
                Answers = uow.AnswerRepository.GetAll().Where(x => x.QuestionId == questionEntity.Id).Select(x => x.ToBllAnswer())
            };
        }

        public QuestionEntity GetQuestionById(int id)
        {
            return uow.QuestionRepository.GetById(id)?.ToBllQuestion();
        }

        public QuestionEntity GetQuestionByParams(int idTest, string question)
        {
            return uow.QuestionRepository.GetQuestionByParams(idTest, question)?.ToBllQuestion();
        }

        public IEnumerable<QuestionEntity> GetQuestionsByIdTest(int id)
        {
            return uow.QuestionRepository.GetQuestionsByIdTest(id)?.ToList().Select(e => e.ToBllQuestion());
        }

        public FullQuestionEntity MoveToNextQuestion(int idTest, string nicknameUser)
        {
            var allQuestionsTest = uow.QuestionRepository.GetQuestionsByIdTest(idTest).Select(x => x.ToBllQuestion());
            var user = uow.UserRepository.GetUserByNickname(nicknameUser)?.ToBllUser();
            var currentResult = uow.ResultRepository.GetResultOfTest(user.Id, idTest).Select(x => x.ToBllResult());
            if (currentResult.Count() == 0)
            {
                var simpleQuestion =  allQuestionsTest.OrderBy(x => x.Id).Skip(0).Take(1).ToList();
                return GetFullQuestion(simpleQuestion[0]);
            }
            if (currentResult.Count() < allQuestionsTest.Count())
            {
                var simpleQuestion = allQuestionsTest.OrderBy(x => x.Id).Skip(currentResult.Count()).Take(1).ToList();
                return GetFullQuestion(simpleQuestion[0]);
            }
            return null;
        }

        public void UpdateQuestion(FullQuestionEntity questionEntity)
        {
            if (questionEntity != null)
            {
                var simpleQuestion = new QuestionEntity
                {
                    Id = questionEntity.Id,
                    QuestionContent = questionEntity.QuestionContent,
                    QuantityPoint = questionEntity.QuantityPoint,
                    TestId = questionEntity.TestId
                };
                uow.QuestionRepository.Update(simpleQuestion.ToDalQuestion());
                uow.AnswerRepository.DeleteAllByIdQuestion(questionEntity.Id);
                questionEntity.Answers.ToList().ForEach(x => uow.AnswerRepository.Create(x.ToDalAnswer()));
                uow.Commit();
            }
        }
    }
}
