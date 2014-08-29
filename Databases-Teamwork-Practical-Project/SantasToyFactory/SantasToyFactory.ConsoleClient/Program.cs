namespace SantasToyFactory.ConsoleClient
{
    using SantasToyFactory.Models;
    using System.Data.Entity;
    using SantasToyFactory.Models.Migrations;
    using SantasToyFactory.DataLayer;

    public class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SantasToyFactorySqlContext, Configuration>());
            var db = new SantasToyFactorySqlContext();

        }
    }
}
//http://content.time.com/time/specials/packages/completelist/0,29569,2049243,00.html