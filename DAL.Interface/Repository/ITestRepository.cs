using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface ITestRepository : IRepository<DalTest>
    {
        DalTest GetTestByName(string nameTest);
        IEnumerable<DalTest> GetForPage(int skipCount, int takeCount, ref int totalSize);
        void PublichingTest(DalTest test);
        IEnumerable<DalTest> GetForPageByText(string searchText);

    }
}
