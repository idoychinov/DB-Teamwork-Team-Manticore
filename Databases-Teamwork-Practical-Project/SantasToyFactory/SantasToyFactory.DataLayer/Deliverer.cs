namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Deliverer
    {
        private ICollection<Delivery> delivery;

        public Deliverer()
        {
            this.delivery = new HashSet<Delivery>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Delivery> Deliverys
        {
            get
            {
                return this.delivery;
            }
            set
            {
                this.delivery = value;
            }
        }
    }
}
