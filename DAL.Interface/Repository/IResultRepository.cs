using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IResultRepository
    {
        IEnumerable<DalResult> GetResultOfTest(int userId, int testId);
        void Create(DalResult result);
        DalResult GetRusultByIdQuestion(int idQuestion, string nickname);
        void DeleteResultsByIdTest(int idTest);
    }
}
