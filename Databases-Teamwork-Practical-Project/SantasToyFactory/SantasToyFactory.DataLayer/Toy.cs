namespace SantasToyFactory.Models
{
    using System.Collections.Generic;

    public class Toy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ToyTypeId { get; set; }

        public int ProducerId { get; set; }

        public virtual ToyType ToyType { get; set; }

    }
}
