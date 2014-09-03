namespace SantasToyFactory.SqliteDb
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using SantasToyFactory.Models;
    

    public class SantasToyFactorySqliteContext : DbContext
    {
        
        public SantasToyFactorySqliteContext()
        {
            Database.SetInitializer<SantasToyFactorySqliteContext>(null);
        }

        public IDbSet<ToyProductionDetails> ToyProductionDetiles { get; set; }
    }
}
