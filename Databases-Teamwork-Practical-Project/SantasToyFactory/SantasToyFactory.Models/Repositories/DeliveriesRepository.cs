namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class DeliveriesRepository : GenericRepository<Delivery>, IGenericRepository<Delivery>
    {
        public DeliveriesRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}