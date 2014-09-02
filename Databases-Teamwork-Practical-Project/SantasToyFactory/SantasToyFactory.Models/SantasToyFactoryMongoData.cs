namespace SantasToyFactory.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;

    using SantasToyFactory.DataOperations;
    using SantasToyFactory.Models;

    public class SantasToyFactoryMongoData
    {
        private const string MongoDBConnection = @"mongodb://localhost";
        private const string DbName = "SantasToyFactoryDB";
        private const string ToysCollectionName = "Toys";
        private const string ProducersCollectionName = "Producers";
        private const string ToyTypesName = "ToyTypes";
        private const string AddressesName = "Addresses";
        private const string TownsName = "Towns";
        private const string CountriesName = "Countries";
        private const string ChildrenName = "Children";
        private const string YearDateName = "Year";

        private const string DataInitializationDocumentString = "../../../dataInfo.xls";

        private readonly MongoServer server;
        private MongoDatabase db;

        public SantasToyFactoryMongoData()
        {
            this.server = new MongoClient(MongoDBConnection).GetServer();
            this.db = this.server.GetDatabase(DbName);
            InitializeDatabase();
        }

        public void InitializeDatabase()
        {
            var excelData = new ExcelDataReader(DataInitializationDocumentString);
            var collection = this.db.GetCollection(ToysCollectionName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetToys(ToysCollectionName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(ProducersCollectionName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetProducers(ProducersCollectionName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(ToyTypesName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetToyTypes(ToyTypesName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(AddressesName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetAddresses(AddressesName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(TownsName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetTowns(TownsName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(CountriesName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetCountries(CountriesName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(ChildrenName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetChildren(ChildrenName);
                collection.InsertBatch(data);
            }

            collection = db.GetCollection(YearDateName);
            if (collection.Count() == 0)
            {
                var data = excelData.GetYears(YearDateName);
                collection.InsertBatch(data);
            }
        }

        public void DropCollectionsFromDatabase()
        {
            this.db.Drop();
        }

        public IEnumerable<Toy> GetToys()
        {
            var collection = this.db.GetCollection<Toy>(ToysCollectionName).FindAll().AsEnumerable<Toy>();
            return collection;
        }

        public IEnumerable<Producer> GetProducers()
        {
            var collection = this.db.GetCollection<Producer>(ProducersCollectionName).FindAll().AsEnumerable<Producer>();
            return collection;
        }

        public IEnumerable<ToyType> GetToyTypes()
        {
            var collection = this.db.GetCollection<ToyType>(ToyTypesName).FindAll().AsEnumerable<ToyType>();
            return collection;
        }

        public IEnumerable<Address> GetAddresses()
        {
            var collection = this.db.GetCollection<Address>(AddressesName).FindAll().AsEnumerable<Address>();
            return collection;
        }

        public IEnumerable<Town> GetTowns()
        {
            var collection = this.db.GetCollection<Town>(TownsName).FindAll().AsEnumerable<Town>();
            return collection;
        }

        public IEnumerable<Country> GetCountries()
        {
            var collection = this.db.GetCollection<Country>(CountriesName).FindAll().AsEnumerable<Country>();
            return collection;
        }

        public IEnumerable<Child> GetChildren()
        {
            var collection = this.db.GetCollection<Child>(ChildrenName).FindAll().AsEnumerable<Child>();
            return collection;
        }

        public IEnumerable<YearDate> GetYearDates()
        {
            var collection = this.db.GetCollection<YearDate>(YearDateName).FindAll().AsEnumerable<YearDate>();
            return collection;
        }
    }
}
