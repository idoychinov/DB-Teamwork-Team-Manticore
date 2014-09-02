namespace SantasToyFactory.DataLayer
{
    using SantasToyFactory.Models;
    using SantasToyFactory.DataLayer.Repositories;

    public interface ISantasToyFactoryDatabase
    {
        IGenericRepository<Address> Addresses { get; }

        IGenericRepository<Child> Children { get; }

        IGenericRepository<Country> Countries { get; }

        IGenericRepository<Deliverer> Deliverers { get; }

        IGenericRepository<Delivery> Deliveries { get; }

        IGenericRepository<Producer> Producers { get; }

        IGenericRepository<Town> Towns { get; }

        IGenericRepository<ToyType> ToyTypes { get; }

        IGenericRepository<YearDate> Years { get; }

        void SaveChanges();

    }
}
