using MASLOW.Tools;
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

        public MongoDatabaseService(IMongoDBSettings mongoDBSettings)
        {
            Database = new MongoClient(mongoDBSettings.ConnectionString).GetDatabase(mongoDBSettings.DatabaseName);
        }
    }
}
