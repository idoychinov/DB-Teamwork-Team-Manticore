namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class ToyTypesRepository : GenericRepository<ToyType>, IGenericRepository<ToyType>
    {
        public ToyTypesRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}