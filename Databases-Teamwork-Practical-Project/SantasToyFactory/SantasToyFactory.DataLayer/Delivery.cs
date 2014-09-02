namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Delivery
    {
        public int Id { get; set; }

        public int YearId { get; set; }

        public int DelivererId { get; set; }

        [ForeignKey("Toy")]
        public int ToyId { get; set; }

        public virtual Toy Toy { get; set; }
    }
}
