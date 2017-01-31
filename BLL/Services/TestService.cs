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
    public class TestService : ITestService
    {
        private readonly IUnitOfWork uow;

        public TestService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void CreateTest(TestEntity test)
        {
            uow.TestRepository.Create(test.ToDalTest());
            uow.Commit();
        }

        public void UpdateTest(TestEntity test)
        {
            uow.TestRepository.Update(test.ToDalTest());
            uow.Commit();
        }

        public FullTestEntity GetTestByName(string testName)
        {
            var notFullTest = uow.TestRepository.GetTestByName(testName);
            if (notFullTest != null)
            {
                var fullTest = new FullTestEntity
                {
                    Id = notFullTest.Id,
                    Name = notFullTest.NameTest,
                    Description = notFullTest.Description,
                    Published = notFullTest.Published,
                    TimeSec = notFullTest.TimeSec,
                    UserId = notFullTest.UserId,
                    Questions = uow.QuestionRepository.GetQuestionsByIdTest(notFullTest.Id)?.ToList().Select(x => x.ToBllQuestion())
                };
                return fullTest;
            }
            return null;
        }

        public FullTestEntity GetFullTestEntity(TestEntity test)
        {
            if (test == null)
                return null;

            return new FullTestEntity
            {
                Id = test.Id,
                Name = test.TestName,
                Description = test.Description,
                Published = test.Published,
                TimeSec = test.TimeSec,
                UserId = test.UserId,
                Questions = uow.QuestionRepository.GetQuestionsByIdTest(test.Id)?.ToList().Select(x => x.ToBllQuestion())
            };
        }

        public TestEntity GetTestById(int id)
        {
            return uow.TestRepository.GetById(id)?.ToBllTest();
        }

        public IEnumerable<TestEntity> GetAll()
        {
            return uow.TestRepository.GetAll().Select(x => x.ToBllTest());
        }

        public IEnumerable<TestEntity> GetTestsForPage(int skipCount, int takeCount, ref int totalSize)
        {
            return uow.TestRepository.GetForPage(skipCount, takeCount, ref totalSize).Select(x => x.ToBllTest());
        }

        public void PublichingTest(TestEntity test)
        {
            uow.TestRepository.PublichingTest(test.ToDalTest());
            uow.Commit();
        }

        public IEnumerable<TestEntity> GetTestsForPageByText(string searchText)
        {
            return uow.TestRepository.GetForPageByText(searchText).Select(x => x.ToBllTest());
        }

        public void DeleteTest(TestEntity test)
        {
            uow.ResultRepository.DeleteResultsByIdTest(test.Id);
            var allQuestions = uow.QuestionRepository.GetQuestionsByIdTest(test.Id)?.Select(x => x.ToBllQuestion()).ToList();
            foreach (var question in allQuestions)
            {
                uow.AnswerRepository.DeleteAllByIdQuestion(question.Id);
                uow.QuestionRepository.Delete(question.ToDalQuestion());
            }
            uow.TestRepository.Delete(test.ToDalTest());
            uow.Commit();
        }
    }
}
