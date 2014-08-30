using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasToyFactory.Models
{
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
