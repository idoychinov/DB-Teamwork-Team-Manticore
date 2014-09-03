namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Delivery
    {
        public int Id { get; set; }
                
        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public int ChildId { get; set; }
        
        public int DelivererId { get; set; }

        [ForeignKey("Toy")]
        public int ToyId { get; set; }

        public virtual Toy Toy { get; set; }


        public int CountryId { get; set; }

		public virtual Country Country { get; set; }
    }
}
