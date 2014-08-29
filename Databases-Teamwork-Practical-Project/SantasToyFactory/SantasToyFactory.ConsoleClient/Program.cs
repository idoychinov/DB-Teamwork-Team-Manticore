namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SantasToyFactory.DataLayer;
    using SantasToyFactory.Models;
    using System.Data.Entity;
    using SantasToyFactory.Models.Migrations;

    public class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SantasToyFactorySqlContext, Configuration>());
            var db = new SantasToyFactorySqlContext();
            
            var toy = new Toy();
            db.Toys.Add(toy);
            db.SaveChanges();
        }
    }
}
