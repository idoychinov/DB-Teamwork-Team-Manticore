using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasToyFactory.DataLayer
{

    public class Delivery
    {
         public ICollection<Vendor> vendors;

         public Delivery()
        {
            this.vendors = new HashSet<Vendor>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Vendor> Vendors
        {
            get
            {
                return this.vendors;
            }
            set
            {
                this.vendors = value;
            }
        }

    }
}
