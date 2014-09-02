namespace SantasToyFactory.Models
{
    public class Child
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GroupAge GroupAge { get; set; }

        public Behaivior Behaviour { get; set; }

        public int AddressId { get; set; }

        public virtual Address Adresss { get; set; }

        public int ToyId { get; set; }

    }
}
