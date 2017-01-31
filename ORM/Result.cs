using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public partial class Result
    {
        [Required]
        public int Id { get; set; }

        public int PassedUserId { get; set; }

        public int QuestionId { get; set; }

        [Required]
        public DateTime PassedTime { get; set; }

        [Required]
        public bool IsTrueAnswer { get; set; }

        public virtual User User { get; set; }

        public virtual Question Question { get; set; }
    }
}
