namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Deliverer
    {
        private ICollection<Delivery> deliveries;

        public Deliverer()
        {
            this.deliveries = new HashSet<Delivery>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
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
