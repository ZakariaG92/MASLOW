using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MASLOW.Entities;
using MASLOW.Entities.Privileges;
using MASLOW.Entities.Users;
using System.Text.Json.Serialization;
using MASLOW.Entities.Entities.Items;

namespace MASLOW.Entities.Items
{
    [BsonDiscriminator(RootClass = true, Required = true)]
    public abstract class Item : IActionnable, ISensor
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, string> Payload { get; set; }

        public abstract Dictionary<string, DataType> ExpectedPayload { get; }

        public abstract List<string> Actions { get; }

        [JsonIgnore]
        public List<GroupPrivilege> GroupPrivileges { get; set; }
        [JsonIgnore]
        public List<UserPrivilege> UserPrivileges { get; set; }

        public abstract List<string> Values { get; }

        public abstract bool DoAction(string action, Dictionary<string, string>? payload, IUser user);
        public abstract string GetValue(string value);
    }
}
