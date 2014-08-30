﻿namespace SantasToyFactory.DataLayer
{
    using System.Collections.Generic;

    public class Toy
    {
        public ICollection<Vendor> vendors;

         public Toy()
        {
            this.vendors = new HashSet<Vendor>();
        }


        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ToyType ProductTypeId { get; set; }

        public virtual ICollection<Vendor> Vendors
        {
            get
            {
                return this.vendors;
            }
            set
            {
                this.vendors = value;
            }
        }

    }
}
