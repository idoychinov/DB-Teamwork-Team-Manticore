namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ClosedXML.Excel;
    using SantasToyFactory.Models;
    using SantasToyFactory.MySqlConnector;
    using Telerik.OpenAccess;

    public class ExcelWriter
    {
        public static void GenerateProductionCostsFile(string filePath, IEnumerable<ToyReport> toyReports, IEnumerable<ToyProductionDetails> toyProductionDetails)
        {
            
            var consolidatedReport = from report in toyReports
                                     join detaile in toyProductionDetails on report.ID equals detaile.Id
                                     select new { ProducerName = report.Producer, Price = report.Price, CommissionPercent = detaile.CommissionPercent, Quantity = detaile.Quantity };
            
            var excelQuery = consolidatedReport.GroupBy(r => r.ProducerName)
                .Select(g => new
                {
                    ProducerName = g.Key,
                    CommissionPercent = g.Average(c => c.CommissionPercent),
                    TotalQuantityProduced = g.Sum(t => t.Quantity),
                    ProductionCommisionsExpences = g.Sum(t => t.Price * t.Quantity * t.CommissionPercent / 100)
                });
            var resultSet = excelQuery.ToList();

            
            var file = new XLWorkbook();
            var sheet = file.Worksheets.Add("ProductionCostsReport");
            
            sheet.Cell("A1").Value = "Producer";
            sheet.Cell("A1").Value = "Average Commission Percent";
            sheet.Cell("C1").Value = "Total Quantity Produced";
            sheet.Cell("D1").Value = "Production Commissions Expenses";

            for (int i = 0; i <resultSet.Count ; i++)
            {
                var row = i +2;
                sheet.Cell("A"+row).Value = resultSet[i].ProducerName;
                sheet.Cell("B"+row).Value = resultSet[i].CommissionPercent;
                sheet.Cell("C"+row).Value = resultSet[i].TotalQuantityProduced;
                sheet.Cell("D" + row).Value = resultSet[i].ProductionCommisionsExpences;
            }

            var tableRange = sheet.Range("A1", "D" + (resultSet.Count + 1));
            var reportTable = tableRange.CreateTable();

            sheet.Columns().AdjustToContents();
            file.SaveAs(filePath);
        }
    }
}
