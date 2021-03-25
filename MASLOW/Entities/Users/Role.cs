using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Users
{
    public class Role : MongoRole<ObjectId>
    {
    }
}
