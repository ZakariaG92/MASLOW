using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Tools
{
    public interface IMongoDBSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class MongoDBSettings : IMongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
