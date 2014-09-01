namespace SantasToyFactory.DataLayer.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using SantasToyFactory.Models;

    public sealed class Configuration : DbMigrationsConfiguration<SantasToyFactorySqlContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SantasToyFactorySqlContext context)
        {
            if (context.Deliverers.Any())
            {
                return;
            }

            var reindeerDasher = new Deliverer()
            {
                Name = "Dasher"
            };
            context.Deliverers.Add(reindeerDasher);

            var reindeerDancer = new Deliverer()
            {
                Name = "Dancer"
            };
            context.Deliverers.Add(reindeerDancer);

            var reindeerPrancer = new Deliverer()
            {
                Name = "Prancer"
            };
            context.Deliverers.Add(reindeerPrancer);

            var reindeerVixen = new Deliverer()
            {
                Name = "Vixen"
            };
            context.Deliverers.Add(reindeerVixen);

            var reindeerComet = new Deliverer()
            {
                Name = "Comet"
            };
            context.Deliverers.Add(reindeerComet);

            var reindeerCupid = new Deliverer()
            {
                Name = "Cupid"
            };
            context.Deliverers.Add(reindeerCupid);

            var reindeerDunder = new Deliverer()
            {
                Name = "Dunder"
            };
            context.Deliverers.Add(reindeerDunder);

            var reindeerRudolph = new Deliverer()
            {
                Name = "Rudolph"
            };
            context.Deliverers.Add(reindeerRudolph);

            var santaClause = new Deliverer()
            {
                Name = "Santa Clause"
            };
            context.Deliverers.Add(santaClause);

            context.SaveChanges();
        }
    }
}
