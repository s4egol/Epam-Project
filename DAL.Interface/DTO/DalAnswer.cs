using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalAnswer
    {
        public int Id { get; set; }
        public string ContentAnswer { get; set; }
        public bool IsTrue { get; set; }
        public int QuestionId { get; set; }
    }
}
