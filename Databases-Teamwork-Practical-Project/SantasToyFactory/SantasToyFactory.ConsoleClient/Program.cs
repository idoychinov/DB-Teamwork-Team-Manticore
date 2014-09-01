namespace SantasToyFactory.ConsoleClient
{
    using System;


    using SantasToyFactory.DataLayer;
    using SantasToyFactory.DataOperations;

    public class Program
    {
        static void Main(string[] args)
        {
            var db = new SantasToyFactoryDatabase();

            Console.WriteLine(db.Deliverers.SearchFor(d=> d.Id == 1));
            db.SaveChanges();

            var excelFiles = ZipManipulator.ExtractFile(@"C:\Users\Stefan\Documents\GitHub\DB-Teamwork-Team-Manticore\Deliveries.zip", @"D:\Deliveries\");
            ExcelManipulator.AddExcelInfoToDatabase("Server = .; Database = SantasToyFactoryDb; Integrated Security = true", excelFiles);
        }
    }
}
//http://content.time.com/time/specials/packages/completelist/0,29569,2049243,00.html