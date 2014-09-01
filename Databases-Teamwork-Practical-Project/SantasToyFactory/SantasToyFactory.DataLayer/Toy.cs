namespace SantasToyFactory.Models
{
    using System.Collections.Generic;

    public class Toy
    {
        private ICollection<Child> children;

         public Toy()
        {
            this.children = new HashSet<Child>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ToyTypeId { get; set; }

        public virtual ToyType ToyType { get; set; }

       public virtual ICollection<Child > Children
        {
            get
            {
                return this.children;
            }
            set
            {
                this.children = value;
            }
        }
    }
}
