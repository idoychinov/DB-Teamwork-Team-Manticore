namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Deliverer
    {
        private ICollection<Delivery> deliveries;
        private ICollection<Child> children;

        public Deliverer()
        {
            this.deliveries = new HashSet<Delivery>();
            this.children = new HashSet<Child>();
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

        public virtual ICollection<Child> Children
        {
            get
            {
                return this.children;
            }

            set
            {
                this.children = value;
            }
        }
    }
}
