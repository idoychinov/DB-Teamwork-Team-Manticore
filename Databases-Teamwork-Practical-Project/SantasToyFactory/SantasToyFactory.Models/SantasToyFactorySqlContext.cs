﻿using SantasToyFactory.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantasToyFactory.Models
{
    public class SantasToyFactorySqlContext : DbContext
    {
        public SantasToyFactorySqlContext()
            : base("SantasToyFactoryDB")
        {

        }
        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Delivery> Deliveries { get; set; }

        public IDbSet<Person> Persons { get; set; }

        public IDbSet<ProductType> ProductTypes { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Toy> Toys { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }
    }
}