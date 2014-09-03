namespace SantasToyFactory.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [ForeignKey("Town")]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }
    }
}
