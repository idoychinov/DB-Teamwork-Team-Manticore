namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class ChildrenRepository : GenericRepository<Child>, IGenericRepository<Child>
    {
        public ChildrenRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}