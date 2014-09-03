namespace SantasToyFactory.SqliteDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
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

        public void Add(ToyProductionDetails entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        private DbEntityEntry AttachIfDetached(ToyProductionDetails entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.ToyProductionDetiles.Attach(entity);
            }

            return entry;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
