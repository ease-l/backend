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
            var dbConnectionString = ConfigurationManager.AppSettings.Get("(MONGOHQ_URL|MONGOLAB_URI)");
            var url = new MongoUrl(dbConnectionString);
            
            return new MongoClient(url);
            
        }
        
        public static IMongoDatabase GetMongoDatabase(String name = "mongodb")
        {
            var connectionstring =
                ConfigurationManager.AppSettings.Get("(MONGOHQ_URL|MONGOLAB_URI)");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);            
            var database = client.GetDatabase(url.DatabaseName);
            return database;
            //return GetMongoClient().GetDatabase(name);
        }
    }
}
