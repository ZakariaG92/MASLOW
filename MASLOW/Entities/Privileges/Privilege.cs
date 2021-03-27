﻿using MASLOW.Entities.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Privileges
{
    public abstract class Privilege<T> where T : IPrivileged
    {
        [BsonId]
        public virtual string Id { get; set; }

        public IActionnable Actionnable { get; set; }

        //If null, accept all Actions
        public string Action { get; set; }

        public PrivilegeMode Mode { get; set; }

        public T Privileged { get; set; }

        public IEnumerable<ISensor> Sensors { get; set; }

        public Func<bool> GetActiveStatus { get; set; }

        public abstract bool IsUserConserned(User currentUser);
    }

    public enum PrivilegeMode
    {
        ALLOW,
        DENY
    }

    public class UserPrivilege : Privilege<User>
    {
        public override bool IsUserConserned(User currentUser)
        {
            return Privileged == currentUser;
        }
    }

    public class GroupPrivilege : Privilege<Group>
    {
        public override bool IsUserConserned(User currentUser)
        {
            return Privileged.Users.Contains(currentUser);
        }
    }
}
