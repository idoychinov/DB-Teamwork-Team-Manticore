namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class AddressesRepository :GenericRepository<Address>, IGenericRepository<Address>
    {
        public AddressesRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}
