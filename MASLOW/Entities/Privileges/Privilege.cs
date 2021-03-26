using MASLOW.Entities.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Privileges
{
    public abstract class Privilege<T> where T : IPrivileged
    {
        public virtual string ID { get; set; }

        public One<IActionnable> Actionnable { get; set; }

        //If null, accept all Actions
        public string Action { get; set; }

        public PrivilegeMode Mode { get; set; }

        public One<T> Privileged { get; set; }

        [OwnerSide]
        public Many<ISensor> Sensors { get; set; }

        public Func<bool> GetActiveStatus { get; set; }

        public abstract bool IsUserConserned(User currentUser);
    }

    public enum PrivilegeMode
    {
        ALLOW,
        DENY
    }

    public class UserPrivilege : Privilege<User>, IEntity
    {
        [BsonId, ObjectId]
        public override string ID { get; set; }

        public string GenerateNewID()
        {
            return ObjectId.GenerateNewId().ToString();
        }

        public override bool IsUserConserned(User currentUser)
        {
            return currentUser.ID == Privileged.ID;
        }
    }

    public class GroupPrivilege : Privilege<Group>, IEntity
    {
        [BsonId, ObjectId]
        public override string ID { get; set; }

        public string GenerateNewID()
        {
            return ObjectId.GenerateNewId().ToString();
        }

        public override bool IsUserConserned(User currentUser)
        {
            var query = Privileged.ToEntityAsync();
            query.Wait();

            return query.Result.Users.Contains(currentUser);
        }
    }
}
