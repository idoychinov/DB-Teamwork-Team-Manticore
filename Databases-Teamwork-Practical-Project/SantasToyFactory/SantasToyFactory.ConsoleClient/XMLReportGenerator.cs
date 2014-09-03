namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Xml.Linq;
    using System.Linq;
    using System.Text;

    using SantasToyFactory.DataLayer;

    class XMLReportGenerator
    {
        public XMLReportGenerator(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName { get; set; }

        class Cost
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public DateTime Date { get; set; }

            public decimal TotalCost { get; set; }
        }

        public void GenerateXMLReport()
        {
            var db = new SantasToyFactorySqlContext();

            string query = "SELECT dr.Id, dr.Name, de.Date, SUM(t.Price) AS TotalCost FROM Deliverers dr INNER JOIN Deliveries de ON dr.Id = de.DelivererId INNER JOIN Toys t ON de.ToyId = t.Id GROUP By dr.Name, de.Date, dr.Id ORDER BY dr.Name";

            var result = db.Database.SqlQuery<Cost>(query);

            XElement costs = new XElement("costs");
            for (int i = 1; i <= db.Deliverers.Count(); i++)
            {
                var currentDeliverers = result.Where(d => d.Id == i);
                if (currentDeliverers.Count() != 0)
                {
                    XElement cost = new XElement("cost");
                    cost.SetAttributeValue("deliverer", currentDeliverers.FirstOrDefault().Name);
                    foreach (var item in currentDeliverers)
                    {
                        XElement summary = new XElement("summary");
                        summary.SetAttributeValue("date", item.Date);
                        summary.SetAttributeValue("total-cost", item.TotalCost);
                        cost.Add(summary);
                    }

                    costs.Add(cost);
                }
            }

            costs.Save(this.FileName);
        }
    }
}
