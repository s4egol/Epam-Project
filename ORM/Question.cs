namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int QuantityPoint { get; set; }

        [Required]
        public int TestId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Result> Results { get; set; }

        public virtual Test Test { get; set; }
    }
}
