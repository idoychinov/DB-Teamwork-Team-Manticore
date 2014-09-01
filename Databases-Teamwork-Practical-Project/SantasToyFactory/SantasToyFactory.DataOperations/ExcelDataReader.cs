﻿namespace SantasToyFactory.DataOperations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.OleDb;

    using SantasToyFactory.Models;


    public class ExcelDataReader
    {
        private OleDbConnection connection;
        private string connectionString;

        public ExcelDataReader(string fileLocation)
        {
            this.connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + fileLocation + ";";
        }

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
                            toys.Add(toy);
                        }
                    }
                }
            }
            return toys.AsEnumerable<Toy>();
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
                            country.Continent = (Continent) int.Parse(reader["ContinentId"].ToString());
                            countries.Add(country);
                        }
                    }
                }
            }
            return countries.AsEnumerable<Country>();
        }
    }
}
