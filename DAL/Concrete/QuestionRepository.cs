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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EntityModel context;

        public QuestionRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalQuestion entity)
        {
            if (entity != null)
                context.Questions.Add(entity.ToOrmQuestion());
        }

        public void Delete(DalQuestion entity)
        {
            if (entity != null)
            {
                var question = context.Questions.FirstOrDefault(x => x.Id == entity.Id);
                if (question != null)
                    context.Questions.Remove(question);
            }
        }

        public IEnumerable<DalQuestion> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalQuestion GetById(int id)
        {
            return context.Questions.FirstOrDefault(x => x.Id == id)?.ToDalQuestion();
        }

        public DalQuestion GetQuestionByParams(int idTest, string question)
        {
            return context.Questions.FirstOrDefault(x => x.TestId == idTest && x.Content == question)?.ToDalQuestion();
        }

        public IEnumerable<DalQuestion> GetQuestionsByIdTest(int id)
        {
            return context.Questions.Where(x => x.TestId == id).ToList().Select(e => e.ToDalQuestion());
        }

        public void Update(DalQuestion entity)
        {
            var question = context.Questions.FirstOrDefault(x => x.Id == entity.Id);
            if (question != null)
                question.QuantityPoint = entity.QuantityPoint;
        }
    }
}
