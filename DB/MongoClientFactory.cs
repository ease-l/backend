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
        public static MongoUrl GetMongoUrl()
        {
            return new MongoUrl(ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
        }
        public static MongoServer GetMongoServer()
        {
            var client = new MongoClient(GetMongoUrl());
            var server = client.GetServer();
            return server;

        }
        public static MongoDatabase GetMongoDatabase()
        {
            var database = GetMongoServer().GetDatabase(GetMongoUrl().DatabaseName);
            return database;
        }
    }
}
