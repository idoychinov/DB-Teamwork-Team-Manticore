namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public static class ExcelManipulator
    {
        public static void AddExcelInfoToDatabase(string connectionString, ICollection<string> excelFiles)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using(connection)
	        {
		        foreach (var path in excelFiles)
                {
                    string oleDbConnection = GetConnectionString(path);
                    using (OleDbConnection conn = new OleDbConnection(oleDbConnection))
                    {
                        conn.Open();
                        
                        OleDbCommand getTable = new OleDbCommand("SELECT * FROM [Delivery$]", conn);
                        OleDbDataReader reader = getTable.ExecuteReader();

                        while (reader.Read())
                        {
                            double yearId = (double)reader["YearId"];
                            double delivererId = (double)reader["DelivererId"];
                            string query = "INSERT Deliveries(YearId, DelivererId) VALUES ( @yearId, @delivererId)";
                            SqlCommand insertProduct = new SqlCommand(query, connection);

                            insertProduct.Parameters.AddWithValue("@yearId", yearId);
                            insertProduct.Parameters.AddWithValue("@delivererId", delivererId);
                            insertProduct.ExecuteNonQuery();
                        }

                        conn.Close();
                    }
                }
	        }
            
        }

        private static string GetConnectionString(string fileName)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            props["Extended Properties"] = "Excel 8.0";
            props["Data Source"] = fileName;

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
    }
}
