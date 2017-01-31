using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities.FullModel
{
    public class FullTestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeSec { get; set; }
        public bool Published { get; set; }
        public int UserId { get; set; }
        public IEnumerable<QuestionEntity> Questions { get; set; }
    }
}
