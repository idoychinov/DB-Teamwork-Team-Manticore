namespace SantasToyFactory.DataLayer
{
    using SantasToyFactory.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface ISantasToyFactorySqlContext
    {
        IDbSet<Address> Addresses { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<Delivery> Deliveries { get; set; }

        IDbSet<Child> Children { get; set; }

        IDbSet<ToyType> ToyTypes { get; set; }

        IDbSet<Town> Towns { get; set; }

        IDbSet<Toy> Toys { get; set; }

        IDbSet<Deliverer> Deliverers { get; set; }

        IDbSet<Producer> Producers { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
