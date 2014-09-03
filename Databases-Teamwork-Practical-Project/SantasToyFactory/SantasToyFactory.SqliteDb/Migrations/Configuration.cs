namespace SantasToyFactory.SqliteDb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SantasToyFactory.SqliteDb.SantasToyFactorySqliteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
           // SetSqlGenerator("System.Data.SqlClient", new NonSystemTableSqlGenerator());
        }

        protected override void Seed(SantasToyFactorySqliteContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.ToyProductionDetiles.Add(new Models.ToyProductionDetails() { Id = 1, Quantity = 45, CommissionPercent = 25 });
            context.SaveChanges();
        }

        //public class NonSystemTableSqlGenerator : SqlServerMigrationSqlGenerator
        //{
        //    protected void GenerateMakeSystemTable(
        //        CreateTableOperation createTableOperation)
        //    {
        //    }
        //}
    }
}
