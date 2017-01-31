using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities.FullModel
{
    public class FullResultEntity
    {
        public string TestName { get; set; }

        public IEnumerable<ResultQuestionEntity> Questions { get; set; }
    }
}
