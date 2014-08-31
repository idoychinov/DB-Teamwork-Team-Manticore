namespace SantasToyFactory.DataLayer
{
    using System.Data.Entity;

    using SantasToyFactory.DataLayer.Migrations;
    using SantasToyFactory.Models;

    public class SantasToyFactorySqlContext : DbContext, ISantasToyFactorySqlContext
    {
        public SantasToyFactorySqlContext()
            : base("SantasToyFactoryDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SantasToyFactorySqlContext, Configuration>());
        }
        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Delivery> Deliveries { get; set; }

        public IDbSet<Child> Children { get; set; }

        public IDbSet<ToyType> ToyTypes { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Toy> Toys { get; set; }

        public IDbSet<Deliverer> Deliverers { get; set; }

        public IDbSet<Producer> Producers { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
