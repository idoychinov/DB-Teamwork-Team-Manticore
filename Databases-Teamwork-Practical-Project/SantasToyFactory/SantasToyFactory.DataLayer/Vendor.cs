namespace SantasToyFactory.DataLayer
{
    using System.Collections.Generic;

    public class Vendor
    {
        public ICollection<Continent> locations;

        public Vendor()
        {
            this.locations = new HashSet<Continent>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

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
