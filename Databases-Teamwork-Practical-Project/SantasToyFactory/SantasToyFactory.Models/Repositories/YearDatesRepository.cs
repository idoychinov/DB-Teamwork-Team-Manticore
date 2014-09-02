namespace SantasToyFactory.DataLayer.Repositories
{
    using SantasToyFactory.Models;

    public class YearDatesRepository : GenericRepository<YearDate>, IGenericRepository<YearDate>
    {
        public YearDatesRepository(ISantasToyFactorySqlContext context)
            : base(context)
        {
        }
    }
}