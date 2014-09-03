using Newtonsoft.Json;
using SantasToyFactory.DataLayer;
using SantasToyFactory.MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.OpenAccess;
using System.Text;
using System.Threading.Tasks;

namespace SantasToyFactory.ConsoleClient
{
    public static class JsonReport
    {
        public static List<ToyReport> CreateReports()
        {
            List<ToyReport> reports = new List<ToyReport>();

            SantasToyFactorySqlContext ctx = new SantasToyFactorySqlContext();
            var toys = ctx.Toys.ToList();
            foreach (var toy in toys)
            {
                int toyId = toy.Id;
                string toyName = toy.Name;
                decimal toyPrice = toy.Price;
                string toyDescription = toy.ToyType.AdditionalInfo;
                string toyProducer = toy.Producer.Name;

                reports.Add(new ToyReport {ID = toyId, Name =toyName, Price = toyPrice, Description = toyDescription, Producer = toyProducer});

                using (FileStream fs = File.Open(@"../../../Json-Reports/" + toy.Id + ".json", FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        using (JsonWriter writer = new JsonTextWriter(sw))
                        {

                            writer.Formatting = Formatting.Indented;
                            writer.WriteStartObject();
                            writer.WritePropertyName("toy-id");
                            writer.WriteValue(toyId);
                            writer.WritePropertyName("name");
                            writer.WriteValue(toyName);
                            writer.WritePropertyName("price");
                            writer.WriteValue(toyPrice);
                            writer.WritePropertyName("toy-description");
                            writer.WriteValue(toyDescription);
                            writer.WritePropertyName("toy-producer");
                            writer.WriteValue(toyProducer);
                        }
                    }
                }
            }

            return reports;
        }

        public static void TransferToMySql(List<ToyReport> reports)
        {
            UpdateDatabase();
            using (ReportModel ctx = new ReportModel())
            {
                ctx.UpdateSchema();
                ctx.Add(reports);
                ctx.SaveChanges();
            }
        }

        private static void UpdateDatabase()
        {
            using (var context = new ReportModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}
