namespace SantasToyFactory.Models
{
    using System.Collections.Generic;
    public class Child        
    {
    //    private ICollection<Delivery> deliveries;
    //    public Child()
    //    {
    //        this.deliveries = new HashSet<Delivery>();
    //    }

        public int Id { get; set; }

        public string Name { get; set; }

        public GroupAge GroupAge { get; set; }

        public Behaivior Behaviour { get; set; }

        public int AddressId { get; set; }

        public virtual Address Adresss { get; set; }
        
    }
}
