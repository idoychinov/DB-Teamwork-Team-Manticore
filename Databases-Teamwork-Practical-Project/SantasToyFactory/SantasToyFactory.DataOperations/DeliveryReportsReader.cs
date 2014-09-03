namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using SantasToyFactory.Models;

    public class DeliveryReportsReader
    {
        private string rootFolder;

        public DeliveryReportsReader(string folder)
        {
            this.rootFolder = folder;
        }

        public IEnumerable<Delivery> GetAll()
        {
            var fileNames = Directory.GetFiles(rootFolder, "*.xls", SearchOption.AllDirectories);
            var deliveries = new List<Delivery>();
            foreach (var filename in fileNames)
            {
                var date = filename.Substring(filename.Length - 15, 11);
                string connectionString ="Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source="+filename+";";

                OleDbConnection excelConnection = new OleDbConnection(connectionString);
                excelConnection.Open();
                var excelData = new ExcelDataReader(excelConnection);
                using (excelConnection)
                {
                    var data = excelData.GetDeliverys("Deliveries");
                    foreach (var delivery in data)
                    {
                        delivery.Date = ParseYear(date);
                        deliveries.Add(delivery);
                    }
                }

            }
            return deliveries.AsEnumerable<Delivery>();
        }

        private DateTime ParseYear(string dateString)
        {
            var year = int.Parse(dateString.Substring(dateString.Length - 4));
            var date = new DateTime(year,12,24);
            return date;
        }
    }
}
