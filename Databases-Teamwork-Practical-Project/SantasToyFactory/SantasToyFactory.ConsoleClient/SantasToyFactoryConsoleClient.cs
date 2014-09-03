﻿namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Data.SQLite.EF6;

    using SantasToyFactory.DataLayer;
    using SantasToyFactory.DataOperations;
    using SantasToyFactory.MySqlConnector;
    using SantasToyFactory.SqliteDb;
    using Telerik.OpenAccess.Exceptions;

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
                ConsoleUtilities.MenuMessage("For testing purposes.\n        Press\n  1 to Initialize MongoDB;\n  2 to Migrate MongoDB to SQL;\n  3 to Read Excel;\n  4 to test SQL standalone initialization; " +
                    "\n  5 to clear data from MongoDb \n  6 to create Json reports and transfer them to MySql \n  7 to Create XML report \n  8 to Generate Excel report");
                if (CheckForEsc(out currentKey))
                {
                    active = false;
                    return;
                }
                switch (currentKey)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        InitializeMongo();
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        MigrateMongoToSql();
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        ReadExcel();
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        InitializeSQL();
                        break;
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.D5:
                        var mongoDb = new SantasToyFactoryMongoData();
                        mongoDb.DropCollectionsFromDatabase();
                        break;
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.D6:
                        CreateJsonReports();
                        break;
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.D7:
                        CreateXMLReports();
                        break;
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.D8:
                        GenerateExcelReport();
                        break;
                    default:
                        ConsoleUtilities.ErrorMessage("Wrong command. Please try again");
                        break;
                }
            }
        }

        private static void GenerateExcelReport()
        {
            using (var sqliteContext = new SantasToyFactorySqliteContext())
            using (var mySqlContext = new ReportModel())
            {
                var sqliteDb = new ToyProductionDetailsRepository(sqliteContext);
                var toyProductionDetiles = sqliteDb.GetToyProductionDetails().ToList();
                var toyReports = mySqlContext.GetAll<ToyReport>();
            }
        }

        private static void CreateJsonReports()
        {
            ChooseServer();

            var reports = JsonReport.CreateReports();
            try
            {
                JsonReport.TransferToMySql(reports);
            }
            catch (DuplicateKeyException e)
            {
                ConsoleUtilities.ErrorMessage("Schema with data already exists in MySql.");
            }
            ConsoleUtilities.SuccessMessage("Reports are created successfully.");
        }

        private static void CreateXMLReports()
        {
            var generator = new XMLReportGenerator(@"..\..\..\Costs-by-Deliverers.xml");
            generator.GenerateXMLReport();
            ConsoleUtilities.SuccessMessage("XML report generated successfully!");
        }

        private static void ChooseServer()
        {
            ConsoleUtilities.MenuMessage("Press 1 for SQL Server or any other key for SQL Express");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1)
            {
                SantasToyFactorySqlContext.InitializeForSqlServer();
            }
            else
            {
                SantasToyFactorySqlContext.InitializeForSqlExpress();
            }
        }
        private static void MigrateMongoToSql()
        {
            ChooseServer();

            try
            {
                ConsoleUtilities.ProcessingMessage("Beginning data migration from MongoDb to SQL database");
                var mongoDb = new SantasToyFactoryMongoData();
                var SqlDb = new SantasToyFactoryDatabase();
                var migrationControler = new MongoToSqlMigrationController(mongoDb, SqlDb);
                migrationControler.MigrateDataToSql();
                ConsoleUtilities.SuccessMessage("Migration completed successfully.");
            }
            catch (Exception e)
            {
                ConsoleUtilities.ErrorMessage(String.Format("Error during data migration: {0}", e));
            }
        }

        private static void ReadExcel()
        {
            const string archiveLocation = @"../../../Delivery Reports.zip";
            const string unpackedLocation = @"../../../ExtractedExcelReports";
            ZipManipulator.ExtractFile(archiveLocation, unpackedLocation);
            var reportsReader = new DeliveryReportsReader(unpackedLocation);
            var allDeliveries = reportsReader.GetAll();
            var db = InitializeSQL();
            var countries = db.Children.All().Select(x => new { childId = x.Id, countryId = x.Adresss.Town.CountryId }).ToList();
            foreach (var delivery in allDeliveries)
            {
                var country = countries.Where(c => c.childId == delivery.ChildId).First().countryId;
                delivery.CountryId = country;
                db.Deliveries.Add(delivery);
            }
            db.SaveChanges();
            //ExcelManipulator.AddExcelInfoToDatabase("Server = .; Database = SantasToyFactoryDb; Integrated Security = true", excelFiles);
            //ExcelManipulator.AddExcelInfoToDatabase("Server = .\\SQLEXPRESS; Database = SantasToyFactoryDb; Integrated Security = true", excelFiles);
        }

        private static void InitializeMongo()
        {
            ConsoleUtilities.ProcessingMessage("Initializing MongoDb database");
            var mongoDb = new SantasToyFactoryMongoData();
            ConsoleUtilities.SuccessMessage("Database initialized successfully.");
        }

        private static SantasToyFactoryDatabase InitializeSQL()
        {
            ConsoleUtilities.MenuMessage("Press 1 for SQL Server or any other key for SQL Express");
            var key = Console.ReadKey();
            Console.WriteLine();
            if (key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1)
            {
                SantasToyFactorySqlContext.InitializeForSqlServer();
            }
            else
            {
                SantasToyFactorySqlContext.InitializeForSqlExpress();
            }
            var db = new SantasToyFactoryDatabase();

            ConsoleUtilities.ProcessingMessage("Initializing SQL database");
            try
            {

                var test = db.Deliverers.SearchFor(d => d.Id == 1).First().Name;

                ConsoleUtilities.SuccessMessage("Database initialized successfully.");
                Console.WriteLine("First deer name is: {0}", test);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                ConsoleUtilities.ErrorMessage(String.Format("Error while using database: {0}", e));
            }

            return db;
        }
    }
}
//http://content.time.com/time/specials/packages/completelist/0,29569,2049243,00.html