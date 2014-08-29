namespace SantasToyFactory.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Country
    {
        public ICollection<Town> towns;

        public Country()
        {
            this.towns = new HashSet<Town>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public Continent Continent { get; set; }

        public virtual ICollection<Town> Towns
        {
            get
            {
                return this.towns;
            }
            set
            {
                this.towns = value;
            }
        }
            

    }
}
