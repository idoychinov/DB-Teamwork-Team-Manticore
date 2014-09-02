namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Delivery
    {
        public int Id { get; set; }
                
        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public int ChildId { get; set; }
        
        public int DelivererId { get; set; }

        public int ToyId { get; set; }

        public int CountryId { get; set; }

    }
}
