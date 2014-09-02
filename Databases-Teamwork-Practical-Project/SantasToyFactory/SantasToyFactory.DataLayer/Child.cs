namespace SantasToyFactory.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Child
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GroupAge GroupAge { get; set; }

        public virtual Behavior Behavior { get; set; }

        public int AddressId { get; set; }

        public virtual Address Adresss { get; set; }

        [ForeignKey("Deliverer")]
        public int DelivererId { get; set; }

        public Deliverer Deliverer { get; set; }
    }
}
