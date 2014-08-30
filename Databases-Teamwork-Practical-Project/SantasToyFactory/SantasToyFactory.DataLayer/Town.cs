namespace SantasToyFactory.Models
{
    using System.Collections.Generic;

    public  class Town
    {
       public ICollection<Address> addresses;

         public Town()
        {
            this.addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PostCode { get; set; }

        public virtual ICollection<Address> Addresses
        {
            get
            {
                return this.addresses;
            }
            set
            {
                this.addresses = value;
            }
        }
    }
}
