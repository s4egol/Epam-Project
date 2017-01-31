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
    public class TestRepository : ITestRepository
    {
        private readonly EntityModel context;

        public TestRepository(EntityModel context)
        {
            this.context = context;
        }

        public void Create(DalTest entity)
        {
            context.Tests.Add(entity.ToOrmTest());
        }

        public void Delete(DalTest entity)
        {
            var test = context.Tests.FirstOrDefault(x => x.Id == entity.Id);
            if (test != null)
                context.Tests.Remove(test);
        }

        public IEnumerable<DalTest> GetAll()
        {
            return context.Tests.ToList().Select(x => x.ToDalTest());
        }

        public DalTest GetById(int id)
        {
            return context.Tests.FirstOrDefault(x => x.Id == id)?.ToDalTest();
        }

        public IEnumerable<DalTest> GetForPage(int skipCount, int takeCount, ref int totalSize)
        {
            //totalSize = context.Tests.Count();
            totalSize = context.Tests.Where(x => x.Published == true).Count();

            return context.Tests.Where(x => x.Published == true).OrderByDescending(x => x.Name)
                .Skip(skipCount)
                .Take(takeCount)
                .ToList()
                .Select(x => x.ToDalTest());
        }

        public IEnumerable<DalTest> GetForPageByText(string searchText)
        {
            searchText = searchText.ToUpperInvariant();
            string[] result = searchText.Split(' ', '.', '!', '?', ',');
            var findEntities = new List<Test>();
            foreach (var searchValue in result)
            {
                if (searchValue.Length >= 3)
                {
                    foreach (var entity in context.Tests.ToList())
                    {
                        if (entity.Description.ToUpperInvariant().Contains(searchValue))
                            findEntities.Add(entity);
                    }
                }
            }
            return findEntities.Where(x => x.Published == true).ToList().Distinct().OrderByDescending(x => x.Name)
                .ToList()
                .Select(x => x.ToDalTest());
        }

        public DalTest GetTestByName(string nameTest)
        {
            return context.Tests.FirstOrDefault(x => x.Name == nameTest)?.ToDalTest();
        }

        public void PublichingTest(DalTest test)
        {
            var searchTest = context.Tests.FirstOrDefault(x => x.Id == test.Id);
            if (searchTest != null)
                searchTest.Published = !searchTest.Published;
        }

        public void Update(DalTest entity)
        {
            var test = context.Tests.FirstOrDefault(x => x.Name == entity.NameTest);
            if (test != null)
            {
                test.Description = entity.Description;
                test.Time = entity.TimeSec;
            }
        }
    }
}
