using AspNetCore.Identity.Mongo.Model;
using MASLOW.Entities.Privileges;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Users
{
    public class User : MongoUser<ObjectId>, IPrivileged, IEntity
    {
        [BsonId, ObjectId, Ignore]
        public string ID { get => Id.ToString(); set => Id = new ObjectId(value); }

        public string GenerateNewID() => Id.ToString();

        [InverseSide]
        public Many<Group> Groups { get; set; }

        public User() : base()
        {
            this.InitManyToMany(() => Groups, group => group.Users);
        }

        public User(string name) : base(name)
        {
            this.InitManyToMany(() => Groups, group => group.Users);
        }
    }
}
