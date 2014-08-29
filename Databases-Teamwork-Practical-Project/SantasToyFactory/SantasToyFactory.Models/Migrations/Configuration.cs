namespace SantasToyFactory.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using SantasToyFactory.DataLayer;

    public sealed class Configuration : DbMigrationsConfiguration<SantasToyFactorySqlContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SantasToyFactorySqlContext context)
        {
            if(context.Vendors.Any())
            {
                return;
            }

            var elfAlabaster = new Vendor()
                {
                    Name = "Alabaster Snowball"
                };
            context.Vendors.Add(elfAlabaster);

            var elfBushy = new Vendor()
                {
                    Name = "Bushy Evergreen"
                };
            context.Vendors.Add(elfBushy);

            var elfPepper = new Vendor()
            {
                Name = "Pepper Minstix"
            };
            context.Vendors.Add(elfPepper);

            var elfShinny = new Vendor()
            {
                Name = "Shinny Upatree"
            };
            context.Vendors.Add(elfShinny);

            var elfSugarplum = new Vendor()
            {
                Name = "Sugarplum Mary "
            };
            context.Vendors.Add(elfSugarplum);

            var santaClause = new Vendor()
            {
                Name = "Santa Clause"
            };
            context.Vendors.Add(santaClause);

            context.SaveChanges();
        }
    }
}
