namespace SantasToyFactory.DataLayer
{
    using System;
    using System.Collections.Generic;

    using SantasToyFactory.DataLayer.Repositories;
    using SantasToyFactory.Models;

    public class SantasToyFactoryDatabase : ISantasToyFactoryDatabase
    {
        private ISantasToyFactorySqlContext context;
        private IDictionary<Type, object> repositories;


        public SantasToyFactoryDatabase()
            : this(new SantasToyFactorySqlContext())
        {
        }

        public SantasToyFactoryDatabase(ISantasToyFactorySqlContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Address> Addresses
        {
            get { return (AddressesRepository)this.GetRepository<Address>(); }
        }

        public IGenericRepository<Child> Children
        {
            get { return (ChildrenRepository)this.GetRepository<Child>(); }
        }

        public IGenericRepository<Country> Countries
        {
            get { return (CountriesRepository)this.GetRepository<Country>(); }
        }

        public IGenericRepository<Deliverer> Deliverers
        {
            get { return (DeliverersRepository)this.GetRepository<Deliverer>(); }
        }

        public IGenericRepository<Delivery> Deliveries
        {
            get { return (DeliveriesRepository)this.GetRepository<Delivery>(); }
        }

        public IGenericRepository<Producer> Producers
        {
            get { return (ProducersRepository)this.GetRepository<Producer>(); }
        }

        public IGenericRepository<Town> Towns
        {
            get { return (TownsRepository)this.GetRepository<Town>(); }
        }

        public IGenericRepository<Toy> Toys
        {
            get { return (ToysRepository)this.GetRepository<Toy>(); }
        }

        public IGenericRepository<ToyType> ToyTypes
        {
            get { return (ToyTypesRepository)this.GetRepository<ToyType>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                if (typeOfModel.IsAssignableFrom(typeof(Address)))
                {
                    type = typeof(AddressesRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Child)))
                {
                    type = typeof(ChildrenRepository);
                }
                     else if (typeOfModel.IsAssignableFrom(typeof(Country)))
                {
                    type = typeof(CountriesRepository);
                }
                else if (typeOfModel.IsAssignableFrom(typeof(Deliverer)))
                {
                    type = typeof(DeliverersRepository);
                }
                 else if (typeOfModel.IsAssignableFrom(typeof(Delivery)))
                {
                    type = typeof(DeliveriesRepository);
                }
                 else if (typeOfModel.IsAssignableFrom(typeof(Producer)))
                {
                    type = typeof(ProducersRepository);
                }
                 else if (typeOfModel.IsAssignableFrom(typeof(Town)))
                {
                    type = typeof(TownsRepository);
                }
                 else if (typeOfModel.IsAssignableFrom(typeof(Toy)))
                {
                    type = typeof(ToysRepository);
                }
                 else if (typeOfModel.IsAssignableFrom(typeof(ToyType)))
                {
                    type = typeof(ToyTypesRepository);
                }

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
        
    }
}
