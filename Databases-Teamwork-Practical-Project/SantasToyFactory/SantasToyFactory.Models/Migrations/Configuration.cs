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
             if (context.Producers.Any() && context.Vendors.Any())
            {
                return;
            }

            var elfAlabaster = new Producer()
                {
                    Name = "Alabaster Snowball"
                };
            context.Producers.Add(elfAlabaster);

            var elfBushy = new Producer()
                {
                    Name = "Bushy Evergreen"
                };
            context.Producers.Add(elfBushy);

            var elfPepper = new Producer()
            {
                Name = "Pepper Minstix"
            };
            context.Producers.Add(elfPepper);

            var elfShinny = new Producer()
            {
                Name = "Shinny Upatree"
            };
            context.Producers.Add(elfShinny);

            var elfSugarplum = new Producer()
            {
                Name = "Sugarplum Mary "
            };
            context.Producers.Add(elfSugarplum);

            context.SaveChanges();


            var reindeerDasher = new Vendor()
            {
                Name = "Dasher"
            };
            context.Vendors.Add(reindeerDasher);

            var reindeerDancer = new Vendor()
            {
                Name = "Dancer"
            };
            context.Vendors.Add(reindeerDancer);

            var reindeerPrancer = new Vendor()
            {
                Name = "Prancer"
            };
            context.Vendors.Add(reindeerPrancer);

            var reindeerVixen = new Vendor()
            {
                Name = "Vixen"
            };
            context.Vendors.Add(reindeerVixen);

            var reindeerComet = new Vendor()
            {
                Name = "Comet"
            };
            context.Vendors.Add(reindeerComet);

            var reindeerCupid = new Vendor()
            {
                Name = "Cupid"
            };
            context.Vendors.Add(reindeerCupid);

            var reindeerDunder = new Vendor()
            {
                Name = "Dunder"
            };
            context.Vendors.Add(reindeerDunder);

            var reindeerRudolph = new Vendor()
            {
                Name = "Rudolph"
            };
            context.Vendors.Add(reindeerRudolph);

            var santaClause = new Vendor()
            {
                Name = "Santa Clause"
            };
            context.Vendors.Add(santaClause);

            context.SaveChanges();
        }
    }
}
