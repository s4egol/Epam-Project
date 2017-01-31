using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalQuestion
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public int QuantityPoint { get; set; }

        public int TestId { get; set; }
    }
}

