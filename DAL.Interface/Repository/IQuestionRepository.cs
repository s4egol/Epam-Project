using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IQuestionRepository : IRepository<DalQuestion>
    {
        IEnumerable<DalQuestion> GetQuestionsByIdTest(int id);
        DalQuestion GetQuestionByParams(int idTest, string question);
    }
}
