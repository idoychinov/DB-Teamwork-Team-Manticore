using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantasToyFactory.DataLayer
{
   public  class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PostCode { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

    }
}
