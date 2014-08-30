using System.Collections.Generic;
namespace SantasToyFactory.Models
{
    public class Child
    {       
       
         public int Id { get; set; }

        public string Name { get; set; }

        public GroupAge GroupAge { get; set; }

        public Behaivior Behaviour { get; set; }

        public Address AddressId { get; set; }

        public int ToyId { get; set; }
       
    }
}
