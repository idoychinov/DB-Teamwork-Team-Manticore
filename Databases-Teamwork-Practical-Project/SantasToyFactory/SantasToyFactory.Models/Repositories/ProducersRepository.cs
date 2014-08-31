namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class ProducersRepository : GenericRepository<Producer>, IGenericRepository<Producer>
    {
        public ProducersRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}