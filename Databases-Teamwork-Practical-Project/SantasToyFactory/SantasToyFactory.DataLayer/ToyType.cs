namespace SantasToyFactory.Models
{
    public class ToyType
    {
        public int Id { get; set; }

        public virtual GroupAge GroupAge { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
