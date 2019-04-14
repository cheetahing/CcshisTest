using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore
{
    public class MongodbContext
    {
        private readonly IMongoDatabase _database = null;

        public static IMongoDatabase Create()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");

            return client.GetDatabase("mydb");
        }
    }
}
