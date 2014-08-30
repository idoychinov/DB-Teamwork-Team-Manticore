namespace SantasToyFactory.Models
{
    using System.Collections.Generic;

    public class Toy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ToyType ProductTypeId { get; set; }

        public Deliverer VendorId { get; set; }

    }
}
