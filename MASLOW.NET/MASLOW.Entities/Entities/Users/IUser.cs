using MASLOW.Entities.Privileges;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Users
{
    public interface IUser : IPrivileged
    {
        ObjectId Id { get; }
        string UserName { get; }
        string Email { get; }
        List<string> Roles { get; }
    }
}
