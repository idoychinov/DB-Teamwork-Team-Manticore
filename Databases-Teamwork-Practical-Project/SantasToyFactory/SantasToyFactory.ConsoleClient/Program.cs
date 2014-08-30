﻿namespace SantasToyFactory.ConsoleClient
{
    using SantasToyFactory.Models;
    using System.Data.Entity;
    using SantasToyFactory.Models.Migrations;
    using SantasToyFactory.DataLayer;
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SantasToyFactorySqlContext, Configuration>());
            var db = new SantasToyFactorySqlContext();
          //  db.Database.ExecuteSqlCommand("DELETE FROM Producers");
          //  db.Database.ExecuteSqlCommand("DELETE FROM Vendors");

            //Console.WriteLine(db.Vendors.Find(1));
            db.SaveChanges();
        }
    }
}
//http://content.time.com/time/specials/packages/completelist/0,29569,2049243,00.html