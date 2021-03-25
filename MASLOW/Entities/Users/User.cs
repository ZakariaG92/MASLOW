using AspNetCore.Identity.Mongo.Model;
using MASLOW.Entities.Privileges;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Users
{
    public class User : MongoUser<ObjectId>, IPrivileged
    {
        
    }
}
