namespace SantasToyFactory.SqliteDb
{
    using System;
    using System.Data.SQLite;
    using System.Linq;
    using System.Data.Entity;
    using SantasToyFactory.Models;

    public class SantasToyFactorySqliteContext : DbContext
    {

        public SantasToyFactorySqliteContext()
            : base(new SQLiteConnection()
            {
                ConnectionString =
                    new SQLiteConnectionStringBuilder() { DataSource = "sqliteDB.sqlite", ForeignKeys = true }
                    .ConnectionString
            }, true)
        {
            Initialize();
        }

        public virtual IDbSet<ToyProductionDetails> ToyProductionDetiles { get; set; }

        // EF6 does not support table creation for SQLite! It throws errors that table do not exist when trying to access it trough repository 
        // So this is the initialization of the database with plain old SQL
        //http://stackoverflow.com/questions/1836173/entity-framework-store-update-insert-or-delete-statement-affected-an-unexpec
        private void Initialize()
        {

            this.Database.ExecuteSqlCommand("CREATE TABLE IF NOT EXISTS ToyProductionDetails(Id INTEGER PRIMARY KEY, Quantity INTEGER, CommissionPercent DOUBLE)");
            if (this.ToyProductionDetiles.Count() == 0)
            {
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (1,100,25)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (2,34,15)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (3,90,12.5)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (4,12,40.32)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (5,200,20)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (6,154,13)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (7,85,22.4)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (8,42,21)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (9,44,32)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (10,64,11.4)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (11,45,34)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (12,66,32)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (13,18,45)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (14,18,22)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (15,100,75)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (16,34,26.11)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (17,44,44)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (18,112,15)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (19,523,5)");
                this.Database.ExecuteSqlCommand("INSERT INTO ToyProductionDetails(Id, Quantity, CommissionPercent) VALUES (20,64,8.3)");

                this.SaveChanges();
            }
        }
    }
}
