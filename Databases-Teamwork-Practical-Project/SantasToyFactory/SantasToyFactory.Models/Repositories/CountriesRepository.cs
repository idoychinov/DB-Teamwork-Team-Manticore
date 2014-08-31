namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class CountriesRepository : GenericRepository<Country>, IGenericRepository<Country>
    {
        public CountriesRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}