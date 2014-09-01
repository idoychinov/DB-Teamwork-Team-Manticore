﻿namespace SantasToyFactory.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Bson;
    using MongoDB.Driver;

    using SantasToyFactory.DataOperations;

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
        }

        public void DropCollectionsFromDatabase()
        {
            this.db.Drop();
        }
    }
}
