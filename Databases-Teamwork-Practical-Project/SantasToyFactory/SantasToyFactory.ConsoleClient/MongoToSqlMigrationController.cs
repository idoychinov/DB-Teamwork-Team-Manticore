namespace SantasToyFactory.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantasToyFactory.DataLayer;
    using SantasToyFactory.DataOperations;

    public class MongoToSqlMigrationController
    {
        private SantasToyFactoryMongoData mongoDB;

        private SantasToyFactoryDatabase sqlDb;

        public MongoToSqlMigrationController(SantasToyFactoryMongoData mongoDB, SantasToyFactoryDatabase sqlDb)
        {
            this.mongoDB = mongoDB;
            this.sqlDb = sqlDb;
        }

        public void MigrateDataToSql()
        {

            if (sqlDb.Countries.All().Count() == 0)
            {
                var countriesCollection = this.mongoDB.GetCountries();
                foreach (var item in countriesCollection)
                {
                    this.sqlDb.Countries.Add(item);
                }
            }

            if (sqlDb.Towns.All().Count() == 0)
            {
                var townsCollection = this.mongoDB.GetTowns();
                foreach (var item in townsCollection)
                {
                    this.sqlDb.Towns.Add(item);
                }
            }

            if (sqlDb.Addresses.All().Count() == 0)
            {
                var addressesCollection = this.mongoDB.GetAddresses();
                foreach (var item in addressesCollection)
                {
                    this.sqlDb.Addresses.Add(item);
                }
            }

            if (sqlDb.ToyTypes.All().Count() == 0)
            {
                var toyTypesCollection = this.mongoDB.GetToyTypes();
                foreach (var item in toyTypesCollection)
                {
                    this.sqlDb.ToyTypes.Add(item);
                }
            }


            if (sqlDb.Producers.All().Count() == 0)
            {
                var produecersCollection = this.mongoDB.GetProducers();
                foreach (var item in produecersCollection)
                {
                    this.sqlDb.Producers.Add(item);
                }
            }

            if (sqlDb.Toys.All().Count() == 0)
            {
                var toysCollection = this.mongoDB.GetToys();
                foreach (var item in toysCollection)
                {
                    this.sqlDb.Toys.Add(item);
                }
            }

            if (sqlDb.Children.All().Count() == 0)
            {
                var childrenCollection = this.mongoDB.GetChildren();
                foreach (var item in childrenCollection)
                {
                    this.sqlDb.Children.Add(item);
                }
            }

            this.sqlDb.SaveChanges();
        }
    }
}
