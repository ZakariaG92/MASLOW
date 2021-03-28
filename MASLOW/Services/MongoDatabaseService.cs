using MASLOW.Entities.Items;
using MASLOW.Tools;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Services
{
    public class MongoDatabaseService
    {
        public IMongoDatabase Database { get; private set; }

        public IMongoCollection<Item> Items => Database.GetCollection<Item>("Items");

        public MongoDatabaseService(MongoDBSettings mongoDBSettings)
        {
            Database = new MongoClient(mongoDBSettings.ConnectionString).GetDatabase(mongoDBSettings.DatabaseName);

            BsonSerializer.RegisterDiscriminatorConvention(typeof(Item), new DiscriminatorConvention());

        }
    }
}
