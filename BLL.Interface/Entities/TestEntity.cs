using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public int TimeSec { get; set; }
        public bool Published { get; set; }
        public int UserId { get; set; }
    }
}
