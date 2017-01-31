namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class Test
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool Published { get; set; }

        [Required]
        public int Time { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual User User { get; set; }
    }
}
