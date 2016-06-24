using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MongoDB;

namespace DB
{
    public class MongoClientFactory
    {
        public static MongoClient GetMongoClient()
        {                        
            var dbConnectionString = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(dbConnectionString);
            if (!String.IsNullOrEmpty(dbConnectionString))
            {
                return new MongoClient(dbConnectionString);
            }
            return new MongoClient();

        }
        public static IMongoDatabase GetMongoDatabase(String name = "mongodb")
        {
           return GetMongoClient().GetDatabase(name);
        }
        public static MongoDatabase GetMongoDatabase2(String name = "mongodb")
        {
            var connectionstring =
            ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            var database = server.GetDatabase(url.DatabaseName);
            return database;
            //return GetMongoClient().GetDatabase(name);
        }
    }
}
