using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasToyFactory.DataLayer
{
    public class Toy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType ProductTypeId { get; set; }

        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
