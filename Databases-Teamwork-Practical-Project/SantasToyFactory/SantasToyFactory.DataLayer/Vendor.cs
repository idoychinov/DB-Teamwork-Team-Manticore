namespace SantasToyFactory.DataLayer
{
    using System;
using System.Collections.Generic;

    public class Vendor
    {
        private ICollection<Continent> locations;
        private ICollection<Delivery> delivery;

        public Vendor()
        {
            this.locations = new HashSet<Continent>();
            this.delivery = new HashSet<Delivery>();
        }

        //public Vendor(string name)
        //    : this()
        //{
        //    this.Name = name;
        //}

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

        public virtual ICollection<Continent> Locations
        {
            get
            {
                return this.locations;
            }
            set
            {
                this.locations = value;
            }
        }


    }
}
