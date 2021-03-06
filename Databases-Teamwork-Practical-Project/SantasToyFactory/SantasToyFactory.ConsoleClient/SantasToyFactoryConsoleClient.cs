﻿namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Data.SQLite.EF6;
    using Telerik.OpenAccess.Exceptions;

    using SantasToyFactory.DataLayer;
    using SantasToyFactory.DataOperations;
    using SantasToyFactory.Models;
    using SantasToyFactory.MySqlConnector;
    using SantasToyFactory.SqliteDb;
    using PdfSharp;
    using PdfSharp.Drawing;
    using PdfSharp.Pdf;

    public class SantasToyFactoryConsoleClient
    {
        static void Main()
        {
            ConsoleUtilities.MenuMessage("For step by step execution press 1. Press any other key for testing main menu.");
            ConsoleKey currentKey = Console.ReadKey(true).Key;
            if (currentKey == ConsoleKey.NumPad1 || currentKey == ConsoleKey.D1)
            {
                StepByStepExecution();
            }
            else
            {
                MainMenu();
            }
        }

        private static void StepByStepExecution()
        {
            ChooseServer();
            ConsoleUtilities.MenuMessage("Press any key to initialize MongoDb");
            Console.ReadKey(true);
            var mongoDb = InitializeMongo();

            ConsoleUtilities.MenuMessage("Press any key to start MongoDb to SQL migration");
            Console.ReadKey(true);
            Console.WriteLine();
            var sqlDb = new SantasToyFactoryDatabase();
            try
            {
                ConsoleUtilities.ProcessingMessage("Beginning data migration from MongoDb to SQL database");
                var migrationControler = new MongoToSqlMigrationController(mongoDb, sqlDb);
                migrationControler.MigrateDataToSql();
                ConsoleUtilities.SuccessMessage("Migration completed successfully.");
            }
            catch (Exception e)
            {
                ConsoleUtilities.ErrorMessage(String.Format("Error during data migration: {0}", e));
            }


            ConsoleUtilities.MenuMessage("Press any key to view sample data");
            Console.ReadKey(true);
            foreach (var toy in sqlDb.Toys.All().ToList())
            {
                Console.WriteLine("Toy: {0}, Type: {1}, Producer: {2}", toy.Name, toy.ToyType.GroupAge, toy.Producer.Name);
            }

            ConsoleUtilities.MenuMessage("Press any key to read excel delivery reports");
            Console.ReadKey(true);
            ReadExcel(sqlDb);

            ConsoleUtilities.MenuMessage("Press any key to view sample data");
            Console.ReadKey(true);
            foreach (var delivery in sqlDb.Deliveries.All().ToList())
            {
                Console.WriteLine("{0} received {1} {2}, at {3}", delivery.Child.Name, delivery.Quantity, delivery.Toy.Name, delivery.Date);
            }

            ConsoleUtilities.MenuMessage("Press any key to generate PDF Reports");
            Console.ReadKey(true);
            GeneratePDFreports(sqlDb);

            ConsoleUtilities.MenuMessage("Press any key to generate XML Reports");
            Console.ReadKey(true);
            CreateXMLReports();

            ConsoleUtilities.MenuMessage("Press any key to generate Json Reports");
            Console.ReadKey(true);
            CreateJsonReports();

            ConsoleUtilities.MenuMessage("Press any key to load data from XML");
            Console.ReadKey(true);
            LoadXMLData(sqlDb);

            ConsoleUtilities.MenuMessage("Press any key to generate excel report");
            Console.ReadKey(true);
            GenerateExcelReport();
        }

        private static bool CheckForEsc(out ConsoleKey pressedKey)
        {
            pressedKey = Console.ReadKey(true).Key;
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
                    "\n  5 to clear data from MongoDb \n  6 to create Json reports and transfer them to MySql \n  7 to Create XML report \n  8 to Generate Excel report  \n  9 to Generate PDF report  \n  0 to Load XML data");
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
                        ReadExcel(InitializeSQL());
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
                        ChooseServer();
                        CreateXMLReports();
                        break;
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.D8:
                        GenerateExcelReport();
                        break;
                    case ConsoleKey.NumPad9:
                    case ConsoleKey.D9:
                        GeneratePDFreports(InitializeSQL());
                        break;
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.D0:
                        LoadXMLData(InitializeSQL());
                        break;
                    default:
                        ConsoleUtilities.ErrorMessage("Wrong command. Please try again");
                        break;
                }
            }
        }

        private static void LoadXMLData(SantasToyFactoryDatabase db)
        {
            var xmlreader = new XMLReader(@"..\..\..\Children-Behaviors.xml", db);
            xmlreader.LoadBehaviorData();
            ConsoleUtilities.SuccessMessage("Successfully loaded xml data!");
        }

        private static void GeneratePDFreports(SantasToyFactoryDatabase db)
        {
            var report = new PdfDocument();
            //report.Info.Title = "PDF export try";
            PdfPage page = report.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            DrawTitle(report, page, gfx, "Deliveries by location");
            DrawText(gfx, 0, report);

            int horizontalFirst = 60;
            int horizontalSecond = 145;
            int horizontalThird = 295;
            int vertical = 75;
            var locations = db.Countries.All();
            var query = db.Deliveries.All().GroupBy(x => new { Country = x.Country.Name, Toy = x.Toy.Name }).Select(g =>
                new { CountryName = g.Key.Country, ToyName = g.Key.Toy, Deliveries = g.Count() });
            foreach (var item in query)
            {

                gfx.DrawString(item.CountryName, new XFont("Times New Roman", 9, XFontStyle.Regular, new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default)), XBrushes.DarkSlateGray, horizontalFirst, vertical, format);
                gfx.DrawString(item.ToyName.ToString(), new XFont("Times New Roman", 9, XFontStyle.Regular, new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default)), XBrushes.DarkSlateGray, horizontalSecond, vertical, format);
                gfx.DrawString(item.Deliveries.ToString(), new XFont("Times New Roman", 9, XFontStyle.Regular, new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default)), XBrushes.DarkSlateGray, horizontalThird, vertical, format);

                vertical += 25;
            }

            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 50, 45, 50, vertical);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 135, 45, 135, vertical);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 290, 45, 290, vertical);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 450, 45, 450, vertical);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 50, vertical, 450, vertical);

            report.Save(@"../../../PDF reports/test-report.pdf");
            ConsoleUtilities.SuccessMessage("Report is created successfully.");
        }

        public static void DrawTitle(PdfDocument report, PdfPage page, XGraphics gfx, string title)
        {
            XRect rect = new XRect(new XPoint(), gfx.PageSize);
            rect.Inflate(-10, -15);
            XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
            gfx.DrawString(title, font, XBrushes.MidnightBlue, rect, XStringFormats.TopCenter);
            rect.Offset(0, 5);
            font = new XFont("Verdana", 8, XFontStyle.Italic);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Far;
            //gfx.DrawString("Created with " + PdfSharp.ProductVersionInfo.Producer, font, XBrushes.DarkOrchid, rect, format);
            font = new XFont("Verdana", 8);
            format.Alignment = XStringAlignment.Center;
            gfx.DrawString(report.PageCount.ToString(), font, XBrushes.DarkOrchid, rect, format);
            report.Outlines.Add(title, page, true);
        }

        public static void DrawText(XGraphics gfx, int number, PdfDocument d)
        {
            const string facename = "Times New Roman";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont fontRegular = new XFont(facename, 9, XFontStyle.Regular, options);
            XFont fontBold = new XFont(facename, 14, XFontStyle.Bold, options);
            XFont fontItalic = new XFont(facename, 20, XFontStyle.Italic, options);
            XFont fontBoldItalic = new XFont(facename, 20, XFontStyle.BoldItalic, options);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 50, 45, 450, 45);
            gfx.DrawString("Location", fontBold, XBrushes.DarkSlateGray, 65, 50, format);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 135, 45, 135, 265);
            gfx.DrawString("Toy Name", fontBold, XBrushes.DarkSlateGray, 150, 50, format);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 290, 45, 290, 265);
            gfx.DrawString("Number Of Deliveries", fontBold, XBrushes.DarkSlateGray, 300, 50, format);
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }), 50, 70, 450, 70);
        }

        private static void GenerateExcelReport()
        {
            using (var sqliteContext = new SantasToyFactorySqliteContext())
            using (var mySqlContext = new ReportModel())
            {
                var sqliteDb = new ToyProductionDetailsRepository(sqliteContext);
                var toyProductionDetiles = sqliteDb.GetToyProductionDetails().ToList();
                var toyReports = mySqlContext.GetAll<ToyReport>().ToList();

                try
                {
                    ExcelWriter.GenerateProductionCostsFile(@"..\..\..\GeneratedReport\ProductionCostsReport.xlsx", toyReports, toyProductionDetiles);
                    ConsoleUtilities.SuccessMessage(string.Format("Excel Report generated successfully"));
                }
                catch (Exception e)
                {
                    ConsoleUtilities.ErrorMessage(string.Format("Error generating excel reports {0}", e));

                }
            }
        }

        private static void CreateJsonReports()
        {
            var reports = JsonReport.CreateReports();
            try
            {
                JsonReport.TransferToMySql(reports);
            }
            catch (DuplicateKeyException)
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
            var key = Console.ReadKey(true);
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

        private static void ReadExcel(SantasToyFactoryDatabase db)
        {
            const string archiveLocation = @"../../../Delivery Reports.zip";
            const string unpackedLocation = @"../../../ExtractedExcelReports";
            ConsoleUtilities.ProcessingMessage("Beginning to unzip and read data and load it to SQL server");
            ZipManipulator.ExtractFile(archiveLocation, unpackedLocation);
            var reportsReader = new DeliveryReportsReader(unpackedLocation);
            var allDeliveries = reportsReader.GetAll();
            var countries = db.Children.All().Select(x => new { childId = x.Id, countryId = x.Adresss.Town.CountryId }).ToList();
            foreach (var delivery in allDeliveries)
            {
                var country = countries.Where(c => c.childId == delivery.ChildId).First().countryId;
                delivery.CountryId = country;
                db.Deliveries.Add(delivery);
            }
            db.SaveChanges();
            ConsoleUtilities.SuccessMessage("Data load compleated successfuly.");

            //ExcelManipulator.AddExcelInfoToDatabase("Server = .; Database = SantasToyFactoryDb; Integrated Security = true", excelFiles);
            //ExcelManipulator.AddExcelInfoToDatabase("Server = .\\SQLEXPRESS; Database = SantasToyFactoryDb; Integrated Security = true", excelFiles);
        }

        private static SantasToyFactoryMongoData InitializeMongo()
        {
            ConsoleUtilities.ProcessingMessage("Initializing MongoDb database");
            var mongoDb = new SantasToyFactoryMongoData();
            ConsoleUtilities.SuccessMessage("Database initialized successfully.");
            return mongoDb;
        }

        private static SantasToyFactoryDatabase InitializeSQL()
        {
            ConsoleUtilities.MenuMessage("Press 1 for SQL Server or any other key for SQL Express");
            var key = Console.ReadKey(true);
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