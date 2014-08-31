namespace SantasToyFactory.ConsoleClient
{
    using SantasToyFactory.DataLayer;
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            var db = new SantasToyFactoryDatabase();

            Console.WriteLine(db.Deliverers.SearchFor(d=> d.Id == 1));
            db.SaveChanges();
        }
    }
}
//http://content.time.com/time/specials/packages/completelist/0,29569,2049243,00.html