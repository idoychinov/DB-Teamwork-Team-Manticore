namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.OleDb;

    using SantasToyFactory.Models;


    public class ExcelDataReader
    {
        private readonly OleDbConnection connection;

        public ExcelDataReader(OleDbConnection connection)
        {
            this.connection = connection;
        }

        public IEnumerable<Toy> GetToys(string sheet)
        {
            return GetElementsFromExcelSheet<Toy>(sheet);
        }

        public IEnumerable<Child> GetChilds(string sheet)
        {
            return GetElementsFromExcelSheet<Child>(sheet);
        }

        public IEnumerable<Producer> GetProducers(string sheet)
        {
            return GetElementsFromExcelSheet<Producer>(sheet);
        }

        public IEnumerable<ToyType> GetToyType(string sheet)
        {
            return GetElementsFromExcelSheet<ToyType>(sheet);
        }

        public IEnumerable<Address> GetAddresses(string sheet)
        {
            return GetElementsFromExcelSheet<Address>(sheet);
        }

        public IEnumerable<Town> GetTowns(string sheet)
        {
            return GetElementsFromExcelSheet<Town>(sheet);
        }

        public IEnumerable<Country> GetCountries(string sheet)
        {
            return GetElementsFromExcelSheet<Country>(sheet);
        }

        public IEnumerable<Delivery> GetDeliverys(string sheet)
        {
            var elements = new List<Delivery>();
            OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + sheet + "$]", this.connection);
            using (var reader = getTable.ExecuteReader())
            {

                while (reader.Read())
                {
                    if (!reader.IsDBNull(2))
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);

                        var delivery = new Delivery();
                        delivery.ChildId = int.Parse(reader[0].ToString());
                        delivery.ToyId = int.Parse(reader[1].ToString());
                        delivery.DelivererId = int.Parse(reader[2].ToString());
                        delivery.Quantity = int.Parse(reader[3].ToString());
                        elements.Add(delivery);
                    }
                    // var delivery = CreateDelivery(reader);
                    // elements.Add(delivery);

                }
            }
            return elements.AsEnumerable<Delivery>();
        }

        private IEnumerable<T> GetElementsFromExcelSheet<T>(string sheet) where T : class
        {
            var elements = new List<T>();
            OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + sheet + "$]", this.connection);
            using (var reader = getTable.ExecuteReader())
            {
                var typeName = typeof(T).Name;

                while (reader.Read())
                {
                    if (reader.IsDBNull(1))
                    {
                        break;
                    }
                    if (typeName == "Toy")
                    {
                        var toy = CreateToy(reader);
                        elements.Add((T)Convert.ChangeType(toy, typeof(T)));
                    }
                    else if (typeName == "Child")
                    {
                        var child = CreateChild(reader);
                        elements.Add((T)Convert.ChangeType(child, typeof(T)));
                    }
                    else if (typeName == "Producer")
                    {
                        var producer = CreateProducer(reader);
                        elements.Add((T)Convert.ChangeType(producer, typeof(T)));
                    }
                    else if (typeName == "ToyType")
                    {
                        var toyType = CreateToyType(reader);
                        elements.Add((T)Convert.ChangeType(toyType, typeof(T)));
                    }
                    else if (typeName == "Address")
                    {
                        var address = CreateAddress(reader);
                        elements.Add((T)Convert.ChangeType(address, typeof(T)));
                    }
                    else if (typeName == "Town")
                    {
                        var town = CreateTown(reader);
                        elements.Add((T)Convert.ChangeType(town, typeof(T)));
                    }
                    else if (typeName == "Country")
                    {
                        var country = CreateCountry(reader);
                        elements.Add((T)Convert.ChangeType(country, typeof(T)));
                    }
                }
            }
            return elements.AsEnumerable<T>();
        }

        private Toy CreateToy(OleDbDataReader reader)
        {
            var toy = new Toy();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                toy.Id = id;
                toy.Name = (string)reader["Name"];
                // toy.Price = decimal.Parse(reader["Price"].ToString());
                toy.ToyTypeId = int.Parse(reader["ToyTypeId"].ToString());
                toy.ProducerId = int.Parse(reader["ProducerId"].ToString());
            }
            return toy;
        }

        private Child CreateChild(OleDbDataReader reader)
        {
            var child = new Child();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                child.Id = id;
                child.Name = (string)reader["Name"];
                child.GroupAge = (GroupAge)int.Parse(reader["GroupAgeId"].ToString());
                // child.Behaviour = (Behaviour)int.Parse(reader["Behaviour"].ToString());
                child.AddressId = int.Parse(reader["AddressId"].ToString());
            }
            return child;
        }

        private Producer CreateProducer(OleDbDataReader reader)
        {
            var producer = new Producer();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                producer.Id = id;
                producer.Name = (string)reader["Name"];
            }
            return producer;
        }

        private ToyType CreateToyType(OleDbDataReader reader)
        {
            var toyType = new ToyType();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                toyType.Id = id;
                toyType.AdditionalInfo = (string)reader["AdditionalInfo"];
                toyType.GroupAge = (GroupAge)int.Parse(reader["GroupAgeId"].ToString());
            }
            return toyType;
        }

        private object CreateAddress(OleDbDataReader reader)
        {
            var address = new Address();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                address.Id = id;
                address.Description = (string)reader["Description"];
                address.TownId = int.Parse(reader["TownId"].ToString());
            }
            return address;
        }

        private Town CreateTown(OleDbDataReader reader)
        {
            var town = new Town();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                town.Id = id;
                town.Name = (string)reader["Name"];
                town.CountryId = int.Parse(reader["CountryId"].ToString());
                town.PostCode = reader["PostCode"].ToString();
            }
            return town;
        }

        private Country CreateCountry(OleDbDataReader reader)
        {
            var country = new Country();
            int id;
            if (int.TryParse(reader["Id"].ToString(), out id))
            {
                country.Id = id;
                country.Name = (string)reader["Name"];
                country.Continent = (Continent)int.Parse(reader["ContinentId"].ToString());
            }
            return country;
        }


        /*
        public IEnumerable<Toy> GetToys(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var toys = new List<Toy>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var toy = new Toy();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            toy.Id = id;
                            toy.Name = (string)reader["Name"];
                            toy.Price = decimal.Parse(reader["Price"].ToString());
                            toy.ToyTypeId = int.Parse(reader["ToyTypeId"].ToString());
                            toy.ProducerId = int.Parse(reader["ProducerId"].ToString());
                            toys.Add(toy);
                        }
                    }
                }
            }
            return toys.AsEnumerable<Toy>();
        }

        public IEnumerable<Child> GetChildren(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var children = new List<Child>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var child = new Child();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            child.Id = id;
                            child.Name = (string)reader["Name"];
                            child.GroupAge = (GroupAge)int.Parse(reader["GroupAgeId"].ToString());
                            // child.Behaviour = (Behaviour)int.Parse(reader["Behaviour"].ToString());
                            child.AddressId = int.Parse(reader["AddressId"].ToString());
                            children.Add(child);
                        }
                    }
                }
            }
            return children.AsEnumerable<Child>();
        }

        public IEnumerable<Producer> GetProducers(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var producers = new List<Producer>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var producer = new Producer();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            producer.Id = id;
                            producer.Name = (string)reader["Name"];
                            producers.Add(producer);
                        }
                    }
                }
            }
            return producers.AsEnumerable<Producer>();
        }

        public IEnumerable<ToyType> GetToyTypes(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var toyTypes = new List<ToyType>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var toyType = new ToyType();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            toyType.Id = id;
                            toyType.AdditionalInfo = (string)reader["AdditionalInfo"];
                            toyType.GroupAge = (GroupAge)int.Parse(reader["GroupAgeId"].ToString());
                            toyTypes.Add(toyType);
                        }
                    }
                }
            }
            return toyTypes.AsEnumerable<ToyType>();
        }

        public IEnumerable<Address> GetAddresses(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var addresses = new List<Address>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var address = new Address();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            address.Id = id;
                            address.Description = (string)reader["Description"];
                            address.TownId = int.Parse(reader["TownId"].ToString());
                            addresses.Add(address);
                        }
                    }
                }
            }
            return addresses.AsEnumerable<Address>();
        }

        public IEnumerable<Town> GetTowns(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var towns = new List<Town>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var town = new Town();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            town.Id = id;
                            town.Name = (string)reader["Name"];
                            town.CountryId = int.Parse(reader["CountryId"].ToString());
                            town.PostCode = reader["PostCode"].ToString();
                            towns.Add(town);
                        }
                    }
                }
            }
            return towns.AsEnumerable<Town>();
        }

        public IEnumerable<YearDate> GetYears(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var years = new List<YearDate>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var year = new YearDate();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            year.Id = id;
                            year.Year = int.Parse(reader["Year"].ToString());
                            years.Add(year);
                        }
                    }
                }
            }

            return years.AsEnumerable<YearDate>();
        }

        public IEnumerable<Country> GetCountries(string tableName)
        {
            this.connection = new OleDbConnection(connectionString);
            this.connection.Open();
            var countries = new List<Country>();
            using (this.connection)
            {
                OleDbCommand getTable = new OleDbCommand("SELECT * FROM [" + tableName + "$]", this.connection);
                using (var reader = getTable.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new Country();
                        int id;
                        if (int.TryParse(reader["Id"].ToString(), out id))
                        {
                            country.Id = id;
                            country.Name = (string)reader["Name"];
                            country.Continent = (Continent)int.Parse(reader["ContinentId"].ToString());
                            countries.Add(country);
                        }
                    }
                }
            }
            return countries.AsEnumerable<Country>();
        }
        */

        /*
        
        */
    }
}
