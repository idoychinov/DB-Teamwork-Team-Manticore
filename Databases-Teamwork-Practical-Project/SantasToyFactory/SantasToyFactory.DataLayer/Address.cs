namespace SantasToyFactory.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }
    }
}
