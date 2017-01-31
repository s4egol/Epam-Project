using BLL.Interface.Entities;
using BLL.Interface.Entities.FullModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface ITestService
    {
        void CreateTest(TestEntity test);
        void UpdateTest(TestEntity test);
        void DeleteTest(TestEntity test);
        FullTestEntity GetTestByName(string testName);
        TestEntity GetTestById(int id);
        FullTestEntity GetFullTestEntity(TestEntity test);
        IEnumerable<TestEntity> GetAll();
        IEnumerable<TestEntity> GetTestsForPage(int skipCount, int takeCount, ref int totalSize);
        IEnumerable<TestEntity> GetTestsForPageByText(string searchText);
        void PublichingTest(TestEntity test);
    }
}
