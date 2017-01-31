namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Answer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string ContentAnswer { get; set; }

        [Required]
        public bool IsTrue { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
