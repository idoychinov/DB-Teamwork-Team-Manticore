namespace SantasToyFactory.ConsoleClient
{
    using SantasToyFactory.DataLayer;
    using System;
    using System.Linq;
    using SantasToyFactory.DataOperations;

    public class SantasToyFactoryConsoleClient
    {
        static void Main()
        {
            MainMenu();
        }

        private static bool CheckForEsc(out ConsoleKey pressedKey)
        {
            pressedKey = Console.ReadKey().Key;
            Console.WriteLine();
            if (pressedKey == ConsoleKey.Escape)
            {
                ConsoleUtilities.ProcessingMessage("Thank you for using Santa's Toy Factory Data System. Good bye and Ho Ho Ho!");
                return true;
            }
            return false;

        }

        private static void MainMenu()
        {
            ConsoleKey currentKey;
            ConsoleUtilities.MenuMessage("Main menu:");
            ConsoleUtilities.MenuMessage("Press ESC at any menu to Exit or any other key to proceed unless any other buttons are specified");

            var active = true;

            while (active)
            {

                // we will make it as linear flow one after another later
                ConsoleUtilities.MenuMessage("For testing purposes. Press 1 to Initialize SQL server; 2 to Initialize MongoDB 3 to Read Excel");
                if (CheckForEsc(out currentKey))
                {
                    active = false;
                    return;
                }
                switch (currentKey)
                {

                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        InitializeSQL();
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        InitializeMongo();
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        ReadExcel();
                        break;
                    default:
                        ConsoleUtilities.ErrorMessage("Wrong command. Please try again");
                        break;
                }
            }
        }

        private static void ReadExcel()
        {
            var excelFiles = ZipManipulator.ExtractFile(@"C:\Users\Stefan\Documents\GitHub\DB-Teamwork-Team-Manticore\Deliveries.zip", @"D:\Deliveries\");
            ExcelManipulator.AddExcelInfoToDatabase("Server = .; Database = SantasToyFactoryDb; Integrated Security = true", excelFiles);
        }

        private static void InitializeMongo()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        private static void InitializeSQL()
        {
            ConsoleUtilities.MenuMessage("Press 1 for SQL Server or any other key for SQL Express");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1)
            {
                SantasToyFactorySqlContext.InitializeForSqlServer();
            }
            ConsoleUtilities.ProcessingMessage("Initializing SQL database");
            try
            {
                var db = new SantasToyFactoryDatabase();
                var test = db.Deliverers.SearchFor(d => d.Id == 1).First().Name;

                ConsoleUtilities.SuccessMessage("Database initialized successfully.");
                Console.WriteLine("First deer name is: {0}", test);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ConsoleUtilities.ErrorMessage(String.Format("Error while using database: {0}", e));
            }
        }
    }
}
//http://content.time.com/time/specials/packages/completelist/0,29569,2049243,00.html