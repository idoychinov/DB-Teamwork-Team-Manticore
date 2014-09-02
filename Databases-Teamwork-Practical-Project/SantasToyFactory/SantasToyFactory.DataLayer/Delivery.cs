namespace SantasToyFactory.Models
{
    using System;
    using System.Collections.Generic;

    public class Delivery
    {
        private ICollection<Child> children;

        public Delivery()
        {
            this.children = new HashSet<Child>();
        }

        public int Id { get; set; }

        public int YearId { get; set; }

        public int DelivererId { get; set; }

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
