namespace SantasToyFactory.SqliteDb
{
    using System;
    using System.Linq;

    using SantasToyFactory.Models;

    public class ToyProductionDetailsRepository
    {
        private readonly SantasToyFactorySqliteContext context;

        public ToyProductionDetailsRepository(SantasToyFactorySqliteContext context)
        {
            this.context = context;
        }

        public IQueryable<ToyProductionDetails> GetToyProductionDetails()
        {
            return context.ToyProductionDetiles.AsQueryable<ToyProductionDetails>();
        }
    }
}
