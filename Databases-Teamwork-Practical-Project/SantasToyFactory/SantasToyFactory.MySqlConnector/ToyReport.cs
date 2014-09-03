using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasToyFactory.MySqlConnector
{
    public class ToyReport
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Producer { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
