namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class ToysRepository : GenericRepository<Toy>, IGenericRepository<Toy>
    {
        public ToysRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}