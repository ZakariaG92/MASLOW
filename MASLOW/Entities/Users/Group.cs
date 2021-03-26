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
    public class Group : Entity, IPrivileged
    {
        public string Name { get; set; }

        public One<Place> Place { get; set; }

        [OwnerSide]
        public Many<User> Users { get; set; }

        public Group() : base()
        {
            this.InitManyToMany(() => Users, user => user.Groups);
        }
    }
}
