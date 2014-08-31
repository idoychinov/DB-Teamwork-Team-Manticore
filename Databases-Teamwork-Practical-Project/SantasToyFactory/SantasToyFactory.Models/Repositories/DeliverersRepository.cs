namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class DeliverersRepository : GenericRepository<Deliverer>, IGenericRepository<Deliverer>
    {
        public DeliverersRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}