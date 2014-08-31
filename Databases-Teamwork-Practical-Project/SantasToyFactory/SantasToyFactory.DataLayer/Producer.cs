namespace SantasToyFactory.Models
{
    using System.Collections.Generic;

    public class Producer
    {
        private ICollection<Toy> toys;

        public Producer ()
        {
            this.toys = new HashSet<Toy>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Toy> Toys
        {
            get
            {
                return this.toys;
            }
            set
            {
                this.toys = value;
            }
        }
    }
}
