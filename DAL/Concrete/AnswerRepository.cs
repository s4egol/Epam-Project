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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly EntityModel context;

        public AnswerRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalAnswer entity)
        {
            if (entity != null)
                context.Answers.Add(entity.ToOrmAnswer());
        }

        public void Delete(DalAnswer entity)
        {
            if (entity != null)
            {
                var answer = context.Answers.FirstOrDefault(x => x.Id == entity.Id);
                if (answer != null)
                    context.Answers.Remove(answer);
            }
        }

        public void DeleteAllByIdQuestion(int id)
        {
            context.Answers.RemoveRange(context.Answers.Where(x => x.QuestionId == id));
        }

        public IEnumerable<DalAnswer> GetAll()
        {
            return context.Answers.ToList().Select(x => x.ToDalAnswer());
        }

        public IEnumerable<DalAnswer> GetAllAnswersByIdQuestion(int idQuestion)
        {
            return context.Answers.Where(x => x.QuestionId == idQuestion)?.ToList().Select(x => x.ToDalAnswer());
        }

        public DalAnswer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DalAnswer entity)
        {
            throw new NotImplementedException();
        }
    }
}
