namespace SantasToyFactory.Models
{
    using System.Collections.Generic;

    public class Toy
    {
        private ICollection<Delivery> deliveries;

        public Toy()
        {
            this.deliveries = new HashSet<Delivery>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ProducerId { get; set; }

        public int ToyTypeId { get; set; }

        public virtual ToyType ToyType { get; set; }

        public virtual ICollection<Delivery> Deliveries
        {
            get
            {
                return this.deliveries;
            }
            set
            {
                this.deliveries = value;
            }
        }
    }
}
