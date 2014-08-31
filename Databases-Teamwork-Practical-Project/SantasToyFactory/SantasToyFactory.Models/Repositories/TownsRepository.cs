namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class TownsRepository : GenericRepository<Town>, IGenericRepository<Town>
    {
        public TownsRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}