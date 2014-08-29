namespace SantasToyFactory.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Person
    {
         public ICollection<Delivery> deliveries;

         public Person()
        {
            this.deliveries = new HashSet<Delivery>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public GroupAge GroupAge { get; set; }

        public Address AddressId { get; set; }

        public Toy ToyId  { get; set; }

        public ICollection<Delivery> Deliveries
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
