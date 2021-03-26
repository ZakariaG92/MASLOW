using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Users
{
    public class Role : MongoRole<ObjectId>, IEntity
    {
        [BsonId, ObjectId, Ignore]
        public string ID { get => Id.ToString(); set => Id = new ObjectId(value); }

        public string GenerateNewID() => Id.ToString();
    }
}
