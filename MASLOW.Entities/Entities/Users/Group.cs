using MASLOW.Entities.Privileges;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Users
{
    public class Group : IPrivileged
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<IUser> Users { get; set; }
    }
}
